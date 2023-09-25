using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerAccountJwt.Shared.Dtos
{
    public class ChangePasswordDto
    {
        public string UserName { get; set;}
        [Required(ErrorMessage = "Current Password Is Required")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "New Password Is Required")]
        public string NewPassword { get; set;}
        [Compare("NewPassword",ErrorMessage ="New Password And ConfirmPassword Is Not Matched")]
        public string ConfirmPassword { get; set;}
    }
}
