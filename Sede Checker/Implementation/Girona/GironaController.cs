using System;

using Sede_Checker.Abstract.Interfaces;
using Sede_Checker.Implementation.Captcha.RuCaptcha;
using Sede_Checker.Implementation.Services.UserService;
using Sede_Checker.Implementation.Driver.Chrome;
using Sede_Checker.Implementation.StepsResolvers.Islands.consts;
using Sede_Checker.Implementation.Services.FileService;
using Sede_Checker.Entities.DTO;
using Sede_Checker.TaskFormParams.Properties;
using Sede_Checker.Delegates;

namespace Sede_Checker.Implementation.StepsResolvers.IllesBalears
{
    class GironaController: StepsConsts, IStepsController
    {
        private IRecaptchaV2ResolverTask _currentTask;

        private ILogger Logger;

        private IFileSystemService _f;

        public IRecaptchaV2Resolver recaptcharResolver;

        public IWebDriverResolver webDriver;

        public SedeTaskData UserData;

        public UserService userService;

        private string RecaptchaResponse;

        private string MallorcaProcedure;

        private SedeCheckerProxyAdressDTO Adress;

        public event StepsControllerMailCallback OnStepsControllerMailCallback;

        public event StepsControllerCitaStatusCallback OnStepsControllerCitaStatusCallback;

        public event StepsControllerCaptchaCallback OnStepsControllerCaptchaCallback;

        public GironaController(SedeTaskData user, SedeCheckerProxyAdressDTO adress, ILogger logger, IRecaptchaV2Resolver recaptchaV2Resolver) : base()
        {
            this.UserData = user;

            this.Logger = logger;

            this.Adress = adress;

            this.userService = new UserService();

            _f = new FileService(Logger);

            recaptcharResolver = recaptchaV2Resolver;
            recaptcharResolver.OnRecaptchaV2TaskCallback += this.RecaptcharResolver_OnRecaptchaV2TaskCallback;
        }

        private void InvokeOnStepsControllerMailCallback(string nameSurname, string Attachment = "")
        {
            if (ReferenceEquals(OnStepsControllerMailCallback, null)) return;

            this.OnStepsControllerMailCallback(this, new StepsControllerMailCallbackEventArgs(nameSurname, Attachment));
        }

        private void InvokeOnStepsControllerCitaStatusCallback(SedeTaskData data, string citaNumber)
        {
            if (ReferenceEquals(OnStepsControllerCitaStatusCallback, null)) return;

            this.OnStepsControllerCitaStatusCallback(this, new StepsControllerCitaStatusCallbackEventArgs(data, citaNumber));
        }

        private void InvokeOnStepsControllerCaptchaCallback(IRecaptchaV2ResolverTask task)
        {
            if (ReferenceEquals(OnStepsControllerCaptchaCallback, null)) return;

            this.OnStepsControllerCaptchaCallback(this, new StepsControllerCaptchaCallbackEventArgs(task));
        }

        private void RecaptcharResolver_OnRecaptchaV2TaskCallback(object sender, RecaptchaV2TaskResultEventArgs eventArgs)
        {
            this.InvokeOnStepsControllerCaptchaCallback(eventArgs.Task);

            switch (eventArgs.Task.Status)
            {
                case Captcha.RuCaptcha.Enums.RuCaptchaTaskStatus.Unknow:
                    break;
                case Captcha.RuCaptcha.Enums.RuCaptchaTaskStatus.NotReady:
                    break;
                case Captcha.RuCaptcha.Enums.RuCaptchaTaskStatus.Solved:
                    this.FinishSteps(eventArgs.Task.CaptchaSolution);
                    break;
                case Captcha.RuCaptcha.Enums.RuCaptchaTaskStatus.Unsolvable:
                    this.webDriver.Dispose();
                    break;
            }
        }

        private void RecaptchaResolver_OnLoggerCallback(object sender, Delegates.LoggerCallbackEventArgs eventArgs)
        {
            switch (eventArgs.LogType)
            {
                case "info":
                    this.Logger.Info(eventArgs.LogMessage);
                    break;
                case "warn":
                    this.Logger.Warning(eventArgs.LogMessage);
                    break;
                case "error":
                    this.Logger.Error(eventArgs.LogMessage);
                    break;
            }
        }

