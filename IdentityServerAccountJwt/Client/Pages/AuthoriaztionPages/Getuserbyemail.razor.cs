using IdentityServerAccountJwt.Client.Services;
using IdentityServerAccountJwt.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace IdentityServerAccountJwt.Client.Pages.AuthoriaztionPages
{
    public partial class Getuserbyemail
    {
        public ApplicationUser applicationUser { get; set; } = new ApplicationUser();
        [Inject]
        public IAuthenticationRegister authenticationRegister { get; set; }
        [Parameter]
        public string Useremail { get; set; }

        protected override async Task OnInitializedAsync()
        {
            applicationUser = await authenticationRegister.GetUserByEmail(Useremail);
        }
    }
}
