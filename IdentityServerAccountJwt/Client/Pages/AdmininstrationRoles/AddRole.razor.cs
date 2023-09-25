using IdentityServerAccountJwt.Client.Helpers;
using IdentityServerAccountJwt.Client.Services;
using IdentityServerAccountJwt.Shared.Dtos.Administrations;
using Microsoft.AspNetCore.Components;

namespace IdentityServerAccountJwt.Client.Pages.AdmininstrationRoles
{
    public partial class AddRole
    {
        [Inject]
        public IRoleService roleService { get;set; }
        [Inject]
        public NavigationManager navigationManager { get;set; }

        public AdministrationRole administrationRole { get; set; } = new AdministrationRole();
        public async Task AddRoleName()
        {
            await roleService.AddRole(administrationRole);
            navigationManager.NavigateTo("rolelist");
            SnackBarHelper.ShowSuccess(snackBar, "Role Added Successfully ");

        }
    }
}
