using IdentityServerAccountJwt.Client.Helpers;
using IdentityServerAccountJwt.Client.Services;
using IdentityServerAccountJwt.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace IdentityServerAccountJwt.Client.Pages.AuthoriaztionPages
{
    public partial class LoginUser
    {
        [Inject]
        public IAuthenticationRegister authorizationregister { get; set; }
 
        public UserLogin userlogin { get; set; } = new UserLogin();
        [Inject] public NavigationManager navigationManager { get; set; }
        public bool IsError { get; set; }
        [Parameter]
        public bool RememberMe { get; set; }=false;
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public async Task LoginModel()
        {
            IsError = false;
            userlogin.IsRememberMe = RememberMe;
            var result = await authorizationregister.LoginUser(userlogin);
            if (!result.IsSuccessLogin)
            {
                Errors = result.Errors;
                IsError = true;
            }
            else
            {
                navigationManager.NavigateTo("/Dashboard");
                SnackBarHelper.ShowSuccess(snackBar, "You Are Successfully Logined");

            }
        }
        public void CheckRememberMe()
        {
            RememberMe = !RememberMe;
        }
   
        public void GoToRegisterUser()
        {
            
          navigationManager.NavigateTo("/Register");

            
        }

    }
}
