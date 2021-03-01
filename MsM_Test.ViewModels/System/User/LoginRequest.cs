using System;
using System.Collections.Generic;
using System.Text;

namespace MsM_Test.ViewModels.System.User
{
    public class LoginRequest
    {
        public string UserName { get; set; }

        public string Passsword { get; set; }

        public bool RememberMe { get; set; }
    }
}
