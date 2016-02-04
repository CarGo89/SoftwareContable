using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftwareContable.Utilities
{
    /// <summary>
    /// Provides abstraction to send emails.
    /// </summary>
    public interface IEmailSender
    {
        #region Methods

        /// <summary>
        /// Sens an email to a SMTP server.
        /// </summary>
        /// <param name="from">The address of the sender.</param>
        /// <param name="recipients">The address of the recipients.</param>
        /// <param name="subject">The subject text.</param>
        /// <param name="htmlBody">The message body</param>
        /// <returns>A task object representing the asynchronous operation.</returns>
        Task Send(string from, IEnumerable<string> recipients, string subject, string htmlBody);

        #endregion Methods
    }
}