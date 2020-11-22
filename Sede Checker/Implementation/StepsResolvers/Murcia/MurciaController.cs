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
using Sede_Checker.Entities.Settings;

namespace Sede_Checker.Implementation.StepsResolvers.Murcia
{
    class MurciaController : IStepsController
    {
        private IRecaptchaV2ResolverTask _currentTask;

        private readonly ILogger _logger;

        private readonly IFileSystemService _f;

        public IRecaptchaV2Resolver CaptchaResolver;

        public IWebDriverResolver WebDriver;

        public SedeTaskData UserData;
        
        private readonly SedeCheckerProxyAdressDTO _proxy;

        public event StepsControllerMailCallback OnStepsControllerMailCallback;

        public event StepsControllerCitaStatusCallback OnStepsControllerCitaStatusCallback;

        private readonly SeaSettings _settings;

        private readonly BaseControllerSettings _controllerSettings;

        private readonly Dictionary<string, string> _scripts;

        public MurciaController(SedeTaskData user, 
                                    SedeCheckerProxyAdressDTO proxy, 
                                        ILogger logger, 
                                            IRecaptchaV2Resolver recaptchaV2Resolver,
                                                SeaSettings settings) : base()
        {
            this.UserData = user;

            this._logger = logger;

            _settings = settings;

            _controllerSettings = settings.ControllerSettings;

            this._proxy = proxy;

            _scripts = settings.SedeScripts.Scripts;

            _f = new FileService(_logger);

            CaptchaResolver = recaptchaV2Resolver;
        }

        private void InvokeOnStepsControllerMailCallback(string nameSurname, string attachment = "")
        {
            if (ReferenceEquals(OnStepsControllerMailCallback, null)) return;

            this.OnStepsControllerMailCallback(this, new StepsControllerMailCallbackEventArgs(nameSurname, attachment));
        }

