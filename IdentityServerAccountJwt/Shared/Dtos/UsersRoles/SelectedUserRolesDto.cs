using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerAccountJwt.Shared.Dtos.UsersRoles
{
    public class SelectedUserRolesDto
    {
        public string Name { get; set; }
        public bool IsSelected { get;set; }
    }
}
