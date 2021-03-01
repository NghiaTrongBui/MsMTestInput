using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsM_Test.Application.System.User;
using MsM_Test.ViewModels.System.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsM_Test.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(/*[FromForm]*/ LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Authenticate(request);
            if(!result.IsSucceed)
            {
                return BadRequest(result.Messeage);
            }
            return Ok(new { token = result.RespondObj }) ;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(/*[FromForm]*/ RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Register(request);
            if (!result.IsSucceed)
            {
                return BadRequest(result.Messeage);
            }
            return Ok();
        }

        [HttpPost("userpage")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userService.GetAllUser();
            if(!users.Any()) 
            {
                return BadRequest("No account");
            }
            return Ok(users);
        }

        [HttpPost("getuserpage")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserByID(/*[FromForm]*/ Guid id)
        {
            var result = await _userService.GetUserByID(id);

            if(result == null)
            {
                return BadRequest(result.Messeage);
            }

            return Ok(result.RespondObj);
        }

        //[HttpPut("{request.Id}/updateuser")]
        [HttpPost("/admin/updateuser")]
        public async Task<IActionResult> UpdateUser(UserRequestUpdate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.UpdateUser(request);
            if (!result.IsSucceed)
            {
                return BadRequest(result.Messeage);
            }
            return Ok(result);
        }

        //[HttpPut("{request.UserId}/roles")]
        [HttpPost("/admin/roles")]
        public async Task<IActionResult> UpdateUserRole(/*[FromForm]*/UserRoleUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.UpdateUserRole(request);
            if (!result)
            {
                return BadRequest("Account is not exist");
            }
            return Ok(result);
        }

        [HttpPost("/admin/deleteuser")]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.DeleteUser(Id);
            if (!result.IsSucceed)
            {
                return BadRequest(result.Messeage);
            }
            return Ok(result);
        }

    }
}
