using IdentityServerAccountJwt.Client.Pages;
using IdentityServerAccountJwt.Shared.Dtos;
using IdentityServerAccountJwt.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerAccountJwt.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DasboardController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DasboardController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetDashboardUsers()
        {
            var dashboard = new DashboardData {
            UsersActiveCount=await _userManager.Users.CountAsync(ic=>ic.IsActive==true),
             UsersInActiveCount=await _userManager.Users.CountAsync(ic=>ic.IsActive==false)
            };
return Ok(dashboard);
        }
    }
}