        private void InvokeOnStepsControllerCitaStatusCallback(SedeTaskData data, string citaNumber)
        {
            if (ReferenceEquals(OnStepsControllerCitaStatusCallback, null)) return;

            this.OnStepsControllerCitaStatusCallback(this, new StepsControllerCitaStatusCallbackEventArgs(data, citaNumber));
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
            var t = new ChromeDriverTask
            {
                Name = UserData.CustomerNameAndSurname,
                TargetUrl = _settings.SitaUrl,
                Proxy = this._proxy
            };

            WebDriver = new ChromeDriverResolver(t, this._logger, this._controllerSettings.ChromeDriverSettings);

            if (!WebDriver.Initialize())
            {
                this._logger.Error("ChromeWebDriver can't be initialized -> Stop task!");
                return;
            }

            if (!this.DoFirstStep())
            {
                this._logger.Error("First step can't be solved, please, see messages higher to see more information");
                this.WebDriver.Dispose();
                return;
            }

            if (!this.DoSecondStep())
            {
                this.WebDriver.Dispose();
                this._logger.Error("Second step can't be solved, please, see messages higher to see more information");
                return;
            }

            if (!this.DoThirdStep())
            {
                this._logger.Error("Third step can't be solved, please, see messages higher to see more information");
                this.WebDriver.Dispose();
                return;
            }

            if (!this.DoFourthStep())
            {
                this._logger.Error("Fourth step can't be solved, please, see messages higher to see more information");
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
                            this.FinishSteps(this._currentTask.CaptchaSolution);
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

        private void FinishSteps(string solution)
        {
            this._logger.Info("Response from recaptcha ready... Finishing fourth step...");
            if (!this.FinishFourthStep(solution))
            {
                this._logger.Info("Fourth step can't be solved, please, see messages higher to see more information");
                this.WebDriver.Dispose();
                return;
            }
            this._logger.Info("Fourth step finished... Completing fifth step...");
            if (!this.DoFifthStep())
            {
                this._logger.Info("Fifth step can't be solved, please, see messages higher to see more information");
                this.CreateHTMLPage("fifth_error");
                if (!this.CaptchaResolver.Report(this._currentTask.RequestId, _settings.RucaptchaBadAction)) this._logger.Error($"Error, cannot report about capthca with id {this._currentTask.RequestId}");
                this.WebDriver.Dispose();
                return;
            }
            this._logger.Info("fifth step finished... Completing sixth step...");
            if (!this.CaptchaResolver.Report(this._currentTask.RequestId, _settings.RucaptchaSuccessAction)) this._logger.Error($"Error, cannot report about capthca with id {this._currentTask.RequestId}");
            if (!this.DoSixthStep())
            {
                this._logger.Error("Sixth step can't be solved, please, see messages higher to see more information");
                this.CreateHTMLPage("sixth_error");
                this.WebDriver.Dispose();
                return;
            }
            this._logger.Info("Sixth step finished... Completing Seventh step...");
            if (!this.DoSeventhStep())
            {
                this._logger.Error("Seventh step can't be solved, please, see messages higher to see more information");
                this.CreateHTMLPage("seventh_error");
                this.WebDriver.Dispose();
                return;
            }
            this._logger.Info("Seventh step finished... Completing Eight step...");
            if (!this.DoEightStep())
            {
                this._logger.Error("Eight step can't be solved, please, see messages higher to see more information");
                this.CreateHTMLPage("eight_error");
                this.WebDriver.Dispose();
                return;
            }
            this._logger.Info("Eight step finished... Completing Nineth step...");
            if (!this.DoNinethStep())
            {
                this._logger.Error("Nineth step can't be solved, please, see messages higher to see more information");
                this.CreateHTMLPage("nineth_error");
                this.WebDriver.Dispose();
                return;
            }

            string PS = this.WebDriver.GetPageSource();
            string Ufile = $"pdf/{UserData.CustomerNameAndSurname}/Cita.Previa.{UserData.CustomerNameAndSurname.Replace(" ", ".")}.{DateTime.Now.ToShortDateString()}.html";

            if (!_f.CreateDirectory($"pdf/{UserData.CustomerNameAndSurname}"))
            {
                _logger.Error($"Cannot create directory for {UserData.CustomerNameAndSurname}");
                return;
            }

            if (!_f.Create(Ufile, PS))
            {
                _logger.Error($"Cannot create file {UserData.CustomerNameAndSurname}");
                return;
            }

            this.InvokeOnStepsControllerCitaStatusCallback(UserData, this.WebDriver.GetDataFromPage("#justificanteFinal"));

            this.InvokeOnStepsControllerMailCallback(this.UserData.CustomerNameAndSurname, $"{_settings.DefaultAppDataDir}{Ufile}");

            this.WebDriver.Dispose();
        }

        private bool DoFirstStep()
        {
            if (this.WebDriver.SelectElementInList("#form", this.UserData.ProcedureRegion) &&
                this.WebDriver.ExecuteScript(_scripts["DomButtonClickAction"], new object[] { "#btnAceptar" }))
            {
                this._logger.Info("OK! First step solved successfully!");
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DoSecondStep()
        {
            if (this.WebDriver.SelectElementInList("#tramite", this.UserData.ProcedureName) &&
                this.WebDriver.ExecuteScript(_scripts["DomButtonClickAction"], new object[] { "#btnAceptar" }))
            {
                this._logger.Info("OK! Second step solved successfully!");
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DoThirdStep()
        {
            if (this.WebDriver.ExecuteScript(_scripts["DomButtonClickAction"], new object[] { "#btnEntrar" }))
            {
                this._logger.Info("OK! Second step solved successfully!");
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DoFourthStep()
        {
            var l = new List<bool> {
                this.WebDriver.InvokeMemberClick($"#{GetDocumentTypeId(this.UserData.DocumentType)}"),
                this.WebDriver.UpdateFieldData("#txtIdCitado", this.UserData.DocumentNumber),
                this.WebDriver.UpdateFieldData("#txtDesCitado", this.UserData.CustomerNameAndSurname),
                this.WebDriver.SelectElementInList("#txtPaisNac", this.UserData.Country)
            };

            if (!this.UserData.TieExpiredDate.Equals(DateTime.MinValue)) {
                l.Add(this.WebDriver.UpdateFieldData("#txtFecha",
                    this.UserData.TieExpiredDate.ToShortDateString().Replace(".", "/")));
            }

            if (!l.All(el => el.Equals(true))) return false;
            this._logger.Info("OK! Second step solved successfully!");
            this._logger.Info("Sending captcha to solving...");
            this._currentTask = this.CaptchaResolver.SendTaskToResolve();

            return true;
        }

        private bool DoFifthStep()
        {
            if (!this.WebDriver.ElementExists("#btnSalir")) return false;

            return this.WebDriver.ExecuteScript(_scripts["DomButtonClickAction"], new object[] { "#btnEnviar" });
        }

        private bool DoSixthStep()
        {
            if (this.WebDriver.ElementExists("#idSede"))
            {
                if (this.WebDriver.ExecuteScript(_scripts["SitaListSelector"], new object[] { "idSede", this.UserData.ProcedureCity }) &&
                    this.WebDriver.ExecuteScript(_scripts["DomButtonClickAction"], new object[] { "#btnSiguiente" }))
                {
                    return true;
                }
                else
                {
                    this.CreateHTMLPage("sixth_error");
                    this._logger.Warning("Error in scripts executing");
                    return false;
                }
            }
            else
            {
                this._logger.Warning("Any availible places on sita!");
                return false;
            }
        }

        private bool DoSeventhStep()
        {
            if (this.WebDriver.ElementExists("#txtTelefonoCitado"))
            {
                if (this.WebDriver.UpdateFieldData("#txtTelefonoCitado", this.UserData.CustomerPhoneNumber) &&
                    this.WebDriver.UpdateFieldData("#emailUNO", this.UserData.CustomerEmail) && 
                    this.WebDriver.UpdateFieldData("#emailDOS", this.UserData.CustomerEmail) &&
                    this.WebDriver.ExecuteScript(_scripts["DomButtonClickAction"], new object[] { "#btnSiguiente" }))
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        private bool DoEightStep()
        {
            if (this.WebDriver.ElementExists("$//*[@id='cita_1']/input"))
            {
                if (this.WebDriver.ExecuteScript(_scripts["SitaRadioDateSelector"], new object[] { ".mf-layout--module__s", this.UserData.SitaMinRequiredDate.ToShortDateString(), this.UserData.SitaMaxRequiredDate.ToShortDateString() }) &&
                    this.WebDriver.ExecuteScript(_scripts["DomButtonClickAction"], new object[] { "#btnSiguiente" }) &&
                    this.WebDriver.SolveAlertWindow())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool DoNinethStep()
        {
            if (this.WebDriver.ExecuteScript(_scripts["DomButtonClickAction"], new object[] { "#enviarCorreo" }) &&
                this.WebDriver.ExecuteScript(_scripts["DomButtonClickAction"], new object[] { "#chkTotal" }) &&
                this.WebDriver.ExecuteScript(_scripts["DomButtonClickAction"], new object[] { "#btnConfirmar" }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool FinishFourthStep(string solution)
        {
            if (this.WebDriver.ExecuteScript(_scripts["SitaGoogleCapthcaSolver"], new object[] { solution }) &&
                this.WebDriver.ExecuteScript(_scripts["DomButtonClickAction"], new object[] { "#btnEnviar" }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CreateHTMLPage(string FileName)
        {
            string PS = this.WebDriver.GetPageSource();
            string PDir = $"pages/{UserData.CustomerNameAndSurname}";

            _f.CreateDirectory(PDir);
            _f.Create($"{PDir}/{FileName}.html", PS);
        }

        private string GetDocumentTypeId(DocumentType DType)
        {
            switch (DType)
            {
                case DocumentType.PASSPORT:
                    return "rdbTipoDocPas";
                case DocumentType.NIE:
                    return "rdbTipoDocNie";
                case DocumentType.DNI:
                    return "rdbTipoDocDni";
                default:
                    return String.Empty;
            }
        }

        public void Dispose()
        {
            WebDriver?.Dispose();
        }
    }
}
