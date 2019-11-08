using OpenQA.Selenium.Support.UI;

namespace ValenteMesmo.SeleniumWrapper
{
    public static class TextGetExtensions
    {

        public static string GetText(this SeleniumWrapper wrapper, string selector)
        {
            var element = wrapper.driver.FindElementByCssSelector(selector);
            if (element.TagName == "select")
                return new SelectElement(element).SelectedOption?.Text;

            if (element.TagName == "textarea" || element.Text == "")
                return element.GetAttribute("value");

            return element.Text;
        }

        public static string GetTextFromTableCell(
            this SeleniumWrapper wrapper
            , string selector
            , int rowIndex
            , int columnIndex
            , int? milliseconds = null) =>
            wrapper.GetCell(selector, rowIndex, columnIndex, milliseconds).Text;

    }
}
