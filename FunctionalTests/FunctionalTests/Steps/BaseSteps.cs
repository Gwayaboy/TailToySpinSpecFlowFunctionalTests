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
            Container.RegisterFactoryAs(c => new BrowserHost(HostedBrowserType.Chrome));
        }

        [AfterScenario]
        public void DisposeBrowser()
        {
            Container.Resolve<BrowserHost>().Dispose();
        }
    }
}
