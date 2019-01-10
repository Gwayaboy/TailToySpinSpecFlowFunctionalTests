@specflow
Feature: third party multiple browser testing
	As a developer
	I would like to run my specflow scenario on third party selenium grid runner
	So that I do not need to update my VM and I can test on the latest browsers

# https://www.browserstack.com/automate/c-sharp
@BrowserStack
Scenario Outline: Adding the model "Fourth Coffee Flyer" airplane to the cart
	Given the selected browser is <browser> <version> on <osName> <osVersion> 
	And I have selected the "Model Airplanes"
	And I have selected the 1st plane
	When I add the plane
	Then I should be redirected to the cart page's url starting with "http://tailspintoys.azurewebsites.net/cart/show" 
	And there should be 1 plane(s) in the cart
	And the 1st plane's description in the cart should be "Fourth Coffee Flyer"

Examples:
| browser | version | osName  | osVersion |
| Chrome  | 50.0    | Windows | 8         |
| Safari  | 10.0    | OS X    | Sierra    |
| Edge    | 15.0    | Windows | 10        |


#https://wiki.saucelabs.com/display/DOCS/Instant+Selenium+C%23+Tests
#https://wiki.saucelabs.com/display/DOCS/Desired+Capabilities+Required+for+Selenium+and+Appium+Tests

@ignore
@SauceLabs
Scenario Outline: Adding the model "Trey Research Rocket" airplane to the cart
	Given the browser is is <browser> <version> on <osName> <osVersion> 
	And I have selected the "Model Airplanes"
	And I have selected the 2nd plane
	When I add the plane
	Then I should be redirected to the cart page's url starting with "http://tailspintoys.azurewebsites.net/cart/show"
	And there should be 1 plane(s) in the cart
	And the 1st plane's description in the cart should be "Fourth Coffee Flyer"

Examples:
| browser | version | osName  | osVersion  | 
| chrome  | 50.0    | Windows | 10         | 
| Safari  | 9.0     | OS X    | El Capitan | 
| IE      |         |         |            | 

# Advanced multiple browser testing with browserstack and custom specflow plugin for Nunit3
# again see https://github.com/amido/TestStack.Seleno.BrowserStack.SpecFlowPlugin 

@ignore
@browser:chrome,50.0,Windows,10
@browser:firefox
@browser:IE,800x600

Scenario: Adding the model "Wingtip Toys Stunt Plane" airplane to the cart
	Given I have selected the 2nd plane
	And I have selected the "Paper Airplanes"
	When I add the plane
	Then I should be redirected to the cart page's url starting with "http://tailspintoys.azurewebsites.net/cart/show"
	And there should be 1 plane(s) in the cart
	And the 1st plane's description in the cart should be "Trey Research Rocket"