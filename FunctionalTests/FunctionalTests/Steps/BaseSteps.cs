using BoDi;
using FunctionalTests.Configuration;
using System;
using TechTalk.SpecFlow;

namespace FunctionalTests.Steps
{
    public class BaseSteps
    {
        protected IObjectContainer Container => ScenarioContext.Current.ScenarioContainer;

        [BeforeScenario]
        public void RegisterDefaultBrowserFactory()
        {
#if RELEASE
            Container.RegisterFactoryAs(c => BrowserHost.CreateForHostedDriver(BrowserType.Chrome));
#else
            Container.RegisterFactoryAs(c => BrowserHost.CreateForLocalDriver(BrowserType.Chrome));
#endif
        }

        [AfterScenario]
        public void DisposeBrowser()
        {
            Container.Resolve<BrowserHost>().Dispose();
        }
    }
}
