using System;
using System.Web.Configuration;

namespace SoftwareContable.Utilities
{
    /// <summary>
    /// Provides a set of properties representing each setting included in the web.config. These properties are cached by implementing the Singleton design pattern.
    /// </summary>
    public sealed class SettingsManager
    {
        #region Fields

        /// <summary>
        /// Stores the single one instance of <see cref="SettingsManager"/>.
        /// </summary>
        private static SettingsManager _instace;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the single one instance of <see cref="SettingsManager"/>.
        /// </summary>
        public static SettingsManager Instance
        {
            get
            {
                _instace = _instace ?? new SettingsManager();

                return _instace;
            }
        }

        /// <summary>
        /// Gets the value of the DefaultUserRoleDbId setting.
        /// </summary>
        public int DefaultUserRoleDbId
        {
            private set;
            get;
        }

        /// <summary>
        /// Gets the value of the AdministratorUserRoleDbId setting.
        /// </summary>
        public int AdministratorUserRoleDbId
        {
            private set;
            get;
        }

        /// <summary>
        /// Gets the value of the SMTPServer setting.
        /// </summary>
        public string SmtpServer
        {
            private set;
            get;
        }

        /// <summary>
        /// Gets the value of the SMTPEmail setting.
        /// </summary>
        public string SmtpEmail
        {
            private set;
            get;
        }

        /// <summary>
        /// Gets the value of the SMTPPassword setting.
        /// </summary>
        public string SmtpPassword
        {
            private set;
            get;
        }

        /// <summary>
        /// Gets the value of the NewUserTemplateFilePath setting.
        /// </summary>
        public string NewUserTemplateFilePath
        {
            private set;
            get;
        }

        /// <summary>
        /// Gets the value of the NewUserAuthorizationSubject setting.
        /// </summary>
        public string NewUserAuthorizationSubject
        {
            private set;
            get;
        }

        /// <summary>
        /// Gets the value of the NewReportTemplateFilePath setting.
        /// </summary>
        public string NewReportTemplateFilePath
        {
            private set;
            get;
        }

        /// <summary>
        /// Gets the value of the NewReportSubject setting.
        /// </summary>
        public string NewReportSubject
        {
            private set;
            get;
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes the single one instance by setting properties.
        /// </summary>
        private SettingsManager()
        {
            DefaultUserRoleDbId = GetValue<int>("DefaultUserRoleDbId");
            AdministratorUserRoleDbId = GetValue<int>("AdministratorUserRoleDbId");
            SmtpServer = GetValue<string>("SMTPServer");
            SmtpEmail = GetValue<string>("SMTPEmail");
            SmtpPassword = GetValue<string>("SMTPPassword");
            NewUserTemplateFilePath = GetValue<string>("NewUserTemplateFilePath");
            NewUserAuthorizationSubject = GetValue<string>("NewUserAuthorizationSubject");
            NewReportTemplateFilePath = GetValue<string>("NewReportTemplateFilePath");
            NewReportSubject = GetValue<string>("NewReportSubject");
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Gets the value of a setting specified by the <see cref="key"/>.
        /// </summary>
        /// <typeparam name="T">The type of the setting.</typeparam>
        /// <param name="key">The <see cref="key"/> assigned to the setting.</param>
        /// <returns>Returns the value of the seeting.</returns>
        private static T GetValue<T>(string key)
        {
            var value = WebConfigurationManager.AppSettings[key];

            return (T)Convert.ChangeType(value, typeof(T));
        }

        #endregion Methods
    }
}