using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Sede_Checker.Entities.Settings;
using Sede_Checker.Entities.Settings.BrowserDrivers.Chrome;
using Sede_Checker.Entities.Settings.Controllers;
using Sede_Checker.Entities.Settings.Mail;
using Sede_Checker.Entities.Settings.Scripts;
using Sede_Checker.Implementation.Driver.Enums;

namespace Sede_Checker
{
    internal static class Program
    {
        /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main(InitializeSettings()));
            Environment.Exit(Environment.ExitCode);
        }

        private static SeaSettings InitializeSettings()
        {
            //todo: move it to json storage -> implement DTO entities & converters
            
            var ss = new SeaSettings
            {
                DefaultAppDir = Application.StartupPath,
                DefaultAppDataDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Sede\\",

                SitaUrl = "https://sede.administracionespublicas.gob.es/icpplus/index.html",
                SitaCaptchaUrl = "https://sede.administracionespublicas.gob.es/icpplus/acEntrada",

                RuCaptchaApiKey = "6Ld3FzoUAAAAANGzDQ-ZfwyAArWaG2Ae15CGxkKt",
                RucaptchaSuccessAction = "reportgood",
                RucaptchaBadAction = "reportbad",

                TaskProccesingMaxTimeout = 300,
                TaskProccesingMinDueTimeout = 150,

                ConstitutionTaskProcessingMaxTimeout =  14441,
                ConstitutionTaskProcessingMinDueTimeout = 14400,

                MailServiceSettings = new MailServiceSettings
                {
                    SenderName = "SEA PROJECT",
                    SenderEmail = "sea@vsolution.com.ua",
                    SmtpUseSsl = false,
                    SmtpServer = "mail.adm.tools",
                    SmtpPort = 25,
                    SmtpPassword = "gXOh4Plg157L52oI2foSGKc9",
                    SmtpUser = "sea@vsolution.com.ua",
                    DefaultSubject = "SEA PROJECT - FOUNDED CITA REPORT",
                    DefaultBodyTemplate = "<b>CONGRATULATIONS!</b> <br /><br /> <b>[REPORT ID]</b> {0} <br /> <b>[REPORT DATETIME]</b> {1} <br /> THE GREAT CITA WAS FOUND FOR <b>{2}</b>!",
                    ReportReceivers = new[]
                    {
                        new MailServiceReportReceiver {
                            Email = "arnezami.ua@gmail.com",
                            Description = "Vitalii Ivanov",
                            IsEnabled = true
                        },
                        new MailServiceReportReceiver {
                            Email = "Amazzzing1005@gmail.com",
                            Description = "Mariya Ponomareva",
                            IsEnabled = true
                        },
                        new MailServiceReportReceiver {
                            Email = "stadnichenko291@gmail.com",
                            Description = "Zachar Stadnichenko",
                            IsEnabled = true
                        }
                    }
                },
                ControllerSettings = new BaseControllerSettings
                {
                    ChromeDriverSettings = new ChromeDriverSettings
                    {
                        WindowState = BrowserWindowState.Normal,
                        //BrowserWindowsSize = new Size(),
                        CloseAlianPages = false,
                        DisableDefaultApps = false,
                        DisableInfoBars = true,
                        DisablePopupBlocking = false,
                        IgnoreCertificateErrors = false,
                        LaunchBrowserIncognitoMode = true,
                        PageLoadTimeout = 60,
                        UseProxy = true,
                        //IsHeadless = true,
                    }
                },
                SedeScripts = new ScriptsSettings {
                    Scripts = new Dictionary<string, string>()
                }
            };

            ss.SedeScripts.Scripts.Add("DomButtonClickAction", $"{ss.DefaultAppDir}\\Scripts\\Sede\\SitaButtonClickAction.js");
            ss.SedeScripts.Scripts.Add("SitaCalendarDateSelector", $"{ss.DefaultAppDir}\\Scripts\\Sede\\SitaCalendarDateSelector.js");
            ss.SedeScripts.Scripts.Add("SitaGoogleCapthcaSolver", $"{ss.DefaultAppDir}\\Scripts\\Sede\\SitaGoogleCaptchaSolver.js");
            ss.SedeScripts.Scripts.Add("SitaRadioDateSelector", $"{ss.DefaultAppDir}\\Scripts\\Sede\\SitaRadioDateSelector.js");
            ss.SedeScripts.Scripts.Add("SitaListSelector", $"{ss.DefaultAppDir}\\Scripts\\Sede\\SitaListSelector.js");

            return ss;
        }
    }
}