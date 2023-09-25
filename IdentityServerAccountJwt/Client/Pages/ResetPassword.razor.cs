using IdentityServerAccountJwt.Client.Pages.AuthoriaztionPages;
using IdentityServerAccountJwt.Client.Services;
using IdentityServerAccountJwt.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace IdentityServerAccountJwt.Client.Pages
{
    public partial class ResetPassword
    {
        [Inject]
        public IUserRole _userRole { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        public ResetPasswordDto _resetPasswordDto { get; set; }=new ResetPasswordDto();
        [Parameter]
        [SupplyParameterFromQuery(Name ="email")]
        public string Email { get; set; }
        [Parameter]
        [SupplyParameterFromQuery(Name = "token")]
        public string Token { get; set; }
       // public bool IsError { get; set; }
       // public IEnumerable<string> Errors { get; set; } = new List<string>();
        public async Task ResetPasswordUser()
        {
            Email=_resetPasswordDto.Email;
            Token=_resetPasswordDto.Token;
           // IsError = false;
             await _userRole.ResetPassword(_resetPasswordDto);
            //if (!result.IsSuccess)
            //{
            //    Errors = result.Errors;
            //    IsError = true;
            //}
            //else
            //{
                _navigationManager.NavigateTo("/Login");
            //}
        }

    }
}
