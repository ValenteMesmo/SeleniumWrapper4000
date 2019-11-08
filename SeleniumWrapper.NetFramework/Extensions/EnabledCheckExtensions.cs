using System;

namespace ValenteMesmo.SeleniumWrapper
{
    public static class EnabledCheckExtensions
    {
        public static bool IsDisabled(this SeleniumWrapper wrapper, string selector)
        {
            var element = wrapper.driver.FindElementByCssSelector(selector);
            if (element == null)
                throw new Exception($"Elemento nao encontrado: {selector}");

            return !element.Enabled;
        }

        public static bool IsEnabled(this SeleniumWrapper wrapper, string selector)
        {
            var element = wrapper.driver.FindElementByCssSelector(selector);
            if (element == null)
                throw new Exception($"Elemento nao encontrado: {selector}");

            return element.Enabled;
        }
    }
}
