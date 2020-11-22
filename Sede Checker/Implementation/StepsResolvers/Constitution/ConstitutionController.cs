using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sede_Checker.Abstract.Interfaces;
using Sede_Checker.Implementation.Driver.Chrome;
using Sede_Checker.Implementation.Services.FileService;
using Sede_Checker.Entities.DTO;
using Sede_Checker.TaskFormParams.Properties;
using Sede_Checker.Delegates;
using Sede_Checker.Entities.Settings.Controllers;
using Sede_Checker.Implementation.Driver.Chrome.Entities;
using Sede_Checker.ConstitutionTaskFormParams.Properties;
using Sede_Checker.Implementation.Services.MailService;
using Sede_Checker.Entities.Settings;
using Sede_Checker.Entities.Settings.Scripts;

namespace Sede_Checker.Implementation.StepsResolvers.Constitution
{
    class ConstitutionController : IStepsController
    {

        public const string Url = "https://sede.administracionespublicas.gob.es/infoext2/jsp/indexnie.jsp";

        public const string ElemData = "return document.getElementsByClassName('textoTablaDer')[6].childNodes[0].innerText;";

        private IRecaptchaV2ResolverTask _currentTask;

        private readonly ILogger _logger;

        private readonly IFileSystemService _f;

        public IRecaptchaV2Resolver CaptchaResolver;

        public IWebDriverResolver WebDriver;

        public ConstitutionTaskData UserData;

        private readonly SedeCheckerProxyAdressDTO _proxy;

        public event StepsControllerMailCallback OnStepsControllerMailCallback;

        public event StepsControllerCitaStatusCallback OnStepsControllerCitaStatusCallback;

        private readonly BaseControllerSettings _settings;

        private readonly SeaSettings _s;

        private readonly Dictionary<string, string> _scripts;

        public ConstitutionController(ConstitutionTaskData user, 
                                        SedeCheckerProxyAdressDTO proxy, 
                                            ILogger logger, 
                                                IRecaptchaV2Resolver recaptchaV2Resolver,
                                                    SeaSettings settings,
                                                        ScriptsSettings scripts)
        {
            this.UserData = user;

            this._logger = logger;

            this._settings = settings.ControllerSettings;

            _s = settings;

            this._proxy = proxy;

            _scripts = scripts.Scripts;

            _f = new FileService(_logger);

            CaptchaResolver = recaptchaV2Resolver;
        }

        public event StepsControllerMailCallback StepsControllerMailCallback;
        public event StepsControllerCitaStatusCallback StepsControllerCitaStatusCallback;

        private void InvokeOnStepsControllerMailCallback(string nameSurname, string attachment = "")
        {
            if (ReferenceEquals(StepsControllerMailCallback, null)) return;

            this.StepsControllerMailCallback(this, new StepsControllerMailCallbackEventArgs(nameSurname, attachment));
        }

        private void InvokeOnStepsControllerCitaStatusCallback(SedeTaskData data, string citaNumber)
        {
            if (ReferenceEquals(StepsControllerCitaStatusCallback, null)) return;

            this.StepsControllerCitaStatusCallback(this, new StepsControllerCitaStatusCallbackEventArgs(data, citaNumber));
        }

        public async void SolveStepsAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    this.SolveSteps();
                }
                catch (Exception e)
                {
                    _logger.Exception(e);
                }
                finally
                {
                    Dispose();
                }
            });
        }

        public void SolveSteps()
        {
            _logger.Info("Solving new program tree!");

            var t = new ChromeDriverTask
            {
                Name = UserData.CustomerNameAndSurname,
                TargetUrl = Url,
                Proxy = this._proxy
            };

            this.WebDriver = new ChromeDriverResolver(t, this._logger, this._settings.ChromeDriverSettings);

            if (!WebDriver.Initialize())
            {
                this._logger.Error("ChromeWebDriver can't be initialized -> Stop task!");
                return;
            }

            if (!this.SolveFields())
            {
                this._logger.Info("Fields cannot be filled successfully!");
                this.WebDriver.Dispose();
                return;
            }
            for (var i = 0; i < 200; i++)
            {
                if (!ReferenceEquals(this._currentTask, null))
                {
                    switch (this._currentTask.Status)
                    {
                        case Captcha.RuCaptcha.Enums.RuCaptchaTaskStatus.Unknow:
                            break;
                        case Captcha.RuCaptcha.Enums.RuCaptchaTaskStatus.NotReady:
                            break;
                        case Captcha.RuCaptcha.Enums.RuCaptchaTaskStatus.Solved:
                            this.Finish(this._currentTask.CaptchaSolution);
                            return;
                        case Captcha.RuCaptcha.Enums.RuCaptchaTaskStatus.Unsolvable:
                            return;
                        default:
                            return;
                    }
                }
                _logger.Info($"{i + 1} attemp to check captcha solution...");
                Thread.Sleep(5000);
            }
        }

        private bool SolveFields()
        {
            if (this.WebDriver.UpdateFieldData("#txtnie", UserData.NIENumber) && 
                this.WebDriver.UpdateFieldData("#txtFecha", UserData.DateRequest.ToShortDateString().Replace(".", "/")) && 
                this.WebDriver.UpdateFieldData("#txtAnnoNac", UserData.CustomerDateBirth.Year.ToString()))
            {
                this._logger.Info("OK! Second step solved successfully!");
                this._logger.Info("Sending captcha to solving...");
                this._currentTask = this.CaptchaResolver.SendTaskToResolve();

                return true;
            }
            else
            {
                return false;
            }

        }

        private void Finish(string CaptchaSolution)
        {
            if (!this.AcceptCaptchaSolution(CaptchaSolution))
            {
                return;
            }

            string _rs = this.WebDriver.GetDataFromPage(null, ElemData);
            if (!_rs.Equals("EN TRAMITE"))
            {
                _s.MailServiceSettings.DefaultSubject = "[TEST] -> Notifacation from https://sede.administracionespublicas.gob.es/infoext2/ServletConsulta";
                _s.MailServiceSettings.DefaultBodyTemplate = $"<b>[REPORT ID]</b> [0] <br /> <b>[REPORT DATETIME]</b> [1] <br /> A NEW VALUE DETECTED FOR <b[2]</b>! A new value detected on this page -> {_rs} ";
                var _m = new MailService(_logger, _s.MailServiceSettings);
                _m.SendReport(UserData.CustomerNameAndSurname);
            }
        }

        private bool AcceptCaptchaSolution(string CaptchaSolution)
        {
            if (this.WebDriver.ExecuteScript(this._scripts["SitaGoogleCapthcaSolver"], new object[] { CaptchaSolution }) && 
                this.WebDriver.ExecuteScript(this._scripts["DomButtonClickAction"], new object[] { ".simbutton" }))
            {               
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            WebDriver?.Dispose();
        }
    }
}
