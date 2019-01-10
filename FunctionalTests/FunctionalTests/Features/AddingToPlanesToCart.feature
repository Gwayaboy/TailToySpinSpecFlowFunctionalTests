@specflow
Feature: Adding Planes To Cart
	As a scale model enthusiast
	I want to add paper or model planes to my cart
	So that I can place an order


@Failing
Scenario: Adding the model "Fourth Coffee Flyer" airplane to the cart

	Given I have selected the "Model Airplanes"
	And I have selected the 1st plane
	When I add the plane
	Then I should be redirected to the cart page's url starting with "http://tailspintoys.azurewebsites.net/cart/show"
	And there should be 1 plane(s) in the cart
	#Next step fails because of 2 titles in cart page and only the first title gets picked up
	And the page title's should be "Fourth Coffee Flyer" 
	And the 1st plane's description in the cart should be "Fourth Coffee Flyer"

@Passing
Scenario: Adding a third paper "Wingtip Toys Stunt Plane" airplane to the cart

	Given the cart has the following planes
	 | Type            | Description          |
	 | Model Airplanes | Trey Research Rocket |
	 | Model Airplanes | Northwind Trader     |
	And I have selected the "Paper Airplanes"
	And I have selected the 2nd plane
	When I add the plane
	Then I should be redirected to the cart page's url starting with "http://tailspintoys.azurewebsites.net/cart/show"
	And there should be 3 plane(s) in the cart
	And the 3rd plane's description in the cart should be "Wingtip Toys Stunt Plane"
	And the 3rd plane's quantity in the cart should be 1
	And the 3rd plane's total price in the cart should be $50.00


@Passing
Scenario: Updating the Northwind Trader airplane's quantity to 2

	Given the cart has the following planes
	 | Type            | Description              |
	 | Paper Airplanes | Wingtip Toys Stunt Plane |
	 | Model Airplanes | Northwind Trader         |
	And I have navigated to the cart page
	When I update the quantity 3 for 2nd plane's in the cart
	Then there should be 2 plane(s) in the cart
	And the 1st plane's quantity in the cart should be 1
	And the 1st plane's total price in the cart should be $50.00
	And the 2nd plane's quantity in the cart should be 3
	And the 2nd plane's total price in the cart should be $150.00
