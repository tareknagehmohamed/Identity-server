using IdentityServerAccountJwt.Client.Services;
using IdentityServerAccountJwt.Shared.Dtos.UsersRoles;
using Microsoft.AspNetCore.Components;

namespace IdentityServerAccountJwt.Client.Pages.AuthoriaztionPages
{
    public partial class UserRoles
    {
        [Parameter]
        public string userid { get; set; } 
        public UserRolesDto userRoles { get; set; }=new UserRolesDto();
        [Inject]
        public IUserRole UserRoleManager { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            userRoles = await UserRoleManager.GetUserRoles(userid);
        }
        private async Task AddUserRole()
        {
            await UserRoleManager.AddUserRole(userRoles);
            NavigationManager.NavigateTo("/userspagination");
        }
    }
}
