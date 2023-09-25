namespace IdentityServerAccountJwt.Client.Statics
{
    public class UserSettings
    {
        private ThemeTypes _theme;

        public ThemeTypes UserTheme
        {
            get { return _theme; }
            set { _theme = value; OnThemeChanged?.Invoke(); }
        }
        [NonSerialized]
        public Action OnThemeChanged;
    }
}
