using IdentityServerAccountJwt.Client.Helpers;
using IdentityServerAccountJwt.Client.Services;
using IdentityServerAccountJwt.Shared.Dtos.Administrations;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace IdentityServerAccountJwt.Client.Pages.AdmininstrationRoles
{
    public partial class RoleList
    {
        [Inject]
        public IRoleService RoleService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public List<AdministrationRole> admininstrationRoles { get; set; } = new List<AdministrationRole>();
        protected override async Task OnInitializedAsync()
        {
            admininstrationRoles = await RoleService.GetRoles();

        }
        public async Task DeleteRole(string RoleId)
        {
            var CurrRole = admininstrationRoles.FirstOrDefault(id => id.Id == RoleId);
            var Confirmed = await JSRuntime.InvokeAsync<bool>("confirm", $"Are You Sure To Delete This Role Of Name{CurrRole.Name}");
            if (Confirmed)
            {
                await RoleService.DeleteAsync(RoleId);
                admininstrationRoles = await RoleService.GetRoles();
                SnackBarHelper.ShowSuccess(snackBar, "Role Deleted Successfully ");

            }
        }
        public void NavigateToAddRole()
        {
            NavigationManager.NavigateTo("/addrole");
        }
        public void NavigateToUpdate(string id)
        {
            NavigationManager.NavigateTo($"/updaterole/{id}");
        }
    }
}
