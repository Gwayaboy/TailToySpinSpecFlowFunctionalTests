using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace FunctionalTests
{
    public static class DriverHost
    {
        public static IWebDriver Instance { get; private set; }

        static DriverHost()
        {
            var options = new ChromeOptions();
            options.AddArgument("test-type");
            Instance = new ChromeDriver(options);
            AppDomain.CurrentDomain.DomainUnload += CurrentDomainDomainUnload;
        }

        private static void CurrentDomainDomainUnload(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.DomainUnload -= CurrentDomainDomainUnload;
            if (Instance != null)
            {
                Instance.Quit();
                Instance.Dispose();
                Instance = null;
            }
        }
    }
}
