//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Sede_Checker.Abstract.Interfaces;
//using Sede_Checker.Delegates;
//using Sede_Checker.Entities.DTO;
//using Sede_Checker.Implementation.Driver.Chrome;
//using Sede_Checker.Implementation.Services.FileService;
//using Sede_Checker.TaskFormParams.Properties;

//namespace Sede_Checker.Implementation.Miners
//{
//    public abstract class SeaBaseCitaMiner
//    {
//        private IRecaptchaV2ResolverTask _currentTask;

//        private ILogger Logger;

//        private IFileSystemService _f;

//        public IRecaptchaV2Resolver recaptcharResolver;

//        public IWebDriverResolver webDriver;

//        public SedeTaskData UserData;
        
//        private string MallorcaProcedure;

//        private SedeCheckerProxyAdressDTO Adress;

//        public event StepsControllerMailCallback OnStepsControllerMailCallback;

//        public event StepsControllerCitaStatusCallback OnStepsControllerCitaStatusCallback;

//        protected SeaBaseCitaMiner(SedeTaskData user, SedeCheckerProxyAdressDTO adress, ILogger logger, IRecaptchaV2Resolver recaptchaV2Resolver) : base()
//        {
//            this.UserData = user;

//            this.Logger = logger;

//            this.Adress = adress;

//            _f = new FileService(Logger);

//            recaptcharResolver = recaptchaV2Resolver;
//            //recaptcharResolver.OnRecaptchaV2TaskCallback += this.RecaptcharResolver_OnRecaptchaV2TaskCallback;
//        }

//        private void InvokeOnStepsControllerMailCallback(string nameSurname, string attachment = "")
//        {
//            if (ReferenceEquals(OnStepsControllerMailCallback, null)) return;
//            this.OnStepsControllerMailCallback(this, new StepsControllerMailCallbackEventArgs(nameSurname, attachment));
//        }

//        private void InvokeOnStepsControllerCitaStatusCallback(SedeTaskData data, string citaNumber)
//        {
//            if (ReferenceEquals(OnStepsControllerCitaStatusCallback, null)) return;

//            this.OnStepsControllerCitaStatusCallback(this, new StepsControllerCitaStatusCallbackEventArgs(data, citaNumber));
//        }

//        public async void SolveStepsAsync()
//        {
//            await Task.Run(() =>
//            {
//                try
//                {
//                    this.SolveSteps();
//                }
//                catch (Exception e)
//                {
//                    Logger.Exception(e);
//                }
//                finally
//                {
//                    Dispose();
//                }
//            });
//        }

//        public void SolveSteps()
//        {
//            webDriver = new ChromeDriverResolver(SitaUrl, this.Adress, this.Logger);

//            if (!webDriver.Initialize())
//            {
//                this.Logger.Error("ChromeWebDriver can't be initialized -> Stop task!");
//                return;
//            }

//            if (!this.DoFirstStep())
//            {
//                this.Logger.Error("First step can't be solved, please, see messages higher to see more information");
//                this.webDriver.Dispose();
//                return;
//            }

//            if (!this.DoSecondStep())
//            {
//                this.webDriver.Dispose();
//                this.Logger.Error("Second step can't be solved, please, see messages higher to see more information");
//                return;
//            }

//            if (!this.DoThirdStep())
//            {
//                this.Logger.Error("Third step can't be solved, please, see messages higher to see more information");
//                this.webDriver.Dispose();
//                return;
//            }

//            if (!this.DoFourthStep())
//            {
//                this.Logger.Error("Fourth step can't be solved, please, see messages higher to see more information");
//                this.webDriver.Dispose();
//                return;
//            }

