using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PX.Core.Ultilities
{
    class MailUtilities
    {
        private readonly MailSettingsViewModel _mailSettings;

        public MailUtilities(EmailSetting mailSettings)
        {
            _mailSettings = new MailSettingsViewModel(mailSettings);
        }

        public void SendEmail(string from, List<string> to, bool isHtml, string subject, string body)
        {
            SendEmail(from, to.ToList(), null, null, isHtml, subject, body);
        }

        public void SendEmail(string from, string[] to, bool isHtml, string subject, string body)
        {
            SendEmail(from, to.ToList(), null, null, isHtml, subject, body);
        }

        public void SendEmail(string from, string to, string subject, string body)
        {
            SendEmail(from, new[] { to }, null, null, false, subject, body);
        }

        public void SendEmail(string from, string[] to, string cc, string bcc, bool isHtml, string subject, string body)
        {
            SendEmail(from, to.ToList(), cc, bcc, isHtml, subject, body);
        }

        public void SendEmail(string from, List<string> to, string cc, string bcc, bool isHtml, string subject, string body)
        {
            if (!_mailSettings.HasSettings) return;

            var mail = new MailMessage();
            var client = new SmtpClient
            {
                Port = _mailSettings.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = _mailSettings.Host,
                EnableSsl = _mailSettings.EnableSsl,
                Timeout = _mailSettings.Timeout,
                Credentials = new NetworkCredential(_mailSettings.User, _mailSettings.Password)
            };

            mail.From = new MailAddress(from);
            foreach (var s in to)
            {
                mail.To.Add(s);
            }

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isHtml;
            // TODO: Send email using thread + queue
            Task.Run(() => client.Send(mail));
        }
    }
    public class MailSettingsViewModel
    {
        public MailSettingsViewModel()
        {

        }

        public MailSettingsViewModel(EmailSetting mailSettings)
        {
            Host = mailSettings.Host;
            Port = mailSettings.Port;
            EnableSsl = mailSettings.EnableSsl;
            Timeout = mailSettings.Timeout;
            User = mailSettings.User;
            Password = mailSettings.Password;
            HasSettings = true;
        }

        #region Public Properties

        public bool HasSettings { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool EnableSsl { get; set; }

        public int Timeout { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        #endregion
    }

    public class EmailSetting
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public bool EnableSsl { get; set; }

        public int Timeout { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }
}
