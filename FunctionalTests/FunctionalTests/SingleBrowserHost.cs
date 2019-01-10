using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using TestStack.Seleno.Configuration;
using TestStack.Seleno.Configuration.WebServers;

namespace FunctionalTests
{
    public static class SingleBrowserHost
    {
        public static SelenoHost Instance => new SelenoHost();

        static SingleBrowserHost()
        {
            Instance.Run(config =>
                            config
                                .WithWebServer(new InternetWebServer("http://tailspintoys.azurewebsites.net"))
                                .WithRemoteWebDriver(Chrome));
        }

        public static RemoteWebDriver Chrome()
        {
            var options = new ChromeOptions();
            options.AddArgument("test-type");
            return new ChromeDriver(options);
        }

    }

}
