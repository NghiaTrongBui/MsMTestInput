using eShopSolution.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MsM_Test.Data.Entities;
using MsM_Test.ViewModels.Common;
using MsM_Test.ViewModels.System.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MsM_Test.Application.System.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, IConfiguration config,
            RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _roleManager = roleManager;
        }

        public async Task<ApiRespond<string>> Authenticate(LoginRequest request)
        {
            //find user name in db
            var user = await _userManager.FindByNameAsync(request.UserName);
            if(user == null) 
            { return new ApiRespond<string>
                {
                    IsSucceed = false,
                    Messeage = "Account is not exist"
                }; 
            }

            //check password
            var result = await _signInManager.PasswordSignInAsync(user, request.Passsword, request.RememberMe, true);
            if(!result.Succeeded) 
            { return new ApiRespond<string> 
                { 
                    IsSucceed = false,
                    Messeage = "User name or Password wrong"
                }; 
            }

            // kiem tra role va y/c nhap them
            
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            
            //token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new ApiRespond<string> {
                IsSucceed = true,
                RespondObj = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public async Task<ApiRespond<bool>> Register(RegisterRequest request)
        {
            if(await _userManager.FindByNameAsync(request.UserName) != null)
            {
                return new ApiRespond<bool>
                {
                    IsSucceed = false,
                    Messeage = "User name is exist"
                };
            }

            if(await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiRespond<bool>
                {
                    IsSucceed = false,
                    Messeage = "Email is exist"
                };
            }

            var user = new AppUser()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dob = request.Dob,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded) 
            {
                new ApiRespond<bool>
                {
                    IsSucceed = true,
                    RespondObj = true //chuyen sang trang nao do
                };
            }

            return new ApiRespond<bool>
            {
                IsSucceed = false,
                Messeage = "Error server"
            };
        }

        public async Task<List<UserViewManager>> GetAllUser()
        {
            var Result = await _userManager.Users
                        .Select(s => new UserViewManager()
                        {   Id  = s.Id,
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            PhoneNumber = s.PhoneNumber,
                            UserName = s.UserName,
                            Email = s.Email,
                            Dob = s.Dob
                        }
                        ).ToListAsync();

            return Result;
        }

        public async Task<ApiRespond<UserViewManager>> GetUserByID(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            
            if(user == null)
            {
                return new ApiRespond<UserViewManager>
                {
                    IsSucceed = false,
                    Messeage = "Account is not exist"
                };
            }

            var result = new UserViewManager()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Email = user.Email,
                Dob = user.Dob
            };

            return new ApiRespond<UserViewManager>
            {
                IsSucceed = true,
                RespondObj = result
            };
        }

        public Task<bool> UpdateUser(UserRequestUpdate user)
        {
            return null;
        }
    }
}
