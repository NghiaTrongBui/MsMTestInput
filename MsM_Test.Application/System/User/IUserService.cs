using MsM_Test.Data.Entities;
using MsM_Test.ViewModels.System.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MsM_Test.ViewModels.Common;

namespace MsM_Test.Application.System.User
{
    public interface IUserService
    {
        Task<ApiRespond<string>> Authenticate(LoginRequest request);

        Task<ApiRespond<bool>> Register(RegisterRequest request);

        Task<List<UserViewManager>> GetAllUser();

        Task<ApiRespond<UserViewManager>> GetUserByID(Guid id);

        Task<bool> UpdateUser(UserRequestUpdate user);
    }
}
