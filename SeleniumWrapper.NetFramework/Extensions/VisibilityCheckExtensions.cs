using System;

namespace ValenteMesmo.SeleniumWrapper
{
    public static class VisibilityCheckExtensions
    {
        public static bool IsVisible(this SeleniumWrapper wrapper, string selector)
        {
            var element = wrapper.driver.FindElementByCssSelector(selector);
            if (element == null)
                throw new Exception($"Elemento nao encontrado: {selector}");

            return element.Displayed;
        }

        public static bool IsInvisible(this SeleniumWrapper wrapper, string selector)
        {
            var element = wrapper.driver.FindElementByCssSelector(selector);
            if (element == null)
                throw new Exception($"Elemento nao encontrado: {selector}");

            return !element.Displayed;
        }
    }
}
