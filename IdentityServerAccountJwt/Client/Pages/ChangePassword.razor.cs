using IdentityServerAccountJwt.Client.Services;
using IdentityServerAccountJwt.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace IdentityServerAccountJwt.Client.Pages
{
    public partial class ChangePassword
    {
        [Parameter]
        public string Username { get; set; }
        [Inject]
        public IUserRole userRole { get;set; }
        [Inject]
        public IAuthenticationRegister authenticationRegister { get; set; }
        public ChangePasswordDto ChangePasswordDto { get; set; } = new ChangePasswordDto();

        public bool Ischangepassword { get; set; }
        public IEnumerable<string> Errors { get; set; } 
        [Inject]
        public NavigationManager NavigationManager { get; set; }    
        public async Task SaveChangePassword()
        {
            Ischangepassword = false;
            ChangePasswordDto.UserName = Username;
            var result=await userRole.ChangePassword(ChangePasswordDto);
            if (!result.IsSuccess)
            {
                Errors = result.Errors;
                Ischangepassword = true;
            }
            else
            {
                await authenticationRegister.Logout();
                NavigationManager.NavigateTo("/");

            }

        }

    }
}
