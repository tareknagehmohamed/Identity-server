using IdentityServerAccountJwt.Client.Helpers;
using IdentityServerAccountJwt.Client.Services;
using IdentityServerAccountJwt.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace IdentityServerAccountJwt.Client.Pages.AuthoriaztionPages
{
    public partial class RegisterUser
    {
        [Inject]
        public IAuthenticationRegister authorizationregister { get; set; }
        public UserRegister userRegister { get; set; }=new UserRegister();
        [Inject] public NavigationManager navigationManager { get; set; }
        public bool IsError { get;set; }
        public IEnumerable<string> Errors { get; set; }    = new List<string>();
        public async Task RegisterModel()
        {
            IsError = false;
          var result=  await authorizationregister.RegisterUser(userRegister);
            if (!result.IsSuccess)
            {
                Errors = result.Errors;
                IsError = true; 
            }
            else
            {
                navigationManager.NavigateTo("/Login");
                SnackBarHelper.ShowSuccess(snackBar,"You Are Successfully Registered");
            }
        }

        



    }
}
