using System;
using System.Security.Principal;
using System.Web;
using AutoMapper;
using Dpr.Core.DataAccess;
using SoftwareContable.DataAccess;
using SoftwareContable.Models;

namespace SoftwareContable.Utilities
{
    public static class LoggedInUserInfo
    {
        #region Fields

        /// <summary>
        /// Stores the user information.
        /// </summary>
        private static IPrincipal _principalUser;

        private static readonly IRepository<DataAccess.Entities.User> UserRepository;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the user information.
        /// </summary>
        internal static IPrincipal PrincipalUser
        {
            set
            {
                _principalUser = value;

                Init();
            }
            get
            {
                if (_principalUser == null)
                {
                    _principalUser = HttpContext.Current.User;

                    Init();
                }

                return _principalUser;
            }
        }

        /// <summary>
        /// Gets the User Name.
        /// </summary>
        public static User User
        {
            get;
            private set;
        }

        #endregion Properties

        #region Constructors

        static LoggedInUserInfo()
        {
            UserRepository = FactoryRepository.Create<SoftwareContableDbContext, DataAccess.Entities.User>();
        }

        #endregion Constructors

        #region Methods

        private static void Init()
        {
            var dbUser = UserRepository
                .Single(user => string.Equals(user.UserName, PrincipalUser.Identity.Name, StringComparison.InvariantCultureIgnoreCase));

            User = Mapper.Map<User>(dbUser);
        }

        #endregion
    }
}