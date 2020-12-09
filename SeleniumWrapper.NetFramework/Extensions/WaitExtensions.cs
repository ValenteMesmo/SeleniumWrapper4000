using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading.Tasks;

namespace ValenteMesmo.SeleniumWrapper
{
    public static class WaitExtensions
    {
        public static void Wait(this SeleniumWrapper wrapper, int? milliseconds = null)
        {
            if (!milliseconds.HasValue)
                milliseconds = wrapper.currentTimeoutInMilliseconds;

            Task.Delay(milliseconds.Value).Wait();
        }

        public static void Wait(this SeleniumWrapper wrapper, Func<bool> condition, int? milliseconds = null)
        {
            if (!milliseconds.HasValue)
                milliseconds = wrapper.currentTimeoutInMilliseconds;

            var wait = new WebDriverWait(wrapper.driver, TimeSpan.FromMilliseconds(milliseconds.Value));

            wait.Until(drv =>
            {
                if (condition())
                    return new { };

                return null;
            });
        }

        public static void WaitUrlChange(this SeleniumWrapper wrapper, int? milliseconds = null)
        {
            var previousUrl = wrapper.driver.Url;
            if (!milliseconds.HasValue)
                milliseconds = wrapper.currentTimeoutInMilliseconds;

            var wait = new WebDriverWait(wrapper.driver, TimeSpan.FromMilliseconds(milliseconds.Value));

            wait.Until(drv =>
            {
                if (previousUrl != wrapper.driver.Url)
                    return wrapper.driver.Url;
                return null;
            });
        }

        public static void WaitTextCondition(this SeleniumWrapper wrapper, string selector, Func<string, bool> condition, int? milliseconds = null)
        {
            if (!milliseconds.HasValue)
                milliseconds = wrapper.currentTimeoutInMilliseconds;

            var wait = new WebDriverWait(wrapper.driver, TimeSpan.FromMilliseconds(milliseconds.Value));

            wait.Until(drv =>
            {
                IWebElement element = null;
                try
                {
                    element = wrapper.driver.FindElementByCssSelector(selector);
                }
                catch { }
                if (element == null)
                    return null;
                if (!element.Displayed)
                    return null;
                if (element.Text == null)
                    return null;
                if (condition(element.Text))
                    return element;
                return null;
            });
        }

        public static void WaitInTableCell(
            this SeleniumWrapper wrapper
            , string tableSelector
            , int rowIndex
            , int columnIndex
            , string inCellSelector
            , int? milliseconds = null
        )
        {
            if (!milliseconds.HasValue)
                milliseconds = wrapper.currentTimeoutInMilliseconds;

            var wait = new WebDriverWait(wrapper.driver, TimeSpan.FromMilliseconds(milliseconds.Value));

            wait.Until(drv =>
            {
                var cell = wrapper.GetCell(tableSelector, rowIndex, columnIndex, milliseconds);
                var element = cell.FindElement(By.CssSelector(inCellSelector));

                if (element == null || !element.Displayed)
                    return null;

                return element;
            });
        }

        public static void WaitInvisibilityOf(this SeleniumWrapper wrapper, string selector, int? milliseconds = null)
        {
            if (!milliseconds.HasValue)
                milliseconds = wrapper.currentTimeoutInMilliseconds;

            var wait = new WebDriverWait(wrapper.driver, TimeSpan.FromMilliseconds(milliseconds.Value));

            wait.Until(drv =>
            {
                var element = wrapper.driver.FindElementByCssSelector(selector);
                if (element != null && element.Displayed)
                    return null;

                return new { };
            });
        }

        public static void WaitVisibilityOf(this SeleniumWrapper wrapper, string selector, int? milliseconds = null)
        {
            if (!milliseconds.HasValue)
                milliseconds = wrapper.currentTimeoutInMilliseconds;

            var wait = new WebDriverWait(wrapper.driver, TimeSpan.FromMilliseconds(milliseconds.Value));

            wait.Until(drv =>
            {
                var element = wrapper.driver.FindElementByCssSelector(selector);

                if (element == null || !element.Displayed)
                    return null;

                return element;
            });
        }

        public static void WaitUntilElementIsEnabled(this SeleniumWrapper wrapper, string selector, int? milliseconds = null)
        {
            if (!milliseconds.HasValue)
                milliseconds = wrapper.currentTimeoutInMilliseconds;

            var wait = new WebDriverWait(wrapper.driver, TimeSpan.FromMilliseconds(milliseconds.Value));

            wait.Until(drv =>
            {
                var element = wrapper.driver.FindElementByCssSelector(selector);

                if (element == null || !element.Enabled)
                    return null;

                return element;
            });
        }

        public static void SetImplicityWait(this SeleniumWrapper wrapper, int milliseconds)
        {
            wrapper.currentTimeoutInMilliseconds = milliseconds;
            wrapper.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(wrapper.currentTimeoutInMilliseconds);
        }


        internal static IWebElement GetCell(this SeleniumWrapper wrapper, string selector, int rowIndex, int columnIndex, int? milliseconds = null)
        {
            if (!milliseconds.HasValue)
                milliseconds = wrapper.currentTimeoutInMilliseconds;

            IWebElement cell = null;
            var wait = new WebDriverWait(wrapper.driver, TimeSpan.FromMilliseconds(milliseconds.Value));

            wait.Until(drv =>
            {
                try
                {
                    var table = wrapper.driver.FindElementByCssSelector(selector);

                    var row = table.FindElements(By.TagName("tr"))[rowIndex];
                    cell = row.FindElements(By.TagName("td"))[columnIndex];

                }
                catch { }
                return cell;
            });

            return cell;
        }
    }
}
