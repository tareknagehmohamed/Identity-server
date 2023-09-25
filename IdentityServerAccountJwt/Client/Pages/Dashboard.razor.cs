using IdentityServerAccountJwt.Client.Services;
using IdentityServerAccountJwt.Shared.Dtos;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace IdentityServerAccountJwt.Client.Pages
{
    public partial class Dashboard
    {
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        IDashboardservice dashboardservice { get; set; }
        [Inject]
        public AuthenticationStateProvider _authenticationStateProvider { get; set; }
        public DashboardData dashboarddata { get; set; } = new();
        private bool arrows = true;
        private bool bullets = true;
        private bool enableSwipeGesture = true;
        private bool autocycle = true;
        private Transition transition = Transition.Slide;
        #region Overrides
        protected override async Task OnInitializedAsync()
        {

            dashboarddata = await dashboardservice.GetDashboardData();
        }
        #endregion
        public void GoToUsers()
        {
            navigationManager.NavigateTo("/userspagination");
        }
    }
}
