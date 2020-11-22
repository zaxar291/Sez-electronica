using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using Sede_Checker.Abstract.Interfaces;
using Sede_Checker.Delegates;
using Sede_Checker.DTO;
using Sede_Checker.Implementation.Captcha.RuCaptcha.Entities;
using Sede_Checker.Implementation.Captcha.RuCaptcha.Enums;

namespace Sede_Checker.Implementation.Captcha.RuCaptcha
{
    public class RuCaptchaResolver : IRecaptchaV2Resolver
    {
        private ILogger _logger;

        private const string CaptchaIsNotReady = "CAPCHA_NOT_READY";
        private const string CaptchaIsUnsolvable = "ERROR_CAPTCHA_UNSOLVABLE";
        private const string CaptchaReportSuccess = "OK_REPORT_RECORDED";

        private readonly Timer _t;

        private readonly List<RuCaptchaTask> _tasks;

        private const int TaskProccesingDelayMilliseconds = 20000;

        public RuCaptchaResolver(string victiomGoogleApiKey, string victimPageUrl, ILogger logger)
        {
            _tasks = new List<RuCaptchaTask>();
            _logger = logger;

            VictimGoogleApiKey = victiomGoogleApiKey;
            VictimPageUrl = victimPageUrl;
            RequestUrl =
                $"http://rucaptcha.com/in.php?key={CaptchaApiToken}&method=userrecaptcha&googlekey={VictimGoogleApiKey}&pageurl={VictimPageUrl}&json=1";

            var callback = new TimerCallback(Timer_Callback);
            _t = new Timer(callback, null, 0, TaskProccesingDelayMilliseconds);
        }

        public string RecaptchaResponse { get; private set; }
        public string Name => "RuCaptcha Resolver Service";

        public string Descriptions => "Visit https://rucaptcha.com/enterpage";

        public string CaptchaApiToken => "db25b1c19ebc16cb5463aa527ad50a0a";

        public string VictimGoogleApiKey { get; }

        public string VictimPageUrl { get; }

        public string RequestUrl { get; }

        public RuCaptchaTask[] Tasks => _tasks?.ToArray();

        public event RecaptchaV2TaskCallback OnRecaptchaV2TaskCallback;
        public event RecaptchaV2ServiceStatusInfoCallback OnServiceStatusInfoCallback;

        public event LoggerCallback OnLoggerCallback;