//            for (var i = 0; i < 200; i++)
//            {
//                if (!ReferenceEquals(this._currentTask, null))
//                {
//                    switch (this._currentTask.Status)
//                    {
//                        case Captcha.RuCaptcha.Enums.RuCaptchaTaskStatus.Unknow:
//                            break;
//                        case Captcha.RuCaptcha.Enums.RuCaptchaTaskStatus.NotReady:
//                            break;
//                        case Captcha.RuCaptcha.Enums.RuCaptchaTaskStatus.Solved:
//                            this.FinishSteps(this._currentTask.CaptchaSolution);
//                            return;
//                        case Captcha.RuCaptcha.Enums.RuCaptchaTaskStatus.Unsolvable:
//                            return;
//                        default:
//                            return;
//                    }
//                }
//                Logger.Info($"{i + 1} attemp to check captcha solution...");
//                Thread.Sleep(5000);
//            }
//        }

//        private void FinishSteps(string solution)
//        {
//            this.Logger.Info("Response from recaptcha ready... Finishing fourth step...");
//            if (!this.FinishFourthStep(solution))
//            {
//                this.Logger.Info("Fourth step can't be solved, please, see messages higher to see more information");
//                this.webDriver.Dispose();
//                return;
//            }
//            this.Logger.Info("Fourth step finished... Completing fifth step...");
//            if (!this.DoFifthStep())
//            {
//                this.Logger.Info("Fifth step can't be solved, please, see messages higher to see more information");
//                this.CreateHtmlPage("fifth_error");
//                this.webDriver.Dispose();
//                return;
//            }
//            this.Logger.Info("fifth step finished... Completing sixth step...");
//            if (!this.DoSixthStep())
//            {
//                this.Logger.Error("Sixth step can't be solved, please, see messages higher to see more information");
//                this.CreateHtmlPage("sixth_error");
//                this.webDriver.Dispose();
//                return;
//            }
//            this.Logger.Info("Sixth step finished... Completing Seventh step...");
//            if (!this.DoSeventhStep())
//            {
//                this.Logger.Error("Seventh step can't be solved, please, see messages higher to see more information");
//                this.CreateHtmlPage("seventh_error");
//                this.webDriver.Dispose();
//                return;
//            }
//            this.Logger.Info("Seventh step finished... Completing Eight step...");
//            if (!this.DoEightStep())
//            {
//                this.Logger.Error("Eight step can't be solved, please, see messages higher to see more information");
//                this.CreateHtmlPage("eight_error");
//                this.webDriver.Dispose();
//                return;
//            }
//            this.Logger.Info("Eight step finished... Completing Nineth step...");
//            if (!this.DoNinethStep())
//            {
//                this.Logger.Error("Nineth step can't be solved, please, see messages higher to see more information");
//                this.CreateHtmlPage("nineth_error");
//                this.webDriver.Dispose();
//                return;
//            }

//            this.CreateHtmlPage("finish");

//            string PS = this.webDriver.GetPageSource();
//            string Ufile = $"pdf/{UserData.CustomerNameAndSurname}/Cita.Previa.{UserData.CustomerNameAndSurname.Replace(" ", ".")}.{DateTime.Now.ToShortDateString()}.html";

//            if (!_f.CreateDirectory($"pdf/{UserData.CustomerNameAndSurname}"))
//            {
//                Logger.Error($"Cannot create directory for {UserData.CustomerNameAndSurname}");
//                return;
//            }

//            if (!_f.Create(Ufile, PS))
//            {
//                Logger.Error($"Cannot create file {UserData.CustomerNameAndSurname}");
//                return;
//            }


//            this.InvokeOnStepsControllerCitaStatusCallback(UserData, this.webDriver.GetDataFromPage("#justificanteFinal"));

//            this.InvokeOnStepsControllerMailCallback(this.UserData.CustomerNameAndSurname, Ufile);

//            this.webDriver.Dispose();
//        }

