using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Sede_Checker.Abstract.Interfaces;
using Sede_Checker.Delegates;
using Sede_Checker.Entities.DTO;

namespace Sede_Checker.Implementation.Services.Proxy
{
    class SedeProxyService : IProxyService
    {
        public SedeCheckerProxyAdressDTO[] Adresses { get; private set; }

        public int NeadableProxyesToWork { get; private set; }

        private ILogger Logger { get; set; }
        
        public SedeProxyService(ILogger logger)
        {
            this.NeadableProxyesToWork = 10;
            this.Logger = logger;
        }

        public event ProxyCallback OnProxyCallback;

        private void InvokeOnProxyCallback()
        {
            if(OnProxyCallback != null)
            {
                this.OnProxyCallback(this, new ProxyCallbackResult(true));
            }
        }

        private SedeCheckerProxyAdressDTO[] ParseAdresses()
        {
            //var p = $@"{_plss.WorkDirectory}\{_plss.ProxyListFileName}";

            var p = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Sede\\proxyes.txt";


            //NotificationService.Info($"Let's parse proxy list file {p}...");

            if (!File.Exists(p))
            {
                var m = $"Hey man, I can't find proxy database here: {p}! Where I can find it?";
                //NotificationService.Error(m);
                //throw new ProxiesListFileNotFound(m);
            }

            StreamReader f = null;

            try
            {
                var sb = new StringBuilder();

                string l;
                f = new StreamReader(p);
                while ((l = f.ReadLine()) != null)
                    sb.Append(l);

                var ls = sb.ToString().Split(' ');

                var lsd = new List<SedeCheckerProxyAdressDTO>();

                foreach (var s in ls)
                {
                    var ip = s.Remove(s.IndexOf(':'));
                    var port = s.Remove(0, s.IndexOf(':') + 1);

                    lsd.Add(new SedeCheckerProxyAdressDTO
                    {
                        Ip = ip,
                        Host = port
                    });
                }
                return lsd.ToArray();
            }
            catch (Exception e)
            {
                //NotificationService.Error($"While trying to parse proxy list file {p}, error occured!");
                //NotificationService.Exception(e);
                return null;
            }
            finally
            {
                if (!ReferenceEquals(f, null))
                    f.Close();
            }
        }


        //private SedeCheckerProxyAdressDTO[] ParseAdresses()
        //{
        //    var fl = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Sede\\proxyes.txt";

        //    if (!File.Exists(fl))
        //    {
        //        MessageBox.Show("Some error detected: file with proxy adresses cannot be parsed", "Parse error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null;
        //    }

        //    try
        //    {
        //        string FileData = File.ReadAllText(fl);

        //        var FileDataArray = FileData.Split(' ');

        //        var Adresses = new List<SedeCheckerProxyAdressDTO>();
        //        foreach (var FD in FileDataArray)
        //        {
        //            var IpAdress = FD.Remove(FD.IndexOf(":"));
        //            var host = FD.Remove(0, FD.IndexOf(":") + 1);
        //            Adresses.Add(new SedeCheckerProxyAdressDTO()
        //            {
        //                Ip = IpAdress,
        //                Host = host
        //            });
        //        }

        //        return Adresses.ToArray();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }


            
        //}

        private SedeCheckerProxyAdressDTO CheckAdressState(SedeCheckerProxyAdressDTO adress)
        {
            const string NCSI_TEST_URL = "http://www.msftncsi.com/ncsi.txt";
            const string NCSI_TEST_RESULT = "Microsoft NCSI";
            const string NCSI_DNS = "dns.msftncsi.com";
            const string NCSI_DNS_IP_ADDRESS = "131.107.255.255";
            try
            {
                var ws = new WebClient
                {
                    Proxy = new WebProxy(adress.Ip, int.Parse(adress.Host))
                    {
                        UseDefaultCredentials = true,
                        BypassProxyOnLocal = true
                    }
                };

                var TestString = ws.DownloadString(NCSI_TEST_URL);
                if (TestString != NCSI_TEST_RESULT)
                {
                    adress.IsConnectionAvailible = false;
                    return adress;
                }
                var DnsHost = Dns.GetHostEntry(NCSI_DNS);
                if (DnsHost.AddressList.Length < 0 || DnsHost.AddressList[0].ToString() != NCSI_DNS_IP_ADDRESS)
                {
                    adress.IsConnectionAvailible = false;
                    return adress;
                }
                this.Logger.Info($"Address {adress.Ip}:{adress.Host} is online.");
                adress.IsConnectionAvailible = true;
                this.InvokeOnProxyCallback();
                return adress;
            }
            catch (WebException)
            {
                this.Logger.Warning($"Proxy [{adress.Ip}:{adress.Host}] are not stable, skip it...");
                adress.IsConnectionAvailible = false;
                return adress;
            }
            catch (System.Net.Sockets.SocketException)
            {
                this.Logger.Warning($"Proxy [{adress.Ip}:{adress.Host}] are not stable, skip it...");
                adress.IsConnectionAvailible = false;
                return adress;
            }
        }

        private void CheckProxyListState()
        {
            Adresses.AsParallel().WithExecutionMode(ParallelExecutionMode.ForceParallelism).ForAll(adress => UpdateProxyAdressState(ref adress));
        }

        private void UpdateProxyAdressState(ref SedeCheckerProxyAdressDTO adress)
        {
            adress = CheckAdressState(adress);
        }

        public SedeCheckerProxyAdressDTO GetAvailibleAdress()
        {
            var pa = new List<SedeCheckerProxyAdressDTO>();

            foreach (var adress in Adresses)
            {
                if (adress.IsConnectionAvailible)
                {
                    pa.Add(new SedeCheckerProxyAdressDTO()
                    {
                        Ip = adress.Ip,
                        Host = adress.Host,
                        IsConnectionAvailible = adress.IsConnectionAvailible
                    });
                }
            }
            if (pa.Count < NeadableProxyesToWork || pa.Count < 1)
            {
                //MessageBox.Show($"Sorry, but we can't launch webdriver now, we need {NeadableProxyesToWork} proxies to correct work, current proxies count is {pa.Count}");
                return null;
            }
            Random RandomIndex = new Random();
            int Index = RandomIndex.Next(pa.Count);
            return pa[Index];
        }

        public void LaunchCheckAdressesState()
        {
            Adresses = ParseAdresses();
            var Thread = new Thread(CheckProxyListState);
            Thread.Start();
        }
    }
}
