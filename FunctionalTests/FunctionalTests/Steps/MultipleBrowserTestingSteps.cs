using FunctionalTests.Configuration;
using TechTalk.SpecFlow;

namespace FunctionalTests.Steps
{
    [Binding]
    public class MultipleBrowserTestingSteps : BaseSteps
    {
        [Given(@"the browser is (.*)")]
        public void GivenTheSelectedBrowserIs(BrowserType browserType)
        {
#if RELEASE
            Container.RegisterFactoryAs(c => BrowserHost.CreateForHostedDriver(browserType));
#else
            Container.RegisterFactoryAs(c => BrowserHost.CreateForLocalDriver(browserType));
#endif
        }

        
    }
}