//        private bool DoFirstStep()
//        {
//            if (this.webDriver.SelectElementInList("#form", this.UserData.ProcedureRegion) && this.webDriver.ExecuteScript(DomFirstStepButton))
//            {
//                this.Logger.Info("OK! First step solved successfully!");
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        private bool DoSecondStep()
//        {
//            if (this.webDriver.SelectElementInList("#tramite", this.UserData.ProcedureName) && this.webDriver.ExecuteScript(DomFirstStepButton))
//            {
//                this.Logger.Info("OK! Second step solved successfully!");
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        private bool DoThirdStep()
//        {
//            if (this.webDriver.ExecuteScript(DomThirdStep))
//            {
//                this.Logger.Info("OK! Second step solved successfully!");
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        private bool DoFourthStep()
//        {
//            if (this.UserData.ProcedureName.Equals(MallorcaProcedure))
//            {
//                if (this.webDriver.InvokeMemberClick($"#{GetDocumentTypeId(this.UserData.DocumentType)}") &&
//                    this.webDriver.UpdateFieldData("#txtIdCitado", this.UserData.DocumentNumber) &&
//                    this.webDriver.UpdateFieldData("#txtDesCitado", this.UserData.CustomerNameAndSurname) &&
//                    this.webDriver.UpdateFieldData("#txtAnnoCitado", this.UserData.CustomerDateOfBirth.Year.ToString()))
//                {
//                    this.Logger.Info("OK! Second step solved successfully!");
//                    this.Logger.Info("Sending captcha to solving...");
//                    this._currentTask = this.recaptcharResolver.SendTaskToResolve();
//                    return true;
//                }
//                else
//                {
//                    return false;
//                }
//            }
//            else
//            {
//                if (this.UserData.TieExpiredDate.ToShortDateString() != "01.01.0001")
//                {
//                    if (this.webDriver.InvokeMemberClick($"#{GetDocumentTypeId(this.UserData.DocumentType)}") && this.webDriver.UpdateFieldData("#txtIdCitado", this.UserData.DocumentNumber) && this.webDriver.UpdateFieldData("#txtDesCitado", this.UserData.CustomerNameAndSurname) && this.webDriver.UpdateFieldData("#txtFecha", this.UserData.TieExpiredDate.ToShortDateString().Replace(".", "/")) && this.webDriver.SelectElementInList("#txtPaisNac", this.UserData.Country))
//                    {
//                        this.Logger.Info("OK! Second step solved successfully!");
//                        this.Logger.Info("Sending captcha to solving...");
//                        this._currentTask = this.recaptcharResolver.SendTaskToResolve();
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                }
//                else
//                {
//                    if (this.webDriver.InvokeMemberClick($"#{GetDocumentTypeId(this.UserData.DocumentType)}") && this.webDriver.UpdateFieldData("#txtIdCitado", this.UserData.DocumentNumber) && this.webDriver.UpdateFieldData("#txtDesCitado", this.UserData.CustomerNameAndSurname) && this.webDriver.SelectElementInList("#txtPaisNac", this.UserData.Country))
//                    {
//                        this.Logger.Info("OK! Second step solved successfully!");
//                        this.Logger.Info("Sending captcha to solving...");
//                        this._currentTask = this.recaptcharResolver.SendTaskToResolve();
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                }

//            }
//        }

//        private bool DoFifthStep()
//        {
//            if (!this.webDriver.ElementExists("#btnSalir")) return false;

//            //don't change it to var -> driver doesn't recognize it

//            object[] obj = { "btnEnviar" };

//            return this.webDriver.ExecuteScript(DomClickButtonScript, obj);
//        }

//        private bool DoSixthStep()
//        {
//            bool sh = this.webDriver.ElementExists("#idSede");

//            if (sh)
//            {
//                //don't change it to var -> driver doesn't recognize it

//                object[] obj = new object[] { "btnSiguiente" };

//                if (this.webDriver.SelectElementInList("idSede", this.UserData.ProcedureCity, true) &&
//                    this.webDriver.ExecuteScript(DomClickButtonScript, obj))
//                {
//                    return true;
//                }
//                else
//                {
//                    this.CreateHtmlPage("list_error");
//                    this.Logger.Warning("Error in scripts executing");
//                    return false;
//                }
//            }
//            else
//            {
//                this.CreateHtmlPage("sixth_error");
//                this.Logger.Warning("Any availible places on sita!");
//                return false;
//            }
//        }