        public IRecaptchaV2ResolverTask SendTaskToResolve()
        {
            try
            {
                var r = (HttpWebRequest) WebRequest.Create(RequestUrl);

                using (var rr = (HttpWebResponse) r.GetResponse())
                {
                    using (var s = rr.GetResponseStream())
                    {
                        using (var sr = new StreamReader(s))
                        {
                            RecaptchaResponse = sr.ReadToEnd();

                            var trdto = JsonConvert.DeserializeObject<RucaptchaTaskResponseDto>(RecaptchaResponse);

                            var t = new RuCaptchaTask(trdto.Data)
                            {
                                RequestUrl = RequestUrl
                            };

                            _tasks.Add(t);

                            return t;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                _logger.Error("While trying to send task to RuCaptcha, exception occured!");
                _logger.Exception(exc);
                //InvokeOnLoggerCallback(exc.Message, "error");
                return null;
            }
        }

        private void InvokeOnRecaptchaV2TaskCallback(RuCaptchaTask task)
        {
            OnRecaptchaV2TaskCallback?.Invoke(this, new RecaptchaV2TaskResultEventArgs(task));
        }

        //private void InvokeOnLoggerCallback(string message, string type)
        //{
        //    OnLoggerCallback?.Invoke(this, new LoggerCallbackEventArgs(message, type));
        //}

        private void Timer_Callback(object obj)
        {
            UpdateBalance();
            TasksProccesing();
        }

        private void TasksProccesing()
        {
            if (ReferenceEquals(_tasks, null) || _tasks.Count.Equals(0)) return;

            try
            {
                foreach (var item in _tasks.Where(el => el.Status.Equals(RuCaptchaTaskStatus.NotReady)))
                {
                    var rr = (HttpWebRequest)WebRequest.Create(
                        $"http://rucaptcha.com/res.php?key={CaptchaApiToken}&action=get&id={item.RequestId}&json=1");

                    using (var r = (HttpWebResponse)rr.GetResponse())
                    {
                        using (var s = r.GetResponseStream())
                        {
                            using (var sr = new StreamReader(s))
                            {
                                RecaptchaResponse = sr.ReadToEnd();
                            }

                            if (RecaptchaResponse.Equals(CaptchaIsNotReady))
                            {
                                _logger.Info("Response from Recaptcha not ready... sleepping....");
                                InvokeOnRecaptchaV2TaskCallback(item);
                            }
                            else if (RecaptchaResponse.Equals(CaptchaIsUnsolvable))
                            {
                                _logger.Error(
                                    "Response from Recaptcha: captcha cannot be solved, check sita settings....");
                                InvokeOnRecaptchaV2TaskCallback(item);
                                item.Status = RuCaptchaTaskStatus.Unsolvable;
                            }
                            else
                            {
                                var trdto = JsonConvert.DeserializeObject<RucaptchaTaskResponseDto>(RecaptchaResponse);

                                if (!trdto.Data.Equals(CaptchaIsNotReady))
                                {
                                    item.CaptchaSolution = trdto.Data;
                                    InvokeOnRecaptchaV2TaskCallback(item);
                                }
                                else
                                {
                                    InvokeOnRecaptchaV2TaskCallback(item);
                                    _logger.Info("Response from Recaptcha not ready... sleepping....");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                _logger.Error("While trying to check task status in RuCaptcha, exception occured!");
                _logger.Exception(exc);
            }
        }

        //private void TasksProccesing()
        //{
        //    if (ReferenceEquals(_tasks, null) || _tasks.Count.Equals(0)) return;

        //    try
        //    {
        //        foreach (var item in _tasks.Where(el => el.Status.Equals(RuCaptchaTaskStatus.NotReady)))
        //        {
        //            var rr = (HttpWebRequest) WebRequest.Create(
        //                $"http://rucaptcha.com/res.php?key={CaptchaApiToken}&action=get&id={item.RequestId}&json=1");

        //            using (var r = (HttpWebResponse) rr.GetResponse())
        //            {
        //                using (var s = r.GetResponseStream())
        //                {
        //                    using (var sr = new StreamReader(s))
        //                    {
        //                        RecaptchaResponse = sr.ReadToEnd();
        //                    }

        //                    if (RecaptchaResponse.Equals(CaptchaIsNotReady))
        //                    {
        //                        _logger.Info("Response from Recaptcha not ready... sleepping....");
        //                        InvokeOnRecaptchaV2TaskCallback(item);
        //                    }
        //                    else if (RecaptchaResponse.Equals(CaptchaIsUnsolvable))
        //                    {
        //                        _logger.Error(
        //                            "Response from Recaptcha: captcha cannot be solved, check sita settings....");
        //                        InvokeOnRecaptchaV2TaskCallback(item);
        //                        item.Status = RuCaptchaTaskStatus.Unsolvable;
        //                    }
        //                    else
        //                    {
        //                        var trdto = JsonConvert.DeserializeObject<RucaptchaTaskResponseDto>(RecaptchaResponse);

        //                        if (!trdto.Data.Equals(CaptchaIsNotReady))
        //                        {
        //                            item.CaptchaSolution = trdto.Data;
        //                            InvokeOnRecaptchaV2TaskCallback(item);
        //                        }
        //                        else
        //                        {
        //                            InvokeOnRecaptchaV2TaskCallback(item);
        //                            _logger.Info("Response from Recaptcha not ready... sleepping....");
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        _logger.Error("While trying to check task status in RuCaptcha, exception occured!");
        //        _logger.Exception(exc);
        //        //InvokeOnLoggerCallback(exc.Message, "error");
        //    }
        //}

        public void UpdateBalance()
        {
            try
            {
                var rr = (HttpWebRequest) WebRequest.Create(
                    $"https://rucaptcha.com/res.php?key={CaptchaApiToken}&action=getbalance&json=1");

                using (var r = (HttpWebResponse) rr.GetResponse())
                {
                    using (var s = r.GetResponseStream())
                    {
                        using (var sr = new StreamReader(s))
                        {
                            var t = JsonConvert.DeserializeObject<RucaptchaTaskResponseDto>(sr.ReadToEnd());

                            InvokeServiceStatusInfoCallback(new CaptchaServiceInfoEventArgs
                            {
                                Balance = double.Parse(t.Data, NumberStyles.Any, CultureInfo.InvariantCulture)
                            });
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                _logger.Error("While trying to get balance status from RuCaptcha, exception occured!");
                _logger.Exception(exc);
            }
        }

        protected virtual void InvokeServiceStatusInfoCallback(CaptchaServiceInfoEventArgs eventargs)
        {
            OnServiceStatusInfoCallback?.Invoke(this, eventargs);
        }

        public bool Report(string CaptchaId, string action)
        {
            var rr = (HttpWebRequest)WebRequest.Create($"http://rucaptcha.com/res.php?key={this.CaptchaApiToken}&action={action}&id={CaptchaId}");

            using (HttpWebResponse RucapthcaTextResponse = (HttpWebResponse)rr.GetResponse())
            {
                using (Stream stream = RucapthcaTextResponse.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        this.RecaptchaResponse = reader.ReadToEnd();
                    }
                }
            }
            if (this.RecaptchaResponse.Equals(CaptchaReportSuccess)) return true;
            return false;
        }
    }
}