using MudBlazor;

namespace IdentityServerAccountJwt.Client.Helpers
{
    public static class SnackBarHelper
    {

        public static void ShowDefault(ISnackbar snackbar, string message, Action<SnackbarOptions> configure = null)
        {
            snackbar.Add(message, configure: configure);
        }
        public static void ShowInfo(ISnackbar snackbar, string message, Action<SnackbarOptions> configure = null)
        {
            snackbar.Add(message, Severity.Info, configure);
        }
        public static void ShowSuccess(ISnackbar snackbar, string message, Action<SnackbarOptions> configure = null)
        {
            snackbar.Add(message, Severity.Success, configure);
        }
        public static void ShowWarning(ISnackbar snackbar, string message, Action<SnackbarOptions> configure = null)
        {
            snackbar.Add(message, Severity.Warning, configure);
        }
        public static void ShowError(ISnackbar snackbar, string message, Action<SnackbarOptions> configure = null)
        {
            snackbar.Add(message, Severity.Error, configure);
        }
    }
}
