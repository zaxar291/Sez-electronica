using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Sede_Checker.Abstract.Interfaces;
using Sede_Checker.Entities.Settings.Mail;

namespace Sede_Checker.Implementation.Services.MailService
{
    public class MailService : IPostMailer
    {
        private readonly ILogger _l;
        private readonly MailServiceSettings _settings;

        public MailService(ILogger l, MailServiceSettings settings)
        {
            _l = l;
            _settings = settings;
        }

        public bool SendReport(string customerName, string[] attachments = null)
        {
            _l.Info("Let's to send reports...");

            foreach (var r in _settings.ReportReceivers.Where(el => el.IsEnabled))
            {
                _l.Info($"Sending report for {r.Description} -> {r.Email}...");

                var body = string.Format(_settings.DefaultBodyTemplate, Guid.NewGuid(), DateTime.Now, customerName.ToUpper());
                
                var sr = SendMail(_settings.SenderName, r.Email, _settings.DefaultSubject, body, attachments);

                if (sr)
                    _l.Info($"Report was successfully send to {r.Description} -> {r.Email}...");
                else
                    _l.Error(
                        $"While trying to send report to {r.Description} -> {r.Email}, error occured! More details in exception...");
            }
            return true;
        }

        private bool SendMail(string from, string to, string subject, string body, string[] attachments = null)
        {
            try
            {
                var ma = new MailAddress(_settings.SmtpUser, from);

                var toma = new MailAddress(to);
                var m = new MailMessage(ma, toma)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                if (!ReferenceEquals(attachments, null) && !attachments.Length.Equals(0))
                    foreach (var a in attachments)
                    {
                        if (string.IsNullOrEmpty(a)) {
                            this._l.Warning("Attachment is empty, skip it...");
                            continue;
                        }

                        if (!File.Exists(a)) {
                            this._l.Warning($"File {a} are not exist! Skip it...");
                            continue;
                        }

                        m.Attachments.Add(new Attachment(a));
                    }

                var smtp = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort)
                {
                    Credentials = new NetworkCredential(_settings.SmtpUser, _settings.SmtpPassword),
                    EnableSsl = _settings.SmtpUseSsl
                };
                smtp.Send(m);
                return true;
            }
            catch (SmtpException e)
            {
                _l.Error($"Error occured, cannot send message to {to}. Error message: {e.Message}");
                return false;
            }
        }
    }
}