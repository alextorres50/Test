using System;
namespace Test.Core.Router
{
	public static class RouterNavigation
	{
        public static string CleanStack = "Test:///";
        public static string Absolute = "/";

        public static string LoginPageInit = $"NavigationPage/{nameof(LoginPage)}";
        public static string LoginPage = $"{nameof(LoginPage)}";

        public static string DashBoardPage = $"{nameof(DashBoardPage)}";
        public static string DashBoardPageInit = $"NavigationPage/{nameof(DashBoardPage)}";

        public static string WeatherPage = $"{nameof(WeatherPage)}";
        public static string ContactsPage = $"{nameof(ContactsPage)}";

        public static string UsersPage = $"{nameof(UsersPage)}";
    }
}

