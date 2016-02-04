using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SoftwareContable.Utilities
{
    /// <summary>
    /// Provides implementation of <see cref="IEmailSender"/>.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        #region Fields

        /// <summary>
        /// Stores the SMTP server.
        /// </summary>
        private readonly string _smtpServer;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="EmailSender"/> by using the specified SMTP server.
        /// </summary>
        /// <param name="smtpServer">The name of the host computer used for SMTP transactions.</param>
        public EmailSender(string smtpServer)
        {
            _smtpServer = smtpServer;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Sens an email to a SMTP server.
        /// </summary>
        /// <param name="from">The address of the sender.</param>
        /// <param name="recipients">The address of the recipients.</param>
        /// <param name="subject">The subject text.</param>
        /// <param name="htmlBody">The message body</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        public async Task Send(string from, IEnumerable<string> recipients, string subject, string htmlBody)
        {
            using (var smtpClient = new SmtpClient(_smtpServer))
            {
                var flatRecipients = string.Join(",", recipients);

                smtpClient.EnableSsl = false;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(SettingsManager.Instance.SmtpEmail, SettingsManager.Instance.SmtpPassword);

                using (var message = new MailMessage(from, flatRecipients, subject, htmlBody))
                {
                    message.IsBodyHtml = true;

                    await smtpClient.SendMailAsync(message).ConfigureAwait(false);
                }
            }
        }

        #endregion Methods
    }
}