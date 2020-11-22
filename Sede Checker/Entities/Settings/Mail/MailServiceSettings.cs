namespace Sede_Checker.Entities.Settings.Mail
{
    public class MailServiceSettings
    {
        public string SenderName { set; get; }
        public string SenderEmail { set; get; }
        public string SmtpServer { set; get; }
        public string SmtpUser { set; get; }
        public string SmtpPassword { set; get; }
        public int SmtpPort { set; get; }
        public bool SmtpUseSsl { set; get; }
        public MailServiceReportReceiver[] ReportReceivers { set; get; }
        public string DefaultSubject { set; get; }
        public string DefaultBodyTemplate { set; get; }
    }
}