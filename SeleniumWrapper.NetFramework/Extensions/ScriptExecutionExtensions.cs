namespace ValenteMesmo.SeleniumWrapper
{
    public static class ScriptExecutionExtensions
    {
        public static T ExecuteScript<T>(this SeleniumWrapper wrapper, string script, params object[] args)
        {
            return (T)wrapper.driver.ExecuteScript(script, args);
        }

        public static object ExecuteScript(this SeleniumWrapper wrapper, string script, params object[] args)
        {
            return wrapper.driver.ExecuteScript(script, args);
        }

        public static void ConsoleWriteError(this SeleniumWrapper wrapper, string text)
        {
            wrapper.ExecuteScript("console.error(arguments[0]);", text);
        }
    }
}