        public void SolveSteps()
        {
            webDriver = new ChromeDriverResolver(SitaUrl, this.Adress, this.Logger);

            if (!this.DoFirstStep())
            {
                this.Logger.Error("First step can't be solved, please, see messages higher to see more information");
                this.webDriver.Dispose();
                return;
            }

            if (!this.DoSecondStep())
            {
                this.webDriver.Dispose();
                this.Logger.Error("Second step can't be solved, please, see messages higher to see more information");
                return;
            }

            if (!this.DoThirdStep())
            {
                this.Logger.Error("Third step can't be solved, please, see messages higher to see more information");
                this.webDriver.Dispose();
                return;
            }

            if (!this.DoFourthStep())
            {
                this.Logger.Error("Fourth step can't be solved, please, see messages higher to see more information");
                this.webDriver.Dispose();
                return;
            }
        }

        private void FinishSteps(string solution)
        {
            this.Logger.Info("Response from recaptcha ready... Finishing fourth step...");
            if (!this.FinishFourthStep(solution))
            {
                this.Logger.Info("Fourth step can't be solved, please, see messages higher to see more information");
                this.webDriver.Dispose();
                return;
            }
            this.Logger.Info("Fourth step finished... Completing fifth step...");
            if (!this.DoFifthStep())
            {
                this.Logger.Info("Fifth step can't be solved, please, see messages higher to see more information");
                this.CreateHTMLPage("fifth_error");
                this.webDriver.Dispose();
                return;
            }
            this.Logger.Info("fifth step finished... Completing sixth step...");
            if (!this.DoSixthStep())
            {
                this.Logger.Error("Sixth step can't be solved, please, see messages higher to see more information");
                this.CreateHTMLPage("sixth_error");
                this.webDriver.Dispose();
                return;
            }
            this.Logger.Info("Sixth step finished... Completing Seventh step...");
            if (!this.DoSeventhStep())
            {
                this.Logger.Error("Seventh step can't be solved, please, see messages higher to see more information");
                this.CreateHTMLPage("seventh_error");
                this.webDriver.Dispose();
                return;
            }
            this.Logger.Info("Seventh step finished... Completing Eight step...");
            if (!this.DoEightStep())
            {
                this.Logger.Error("Eight step can't be solved, please, see messages higher to see more information");
                this.CreateHTMLPage("eight_error");
                //this.webDriver.Dispose();
                return;
            }
            this.Logger.Info("Eight step finished... Completing Nineth step...");
            if (!this.DoNinethStep())
            {
                this.Logger.Error("Nineth step can't be solved, please, see messages higher to see more information");
                this.CreateHTMLPage("nineth_error");
                this.webDriver.Dispose();
                return;
            }

            this.CreateHTMLPage("finish");

            string PS = this.webDriver.GetPageSource();
            string Ufile = $"pdf/{UserData.CustomerNameAndSurname}/Cita.Previa.{UserData.CustomerNameAndSurname.Replace(" ", ".")}.{DateTime.Now.ToShortDateString()}.html";

            if (!_f.CreateDirectory($"pdf/{UserData.CustomerNameAndSurname}"))
            {
                Logger.Error($"Cannot create directory for {UserData.CustomerNameAndSurname}");
                return;
            }

            if (!_f.Create(Ufile, PS))
            {
                Logger.Error($"Cannot create file {UserData.CustomerNameAndSurname}");
                return;
            }


            this.InvokeOnStepsControllerCitaStatusCallback(UserData, this.webDriver.GetDataFromPage("#justificanteFinal"));

            this.InvokeOnStepsControllerMailCallback(this.UserData.CustomerNameAndSurname, Ufile);

            this.webDriver.Dispose();
        }

