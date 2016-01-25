using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SoftwareContable
{
    public enum UserRole
    {
        Unkown = 0,

        [Description("Usuario")]
        User,

        [Description("Administrador")]
        Administrator
    }
}