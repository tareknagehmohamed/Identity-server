using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerAccountJwt.Shared.Dtos
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Username Is Requered")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email Is Requered")]
        [EmailAddress(ErrorMessage ="Format Email Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Requered")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Confirm Password Not Matched With Password")]
        public string ConfirmPassword { get; set; }
        public bool IsActive { get; set; }

    }
}
