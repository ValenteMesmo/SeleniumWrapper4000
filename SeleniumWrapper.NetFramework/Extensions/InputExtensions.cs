using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace ValenteMesmo.SeleniumWrapper
{
    public static class InputExtensions
    {
        public static void SendText(this SeleniumWrapper wrapper, string text)
        {
            new Actions(wrapper.driver)
                .SendKeys(text)
                .Perform();
        }

        public static void PressEnter(this SeleniumWrapper wrapper)
        {
            new Actions(wrapper.driver)
                .SendKeys(Keys.Enter)
                .Perform();
        }
        public static void PressLeft(this SeleniumWrapper wrapper)
        {
            new Actions(wrapper.driver)
                .SendKeys(Keys.Left)
                .Perform();
        }
        public static void PressRight(this SeleniumWrapper wrapper)
        {
            new Actions(wrapper.driver)
                .SendKeys(Keys.Right)
                .Perform();
        }
        public static void PressUp(this SeleniumWrapper wrapper)
        {
            new Actions(wrapper.driver)
                .SendKeys(Keys.Up)
                .Perform();
        }
        public static void PressDown(this SeleniumWrapper wrapper)
        {
            new Actions(wrapper.driver)
                .SendKeys(Keys.Down)
                .Perform();
        }

        public static void PressDelete(this SeleniumWrapper wrapper)
        {
            new Actions(wrapper.driver)
                .SendKeys(Keys.Delete)
                .Perform();
        }

        public static void PressEnter(this SeleniumWrapper wrapper, string selector)
        {
            wrapper.driver
                .FindElementByCssSelector(selector)
                .SendKeys(Keys.Enter);
        }


        public static void PressTab(this SeleniumWrapper wrapper)
        {
            new Actions(wrapper.driver)
                .SendKeys(Keys.Tab)
                .Perform();
        }

        public static void Click(this SeleniumWrapper wrapper, string selector, int? milliseconds = null)
        {
            if (!milliseconds.HasValue)
                milliseconds = wrapper.currentTimeoutInMilliseconds;

            var wait = new WebDriverWait(wrapper.driver, TimeSpan.FromMilliseconds(milliseconds.Value));

            IWebElement element = null;
            wait.Until(drv =>
            {
                element = wrapper.driver.FindElementByCssSelector(selector);

                if (element == null || !element.Displayed || !element.Enabled)
                    return null;

                try
                {
                    element.Click();
                }
                catch (ElementClickInterceptedException)
                {
                    //driver.ExecuteScript("arguments[0].click();", element);
                    return null;
                }

                return element;
            });
        }

        public static void ClickOnTableCell(
            this SeleniumWrapper wrapper
            , string tableSelector
            , int rowIndex
            , int columnIndex
            , string selector
            , int? milliseconds = null
        )
        {
            var cell = wrapper.GetCell(tableSelector, rowIndex, columnIndex, milliseconds);

            var elements = cell.FindElements(By.CssSelector(selector));
            foreach (var element in elements)
            {
                if (element.Displayed)
                {
                    element.Click();
                    break;
                }
            }
        }

        public static void SetFile(this SeleniumWrapper wrapper, string selector, string fileName, string fileContent)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            File.WriteAllText(filePath, fileContent);

            var element = wrapper.driver.FindElementByCssSelector(selector);
            element.SendKeys(filePath);
        }

        public static void SetFile(this SeleniumWrapper wrapper, string selector)
        {
            var value = Guid.NewGuid();
            wrapper.SetFile(selector, $"Teste_{value}.txt", value.ToString());
        }

        public static void SetFile(this SeleniumWrapper wrapper, string selector, string filePath)
        {
            var element = wrapper.driver.FindElementByCssSelector(selector);
            element.SendKeys(filePath);
        }

        public static void SelectOption(this SeleniumWrapper wrapper, string selector, string value)
        {
            var select = wrapper.driver.FindElementByCssSelector(selector);
            var options = select.FindElements(By.TagName("option"));
            foreach (var option in options)
            {
                if (option.Text.Equals(value))
                {
                    option.Click();
                    break;
                }
            }
        }

        public static void SetText(this SeleniumWrapper wrapper, string selector, string text)
        {
            var element = wrapper.driver.FindElementByCssSelector(selector);
            element.Clear();
            element.SendKeys(text);
        }
    }
}