//        private bool DoSeventhStep()
//        {
//            bool sh = this.webDriver.ElementExists("#txtTelefonoCitado");

//            if (sh)
//            {
//                if (this.UserData.ProcedureName.Equals(MallorcaProcedure))
//                {
//                    if (this.webDriver.UpdateFieldData("#txtTelefonoCitado", this.UserData.CustomerPhoneNumber) &&
//                        this.webDriver.UpdateFieldData("#emailUNO", this.UserData.CustomerEmail) &&
//                        this.webDriver.UpdateFieldData("#emailDOS", this.UserData.CustomerEmail) &&
//                        this.webDriver.UpdateFieldData("#txtObservaciones", this.UserData.ProcedureReason) &&
//                        this.webDriver.ExecuteScript("return document.getElementById('btnSiguiente').click();"))
//                    {
//                        return true;
//                    }
//                    return false;
//                }
//                else
//                {
//                    if (this.webDriver.UpdateFieldData("#txtTelefonoCitado", this.UserData.CustomerPhoneNumber) &&
//                        this.webDriver.UpdateFieldData("#emailUNO", this.UserData.CustomerEmail) &&
//                        this.webDriver.UpdateFieldData("#emailDOS", this.UserData.CustomerEmail) &&
//                        this.webDriver.ExecuteScript("return document.getElementById('btnSiguiente').click();"))
//                    {
//                        return true;
//                    }
//                    return false;
//                }
//            }
//            else
//            {
//                return false;
//            }
//        }

//        private bool DoEightStep()
//        {
//            if (this.UserData.ProcedureName.Equals(MallorcaProcedure))
//            {
//                bool s = this.webDriver.ElementExists("#VistaMapa_Datatable");
//                if (s)
//                {
//                    if (this.webDriver.ExecuteScript(DomIlesBalearsMallorcaProcedureDate) && this.webDriver.SolveAlertWindow())
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                }
//                else
//                {
//                    return false;
//                }
//            }
//            else
//            {
//                bool sh = this.webDriver.ElementExists("$//*[@id='cita_1']/input");
//                if (sh)
//                {
//                    if (this.webDriver.InvokeMemberClick("$//*[@id='cita_1']/input") && this.webDriver.ExecuteScript("return document.getElementById('btnSiguiente').click();") && this.webDriver.SolveAlertWindow())
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                }
//                else
//                {
//                    return false;
//                }
//            }
//        }

//        private bool DoNinethStep()
//        {
//            if (this.webDriver.InvokeMemberClick("#cookie_action_close_header") && this.webDriver.ExecuteScript(DomNinethStepCheckbox1) && this.webDriver.ExecuteScript(DomNinethStepCheckbox2) && this.webDriver.ExecuteScript(DomNinethStepBtn))
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        private bool FinishFourthStep(string solution)
//        {
//            object[] obj = { solution };

//            if (this.webDriver.ExecuteScript(DomCaptchaSolutionPath, obj) &&
//                this.webDriver.ExecuteScript(DomCaptchaSuccessScript))
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        private void CreateHtmlPage(string fileName)
//        {
//            string ps = this.webDriver.GetPageSource();
//            string dir = $"pages/{UserData.CustomerNameAndSurname}";

//            _f.CreateDirectory(dir);
//            _f.Create($"{dir}/{fileName}.html", ps);
//        }

//        private string GetDocumentTypeId(DocumentType type)
//        {
//            switch (type)
//            {
//                case DocumentType.PASSPORT:
//                    return "rdbTipoDocPas";
//                case DocumentType.NIE:
//                    return "rdbTipoDocNie";
//                case DocumentType.DNI:
//                    return "rdbTipoDocDni";
//                default:
//                    return String.Empty;
//            }
//        }

//        public void Dispose()
//        {
//            webDriver?.Dispose();
//        }
//    }
//}