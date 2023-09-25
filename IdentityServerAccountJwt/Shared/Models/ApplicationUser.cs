﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerAccountJwt.Shared.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? Country { get; set; }
        public bool? IsActive { get; set; }

        [NotMapped]
        public string UserRoles { get; set; }    
    }
}
