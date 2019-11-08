using System;

namespace ValenteMesmo.SeleniumWrapper
{
    public static class SelectionCheckExtensions
    {
        public static bool IsSelected(this SeleniumWrapper wrapper, string selector)
        {
            var element = wrapper.driver.FindElementByCssSelector(selector);
            if (element == null)
                throw new Exception($"Elemento nao encontrado: {selector}");

            return element.Selected;
        }

        public static bool IsNotSelected(this SeleniumWrapper wrapper, string selector)
        {
            var element = wrapper.driver.FindElementByCssSelector(selector);
            if (element == null)
                throw new Exception($"Elemento nao encontrado: {selector}");

            return !element.Selected;
        }
    }
}
