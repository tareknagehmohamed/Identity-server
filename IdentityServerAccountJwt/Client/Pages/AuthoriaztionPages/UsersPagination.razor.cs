using IdentityServerAccountJwt.Client.Services;
using IdentityServerAccountJwt.Shared.Enums;
using IdentityServerAccountJwt.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace IdentityServerAccountJwt.Client.Pages.AuthoriaztionPages
{
    public partial class UsersPagination
    {
        private int CurrentPage { get; set; } = 0;
        private int TotalCount = 0;
        private int PageSize = 10;
        private double TotalPageCount = 0;
        private string OldSearchText = string.Empty;
        private string SearchText { get; set; } = string.Empty;

        public List<ApplicationUser> applicationUsers { get; set; } = new List<ApplicationUser>();
        [Inject]
        public IAuthenticationRegister authenticationRegister { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            applicationUsers = await authenticationRegister.GetUsers();
        }
        #region Pagination Functions
        private void DoLoadPageValue(int page) => LoadPageValue(page);
        private async void LoadPageValue(int page, bool forceReload = false)
        {
            if (!forceReload)
            {
                if (page == CurrentPage) return;
                var pages = Convert.ToInt32(Math.Ceiling(TotalPageCount));
                if (loadingContainer.IsLoading() && page == pages) return;
            }
            bool faild = false;
            if (!loadingContainer.TriggerLoading(snackBar)) return;
            var result = await authenticationRegister.GetUsersPagination(new()
            {
                PageSize = PageSize,
                Index = page - 1,
                SearchText = SearchText
            });
            if (result.Status == ApiReturnStatus.Success)
            {
                TotalCount = result.TotalPaginationCount;
                TotalPageCount = (double)TotalCount / (double)PageSize;
                CurrentPage = page;
                applicationUsers = result.Result;
            }
            else
            {
                faild = true;
                CurrentPage = 0;
                TotalCount = 0;
                OldSearchText = SearchText = string.Empty;
                TotalPageCount = 0;
                applicationUsers = new();

            }
            StateHasChanged();
            loadingContainer.ReleaseLoading();
            if (faild)
                LoadPageValue(1);

        }

        private void DoChanePageSize(int size)
        {
            if (size == PageSize) return;
            PageSize = size;
            LoadPageValue(1, true);
        }

        private void DoTableSearch(KeyboardEventArgs args)
        {
            if (args.Key == "Enter")
            {

                if (OldSearchText == SearchText) return;
                OldSearchText = SearchText;
                LoadPageValue(1, true);
            }
        }
        private void DoClearSearch()
        {
            OldSearchText = SearchText;
            LoadPageValue(1, true);
        }
        #endregion
        public void GetUserByName(string username)
        {
            navigationManager.NavigateTo($"/getuserbyname/{username}");
        }
        public void GetUserByEmail(string useremail)
        {
            navigationManager.NavigateTo($"/getuserbyemail/{useremail}");
        }
        public void GetUserRoles(string userid)
        {
            navigationManager.NavigateTo($"/userroles/{userid}");
        }
    }
}
