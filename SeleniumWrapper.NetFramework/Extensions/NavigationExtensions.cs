using System;
using System.Collections.Generic;
using System.Linq;

namespace ValenteMesmo.SeleniumWrapper
{
    public static class NavigationExtensions
    {
        public static void GoToUrl(this SeleniumWrapper wrapper, string url)
        {
            wrapper.driver.Navigate().GoToUrl(url);
        }

        public static void Refresh(this SeleniumWrapper wrapper, bool deleteCookies = false)
        {
            if (deleteCookies)
                wrapper.driver.Manage().Cookies.DeleteAllCookies();
            wrapper.driver.Navigate().Refresh();
        }

        public static string GetQueryStringValue(this SeleniumWrapper wrapper, string key)
        {
            var myUri = new Uri(wrapper.driver.Url);
            var queryStrings = DecodeQueryParameters(myUri);
            if (queryStrings.ContainsKey(key))
                return queryStrings[key];

            return string.Empty;
        }

        public static void MaximizeWindow(this SeleniumWrapper wrapper) =>
            wrapper
                .driver
                .Manage()
                .Window
                .Maximize();

        public static void MinimizeWindow(this SeleniumWrapper wrapper) => 
            wrapper
                .driver
                .Manage()
                .Window
                .Minimize();

        private static Dictionary<string, string> DecodeQueryParameters(Uri uri)
        {
            if (uri == null || uri.Query.Length == 0)
                return new Dictionary<string, string>();

            return uri.Query
                        .TrimStart('?')
                        .Split(new[] { '&', ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(parameter => parameter.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries))
                        .GroupBy(
                            parts => parts[0]
                            , parts =>
                                parts.Length > 2
                                ? string.Join("=", parts, 1, parts.Length - 1)
                                : (parts.Length > 1 ? parts[1] : "")
                        )
                        .ToDictionary(grouping => grouping.Key,
                                        grouping => string.Join(",", grouping));
        }
    }
}
