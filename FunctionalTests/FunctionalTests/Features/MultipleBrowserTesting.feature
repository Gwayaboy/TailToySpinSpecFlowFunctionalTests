@specflow 
Feature: basic local multiple browser testing
	As a developer
	I would like to run my scenario on multiple browsers
	So that I can verify that my web applications work accross my supported web browser

Scenario Outline: Adding the model "Fourth Coffee Flyer" airplane to the cart
	Given the browser is <browser>
	And I have selected the "Model Airplanes"
	And I have selected the 1st plane
	When I add the plane
	Then I should be redirected to the cart page's url starting with "http://tailspintoys.azurewebsites.net/cart/show" 
	And there should be 1 plane(s) in the cart
	And the 1st plane's description in the cart should be "Fourth Coffee Flyer"
	
Examples:
| browser |
| Chrome  |
| Firefox |

#For concerns about browser configuration leaking into the specifications:
#  => The Browser is a tool which doesn't belong to your application, why write code to instantiate it?
# see https://github.com/amido/TestStack.Seleno.BrowserStack.SpecFlowPlugin 