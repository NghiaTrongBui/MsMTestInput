using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsM_Test.Application.System.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsM_Test.WebAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public IActionResult Index()
        {
            return View();
        }

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("rolemanager")]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAll();
            return Ok(roles);
        }
    }
}
