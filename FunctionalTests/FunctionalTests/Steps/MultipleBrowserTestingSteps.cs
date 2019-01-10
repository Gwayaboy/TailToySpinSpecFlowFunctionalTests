using FunctionalTests.Configuration;
using TechTalk.SpecFlow;

namespace FunctionalTests.Steps
{
    [Binding]
    public class MultipleBrowserTestingSteps : BaseSteps
    {
        [Given(@"the browser is (.*)")]
        public void GivenTheSelectedBrowserIs(HostedBrowserType browserType)
        {
            Container.RegisterInstanceAs(new BrowserHost(browserType));
        }

        
    }
}
