using MsM_Test.ViewModels.System.Role;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MsM_Test.Application.System.Role
{
    public interface IRoleService
    {
        Task<List<RoleViewManger>> GetAll();
    }
}
