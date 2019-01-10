using FunctionalTests.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace FunctionalTests.Steps
{
    [Binding]
    public class ThirdPartySeleniumGridSteps : BaseSteps
    {
        [Given("the selected browser is (.*) (.*) on (.*) (.*)")]
        public void GivenTheRemoteSelectedBrowserIs(string browserName, string browserVersion, string os, string osVersion)
        {
            Container.RegisterInstanceAs(new BrowserHost(new RemoteBrowserConfiguration(browserName, 
                                                                                        browserVersion, 
                                                                                        os, 
                                                                                        osVersion)));
        }
    }
}
