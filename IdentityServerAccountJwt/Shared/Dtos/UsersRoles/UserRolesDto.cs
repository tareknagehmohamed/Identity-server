using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServerAccountJwt.Shared.Dtos.UsersRoles
{
    public class UserRolesDto
    {
        public string Id { get;set; }
        public string UserName { get;set; }
        public List<SelectedUserRolesDto> SelectedUserRoles { get; set; }   
    }
}
