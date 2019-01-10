using System;
using OpenQA.Selenium.Remote;
using System.Configuration;
using TechTalk.SpecFlow;
using System.Collections.Specialized;
using System.IO;

namespace FunctionalTests.Configuration
{
    public class BrowserStackDriver
    {
        private RemoteWebDriver driver;
        private string profile;
        private string environment;
        private ScenarioContext context;

        public RemoteWebDriver Init(string profile, string environment)
        {
            var caps = (NameValueCollection)ConfigurationManager.GetSection("capabilities/" + profile);
            var settings = (NameValueCollection)ConfigurationManager.GetSection("environments/" + environment);

            DesiredCapabilities capability = new DesiredCapabilities();

            foreach (string key in caps.AllKeys)
            {
                capability.SetCapability(key, caps[key]);
            }

            foreach (string key in settings.AllKeys)
            {
                capability.SetCapability(key, settings[key]);
            }

            String username = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
            if (username == null)
            {
                username = ConfigurationManager.AppSettings.Get("user");
            }

            String accesskey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
            if (accesskey == null)
            {
                accesskey = ConfigurationManager.AppSettings.Get("key");
            }

            capability.SetCapability("browserstack.user", username);
            capability.SetCapability("browserstack.key", accesskey);

            File.AppendAllText("C:\\Users\\Admin\\Desktop\\sf.log", "Starting driver");
            return driver = new RemoteWebDriver(new Uri("http://" + ConfigurationManager.AppSettings.Get("server") + "/wd/hub/"), capability);
        }

        public void Cleanup()
        {
            driver.Quit();
            
        }
    }

}
