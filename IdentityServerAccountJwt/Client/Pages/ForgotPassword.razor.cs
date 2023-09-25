using IdentityServerAccountJwt.Client.Services;
using IdentityServerAccountJwt.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace IdentityServerAccountJwt.Client.Pages
{
    public partial class ForgotPassword
    {
        [Inject]
        public IUserRole userRole { get; set; }
        public ForgotPasswordDto _forgotPasswordDto { get; set; }=new ForgotPasswordDto();

        [Inject] public NavigationManager navigationManager { get; set; }
        public bool IsError { get; set; }
        public bool IsShowDiv { get; set; }=true;
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public bool ShowLinkButton { get; set; }=false;
        public async Task ForgotPasswordToEmail()
        {
            IsError = false;
            _forgotPasswordDto.Weblink = navigationManager.BaseUri + "resetpassword";
         
        var result= await userRole.ForgotPasswordTarek(_forgotPasswordDto);
            if (!result.IsSuccess)
            {
                Errors = result.Errors;
                IsError = true;
            }
            else
            {
             ShowLinkButton = true;
                IsShowDiv=false;
            }
        }

    }
}
