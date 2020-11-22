using Sede_Checker.Entities.Settings.Controllers;
using Sede_Checker.Entities.Settings.Mail;
using Sede_Checker.Entities.Settings.Scripts;

namespace Sede_Checker.Entities.Settings
{
    public class SeaSettings : BaseSettings
    {
        public int TaskProccesingMinDueTimeout { set; get; }
        public int TaskProccesingMaxTimeout { set; get; }
        public int ConstitutionTaskProcessingMinDueTimeout { get; set; }
        public int ConstitutionTaskProcessingMaxTimeout { get; set; }
        public MailServiceSettings MailServiceSettings { set; get; }
        public BaseControllerSettings ControllerSettings { set; get; }
        public string DefaultAppDir { get; set; }
        public string DefaultAppDataDir { get; set; }
        public string SitaUrl { get; set; }
        public string SitaCaptchaUrl { get; set; }
        public string RuCaptchaApiKey { get; set; }
        public string RucaptchaSuccessAction { get; set; }
        public string RucaptchaBadAction { get; set; }
        public ScriptsSettings SedeScripts { get; set; }
    }
}