using IdentityServerAccountJwt.Shared.Pagination_Models;
using IdentityServerAccountJwt.Shared.Dtos;
using IdentityServerAccountJwt.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityServerAccountJwt.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IConfigurationSection _jwtSettings;
        public AccountsController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
            _jwtSettings = config.GetSection("Jwtsettings");
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRegister userRegister)
        {
            if (userRegister == null || !ModelState.IsValid)
                return BadRequest("The Entity Is Null");
            var user = new ApplicationUser
            {
                UserName = userRegister.UserName,
                Email = userRegister.Email,
                IsActive= userRegister.IsActive
            };
            var result = await _userManager.CreateAsync(user, userRegister.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new UserResponse { Errors = errors });

            }
           await _userManager.AddToRoleAsync(user, "Viewer");
            return StatusCode(201);

        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin login)
        {
            var user=await _userManager.FindByNameAsync(login.UserName);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(login.UserName);
            }
            if (user==null||!await _userManager.CheckPasswordAsync(user,login.Password))
            {
                return Unauthorized(new ResponseLogin { Errors = new[] {"Login Is Failed"}});
            }
            var GetSigningKey = GetSigningCredentials();
            var GetClaims =await GetClaimssss(user);
       var GetToken = GenerateTokenOptions(GetSigningKey,GetClaims);
            var Token = new JwtSecurityTokenHandler().WriteToken(GetToken);
            return Ok( new ResponseLogin { IsSuccessLogin=true,Token= Token});
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings["securityKey"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaimssss(ApplicationUser user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName)
    };
            var userroles = await _userManager.GetRolesAsync(user);
            foreach (var role in userroles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings["ValidIssure"],
                audience: _jwtSettings["ValidAudiance"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["ExpiredInMin"])),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var userlist = await _userManager.Users.ToListAsync();
            var user = userlist.Select(u => new ApplicationUser()
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Country = u.Country,
                UserRoles = string.Join(", ", _userManager.GetRolesAsync(u).Result.ToArray())
            });
            if (user == null)
                return NotFound("Users Not Found");
            return Ok(user);
        }
        [HttpGet]
        public async Task<IActionResult> GetUsersByName(string name)
        {
            var user =await  _userManager.FindByNameAsync(name);
            if (user == null)
                return NotFound("Users Not Found");
            var userroles=await _userManager.GetRolesAsync(user);
            var userapp = new ApplicationUser()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Country = user.Country,
                UserRoles = string.Join(", ",userroles.ToArray())
            };
            return Ok(userapp);
        }
        [HttpGet]
        public async Task<IActionResult> GetUsersByEmail(string email)
        {
            var user =await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound("Users Not Found");
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> GetUsersPagination(Pagination pagination)
        {
            if (pagination == null)
                return Ok(new Shared.GenaricResponse.ApiReturnObj<object>
                {
                    ReturnValue = null,
                    Status = Shared.Enums.ApiReturnStatus.BadRequest,
                    TotalPaginationCount = 0
                }) ;
            bool hassearch =! string.IsNullOrEmpty(pagination.SearchText);
            int datacount = -1;
            if(hassearch)
                datacount=await _userManager.Users.Where(u=>u.UserName.Contains(pagination
                    .SearchText)).CountAsync();
            else
                datacount=await _userManager.Users.CountAsync();
            if(pagination.Index*pagination.PageSize>datacount)
                return Ok(new Shared.GenaricResponse.ApiReturnObj<object>
                {
                    ReturnValue = null,
                    Status = Shared.Enums.ApiReturnStatus.IndexOutOfRange,
                    TotalPaginationCount = datacount
                });
            List<ApplicationUser> users;
            if (hassearch)
            
                users = await _userManager.Users.Where(u => u.UserName.Contains(
                    pagination.SearchText)).OrderBy(u => u.UserName)
                    .Skip(pagination.Index * pagination.PageSize).
                    Take(pagination.PageSize).ToListAsync();
              
            
            else
                users = await _userManager.Users.OrderBy(u => u.UserName)
                 .Skip(pagination.Index * pagination.PageSize).
                 Take(pagination.PageSize).ToListAsync();
            var user = users.Select(u => new ApplicationUser()
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Country = u.Country,
                UserRoles = string.Join(", ", _userManager.GetRolesAsync(u).Result.ToArray())
            });
            return Ok(new Shared.GenaricResponse.ApiReturnObj<List<ApplicationUser>>
            {
                ReturnValue = user.ToList(),
                Status = Shared.Enums.ApiReturnStatus.Success,
                TotalPaginationCount = datacount
            });
        }
    }
}
