using MsM_Test.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsM_Test.ViewModels.System.User
{
    public class UserRoleUpdateRequest
    {
        public Guid UserId { get; set; }

        public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
    }
}