        private bool DoFirstStep()
        {
            if (this.webDriver.SelectElementInList("#form", this.UserData.ProcedureRegion) && this.webDriver.ExecuteScript(DomFirstStepButton))
            {
                this.Logger.Info("OK! First step solved successfully!");
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DoSecondStep()
        {
            if (this.webDriver.SelectElementInList("#tramite", this.UserData.ProcedureName) && this.webDriver.ExecuteScript(DomFirstStepButton))
            {
                this.Logger.Info("OK! Second step solved successfully!");
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DoThirdStep()
        {
            if (this.webDriver.ExecuteScript(DomThirdStep))
            {
                this.Logger.Info("OK! Second step solved successfully!");
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DoFourthStep()
        {
            if (this.webDriver.InvokeMemberClick($"#{GetDocumentTypeId(this.UserData.DocumentType)}") &&
            this.webDriver.UpdateFieldData("#txtIdCitado", this.UserData.DocumentNumber) &&
            this.webDriver.UpdateFieldData("#txtDesCitado", this.UserData.CustomerNameAndSurname) &&
            this.webDriver.UpdateFieldData("#txtAnnoCitado", this.UserData.CustomerDateOfBirth.Year.ToString()) &&
            this.webDriver.SelectElementInList("#txtPaisNac", this.UserData.Country))
            {
                this.Logger.Info("OK! Second step solved successfully!");
                this.Logger.Info("Sending captcha to solving...");
                this._currentTask = this.recaptcharResolver.SendTaskToResolve();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DoFifthStep()
        {
            if (!this.webDriver.ElementExists("#btnSalir"))
            {
                return false;
            }
            var obj = new object[] { "btnEnviar" };
            if (!this.webDriver.ExecuteScript(DomClickButtonScript, obj))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool DoSixthStep()
        {
            bool sh = this.webDriver.ElementExists("#idSede");
            if (sh)
            {
                var obj = new object[] { "btnSiguiente" };
                if (this.webDriver.SelectElementInList("idSede", this.UserData.ProcedureCity, true, true) && this.webDriver.ExecuteScript(DomClickButtonScript, obj))
                {
                    return true;
                }
                else
                {
                    this.CreateHTMLPage("list_error");
                    this.Logger.Warning("Error in scripts executing");
                    return false;
                }
            }
            else
            {
                this.CreateHTMLPage("sixth_error");
                this.Logger.Warning("Any availible places on sita!");
                return false;
            }
        }

        private bool DoSeventhStep()
        {
            bool sh = this.webDriver.ElementExists("#txtTelefonoCitado");
            if (sh)
            {
                if (this.UserData.ProcedureName.Equals(MallorcaProcedure))
                {
                    if (this.webDriver.UpdateFieldData("#txtTelefonoCitado", this.UserData.CustomerPhoneNumber) && this.webDriver.UpdateFieldData("#emailUNO", this.UserData.CustomerEmail) && this.webDriver.UpdateFieldData("#emailDOS", this.UserData.CustomerEmail) && this.webDriver.UpdateFieldData("#txtObservaciones", this.UserData.ProcedureReason) && this.webDriver.ExecuteScript("return document.getElementById('btnSiguiente').click();"))
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    if (this.webDriver.UpdateFieldData("#txtTelefonoCitado", this.UserData.CustomerPhoneNumber) && this.webDriver.UpdateFieldData("#emailUNO", this.UserData.CustomerEmail) && this.webDriver.UpdateFieldData("#emailDOS", this.UserData.CustomerEmail) && this.webDriver.ExecuteScript("return document.getElementById('btnSiguiente').click();"))
                    {
                        return true;
                    }
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        private bool DoEightStep()
        {
            bool sh = this.webDriver.ElementExists("$//*[@id='cita_1']/input");
            if (sh)
            {
                this.webDriver.InvokeMemberClick("$//*[@id='cita_1']/input");
                if (this.webDriver.ExecuteScript("return document.getElementById('btnSiguiente').click();") && this.webDriver.SolveAlertWindow())
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
            if (this.webDriver.InvokeMemberClick("#cookie_action_close_header") && this.webDriver.ExecuteScript(DomNinethStepCheckbox1) && this.webDriver.ExecuteScript(DomNinethStepCheckbox2) && this.webDriver.ExecuteScript(DomNinethStepBtn))
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
            var obj = new object[] { solution };
            if (this.webDriver.ExecuteScript(DomCaptchaSolutionPath, obj) && this.webDriver.ExecuteScript(DomCaptchaSuccessScript))
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
            string PS = this.webDriver.GetPageSource();
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
    }
}
