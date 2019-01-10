using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using TestStack.Seleno.Configuration;
using TestStack.Seleno.Configuration.WebServers;
using TestStack.Seleno.PageObjects;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Configuration;
using TechTalk.SpecFlow;
using OpenQA.Selenium;

namespace FunctionalTests.Configuration
{
    public class BrowserHost : IDisposable
    {
        private SelenoHost _selenoHost = new SelenoHost();

        private BrowserHost(Func<RemoteWebDriver> remoteWebDriverFactory)
        {
            _selenoHost
                .Run(config => config
                                .WithWebServer(new InternetWebServer("http://tailspintoys.azurewebsites.net"))
                                .WithRemoteWebDriver(remoteWebDriverFactory));
        }

        public static BrowserHost CreateForLocalDriver(BrowserType browserType)
        {
            return new BrowserHost(LocalWebDriverFor(browserType));
        }

        public static BrowserHost CreateForHostedDriver(BrowserType browserType)
        {
            return new BrowserHost(HostedWebDriverFor(browserType));
        }


        public BrowserHost(RemoteBrowserConfiguration configuration) : this(BrowserStackRemoteDriver(configuration))
        { }

        public TPage NavigateToInitialPage<TPage>(string url = "")
            where TPage : Page, new()
        {
            return _selenoHost.NavigateToInitialPage<TPage>(url);
        }

        #region private members

        private static RemoteWebDriver Chrome()
        {
            var options = new ChromeOptions();
            options.AddArgument("test-type");
            return new ChromeDriver(options);
        }

        private static RemoteWebDriver HostedChrome()
        {
            var options = new ChromeOptions();
            options.AddArgument("test-type");
            return new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver"),options).MaximizeBrowserWindow();
        }

        private static RemoteWebDriver FireFox()
        {
            return 
                new FirefoxDriver(new FirefoxOptions {BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe"})
                    .MaximizeBrowserWindow();
        }

        private static RemoteWebDriver InternetExplorer()
        {
            var options = new InternetExplorerOptions {
                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                IgnoreZoomLevel = true,
            };
            Environment.SetEnvironmentVariable("webdriver.ie.driver", "IEDriverServer.exe");
            return new InternetExplorerDriver(options).MaximizeBrowserWindow();
        }

        private static Func<RemoteWebDriver> LocalWebDriverFor(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.InternetExplorer:
                    return InternetExplorer;
                case BrowserType.FireFox:
                    return FireFox;
                default:
                    return Chrome;
            }
        }

        private static Func<RemoteWebDriver> HostedWebDriverFor(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.InternetExplorer:
                    return HostedIEDriver;
                case BrowserType.FireFox:
                    return HostedFireFox;
                default:
                    return HostedChrome;
            }
        }

        private static RemoteWebDriver HostedIEDriver()
        {
            var options = new InternetExplorerOptions
            {
                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                IgnoreZoomLevel = true,
                
            };
            Environment.SetEnvironmentVariable("webdriver.ie.driver", "IEDriverServer.exe");
            return new InternetExplorerDriver(Environment.GetEnvironmentVariable("IEWebDriver"), options).MaximizeBrowserWindow();
        }

        private static RemoteWebDriver HostedFireFox()
        {
            var options = new FirefoxOptions
            {
                BrowserExecutableLocation = Environment.GetEnvironmentVariable("GeckoWebDriver")
            };

            return new FirefoxDriver(options);
        }

        private static Func<RemoteWebDriver> BrowserStackRemoteDriver(RemoteBrowserConfiguration configuration = null)
        {
            var capability = new DesiredCapabilities();
            capability.SetCapability(RemoteCapabilityType.BrowserStack.UserName, ConfigurationManager.AppSettings[RemoteCapabilityType.BrowserStack.UserName]);
            capability.SetCapability(RemoteCapabilityType.BrowserStack.AccessKey, ConfigurationManager.AppSettings[RemoteCapabilityType.BrowserStack.AccessKey]);

            if (configuration != null)
            {
                capability.SetCapability(RemoteCapabilityType.Default.BrowserName, configuration.Name);
                capability.SetCapability(RemoteCapabilityType.Default.Version, configuration.Version);
                capability.SetCapability(RemoteCapabilityType.BrowserStack.Os, configuration.OS);
                capability.SetCapability(RemoteCapabilityType.BrowserStack.OsVersion, configuration.OSVersion);

                PlatformType platformType;
                if (Enum.TryParse(configuration.OS, true, out platformType))
                {
                    var platform = new Platform(platformType);
                    capability.SetCapability(RemoteCapabilityType.Default.Platform, platform);
                }

                capability.SetCapability(RemoteCapabilityType.BrowserStack.TestName, ScenarioContext.Current.ScenarioInfo.Title);
                capability.SetCapability(RemoteCapabilityType.BrowserStack.Project, FeatureContext.Current.FeatureInfo.Title);
            }

            //for more capabilities https://www.browserstack.com/automate/c-sharp


            return () => new RemoteWebDriver(new Uri("http://hub-cloud.browserstack.com/wd/hub/"), capability).MaximizeBrowserWindow();
        }

        private static Func<RemoteWebDriver> SauceLabsRemoteDriver(string userName, string accessKey, RemoteBrowserConfiguration configuration = null)
        {
            var capability = new DesiredCapabilities();
            capability.SetCapability("username", userName);
            capability.SetCapability("accessKey", accessKey);

            if (configuration != null)
            {
                capability.SetCapability(CapabilityType.BrowserName, configuration.Name);
            }


            // for more capabilities https://wiki.saucelabs.com/display/DOCS/Instant+Selenium+C%23+Tests

            return () => new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), capability).MaximizeBrowserWindow();
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _selenoHost.Dispose();
                    _selenoHost = null;
                }
                disposedValue = true;
            }
        }


        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }

}
