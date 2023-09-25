
using IdentityServerAccountJwt.Shared.Dtos;
using IdentityServerAccountJwt.Shared.Dtos.UsersRoles;
using IdentityServerAccountJwt.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerAccountJwt.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        public UserRolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }
        [HttpGet("{userid}")]
        public async Task<IActionResult> GetUserRoles(string userid)
        {
            var user=await _userManager.FindByIdAsync(userid);
            if (user==null)
            {
                return NotFound();
            }
            var _roles = await _roleManager.Roles.ToListAsync();
            var userroles = new UserRolesDto {
                Id = user.Id,
                UserName = user.UserName,
                SelectedUserRoles = _roles.Select(r => new SelectedUserRolesDto
                {
                    Name = r.Name,
                    IsSelected = _userManager.IsInRoleAsync(user,r.Name).Result
                }).ToList() 
            
            };
            return Ok(userroles);
        }
        [HttpPost]
        public async Task<IActionResult> AddRemoveUserRole([FromBody] UserRolesDto userRolesDto) {
            var user =await _userManager.FindByIdAsync(userRolesDto.Id);
            if (user==null)
            {
                return NotFound();
            }
            foreach (var role in userRolesDto.SelectedUserRoles)
            {
                if (role.IsSelected)
                {
                    await _userManager.AddToRoleAsync(user,role.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user,role.Name);
                }

            }
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> ChangePasswordUser([FromBody] ChangePasswordDto changePasswordDto)

        {
            var user=await _userManager.FindByNameAsync(changePasswordDto.UserName);
            if (user==null)
            {
                return NotFound();
            }
            var newpassword = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword,
                changePasswordDto.NewPassword);
            if (!newpassword.Succeeded) {

                return Unauthorized(new UserResponse { Errors = new[] {"Invalid Current Password"}});

            }
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword( ForgotPasswordDto forgotPasswordDto)
        {
            //string email = "medonageh1999@gmail.com";
            //string subject = "mohaed nageh";
            //string message = "welcome to egypt";
            //await _emailSender.SendEmailAsync(email, subject, message);
            //return Ok("Email Sended");
               if (ModelState.IsValid)
            {
                var user=await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
                if (user==null)
                {
                    return NotFound();
                }
                var token=await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordresetlink=$"{forgotPasswordDto.Weblink}?email={forgotPasswordDto.Email}&token={token}";
                await _emailSender.SendEmailAsync(user.Email, "Reset Password", $"<p>Hi {user.UserName}</p>" +
                    $"<p>You Should <a href={passwordresetlink}>Click Here</a> To Reset Password</P>");
               // await _emailSender.SendEmailAsync(user.Email, "Reset Password",passwordresetlink);
                return Ok("Email Sended");

            }
return BadRequest("Cant Sent Your Email");
       
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordTarek( ForgotPasswordDto forgotPasswordDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
                if (user == null)
                {
                    return NotFound();
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                forgotPasswordDto.Weblinkfulltoken= $"{forgotPasswordDto.Weblink}?email={forgotPasswordDto.Email}&token={token}";
                var passwordreset = forgotPasswordDto.Weblinkfulltoken;
                //return Ok();
                if (passwordreset!=null)
                {
                    return Ok(passwordreset);

                }

            }
            return BadRequest("Email Or Token Is Not Valid");
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (ModelState.IsValid)
            {

                var user=await _userManager.FindByEmailAsync(resetPasswordDto.Email);   
                if (user == null)
                {
                    return NotFound();
                }
                var newtoken = resetPasswordDto.Token.Replace(" ","+");
                var result = await _userManager.ResetPasswordAsync(user, newtoken, resetPasswordDto.Password);
                if (!result.Succeeded) {
                    return BadRequest("Cant Reset Password");
                }
                
                return Ok();
            }
            return BadRequest("Cant Reset Password");
        }
    }
}
