using IdentityServerAccountJwt.Client.Helpers;
using IdentityServerAccountJwt.Client.Services;
using IdentityServerAccountJwt.Shared.Dtos.Administrations;
using Microsoft.AspNetCore.Components;

namespace IdentityServerAccountJwt.Client.Pages.AdmininstrationRoles
{
    public partial class UpdateRole
    {
        [Inject]
        public IRoleService roleService { get; set; }
        public AdministrationRole administrationRole { get; set; }=new AdministrationRole();
        [Parameter]
        public string RoleId { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            administrationRole = await roleService.GetRoleById(RoleId);
        }
        public async Task UpdateRoleUser()
        {
            await roleService.EditRole(administrationRole);
            navigationManager.NavigateTo("/rolelist");
            SnackBarHelper.ShowSuccess(snackBar, "Role Updated Successfully ");

        }

    }
}
