using OpenQA.Selenium.Remote;

namespace FunctionalTests.Configuration
{
    public static class RemoteDriverExtensions
    {
        public static RemoteWebDriver MaximizeBrowserWindow(this RemoteWebDriver driver)
        {
            if (driver != null )
            {
                driver.Manage().Window.Maximize();
            }
            return driver;
        }
    }

}
