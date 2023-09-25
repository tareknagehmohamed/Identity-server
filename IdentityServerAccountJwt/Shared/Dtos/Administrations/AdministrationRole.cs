using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerAccountJwt.Shared.Dtos.Administrations
{
    public class AdministrationRole
    {
        public  string? Id { get; set; }
        [Required(ErrorMessage ="Name Is Required")]

        public string Name { get; set; }
    }
}
