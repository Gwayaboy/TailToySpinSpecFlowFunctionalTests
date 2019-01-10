using FunctionalTests.Assertions;
using FunctionalTests.Configuration;
using FunctionalTests.Models;
using FunctionalTests.Pages;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace FunctionalTests.Steps
{
    [Binding]
    public class AddingPlanesToCartSteps
    {
        private BrowserHost _browser;

        // One instance of driver for each scenario (to force cart to be empty)
        public AddingPlanesToCartSteps(BrowserHost browser)
        {
            _browser = browser;
        }
      
        [Given(@"the cart has the following planes")]
        public void GivenTheCartAlreadyHasTheFollowingPlanes(IEnumerable<Plane> planes)
        {
            //When possible prefer accessing from backend to set up 
            foreach(var plane in planes)
            {
                CartPage =
                 _browser
                    .NavigateToInitialPage<AirplaneListPage>($"http://tailspintoys.azurewebsites.net/?slug={plane.Type}")
                    .GoToPlaneDetailsFrom(plane.Description)
                    .AddToCart();
            }
        }

        [Given(@"I have navigated to the cart page")]
        public void GivenIHaveNavigatedToTheCartPage()
        {
            CartPage = CartPage ?? _browser.NavigateToInitialPage<CartPage>("http://tailspintoys.azurewebsites.net/Cart/Show");
        }


        [Given(@"I have selected the ""(.*)""")]
        public void GivenIHaveNavigatedToTheModelAirplanes(AirplaneType airplaneType)
        {
            AirplaneListPage = _browser.NavigateToInitialPage<AirplaneListPage>($"http://tailspintoys.azurewebsites.net/?slug={airplaneType}");
        }
        
        [Given(@"I have selected the (.*) plane")]
        public void GivenISelectedTheNthPlane(int position)
        {
            SelectedPlaneDetailPage = AirplaneListPage.GoToPlaneDetailsAt(position);
        }

        [When("I update the quantity (.*) for (.*) plane's in the cart")]
        public void GivenIHaveEnteredQuantityInTheCartForNthPlane(string newQuantity, int planePosition)
        {
            CartPage.UpdateQuantity(newQuantity, planePosition);
        }

        [When(@"I add the plane")]
        public void WhenAddThePlane()
        {
            CartPage = SelectedPlaneDetailPage.AddToCart();
        }
        
        [Then(@"I should be redirected to the cart page's url starting with ""(.*)""")]
        public void ThenIShouldBeRedirectedToTheCartPage(string expectedStartingUrl)
        {
            CartPage.Should().HaveUrlStartingWith(expectedStartingUrl);
        }

        [Then(@"the page title's should be ""(.*)""")]
        public void ThenThePageTitleShouldBe(string expectedPageTitle)
        {
            CartPage.Should().HavePageTitle(expectedPageTitle);
        }

        [Then(@"there should be (\d+) plane\(s\) in the cart")]
        public void ThenThereShouldBeNumberOfPlanesInCart(int expectedNumberOfPlanesInCart)
        {
            CartPage.Should().HaveNumberOfPlanes(expectedNumberOfPlanesInCart);
        }

        [Then(@"the (.*) plane's description in the cart should be ""(.*)""")]
        public void ThenNthPlanDescriptionShouldBe(int planePosition, string planeDescription)
        {
            CartPage.Should().HavePlaneDescriptionAt(planePosition, planeDescription);
        }
        
        [Then(@"the (.*) plane's total price in the cart should be (.*)")]
        public void ThenTheNthPlaneTotalPriceShouldBe(int planePosition, string expectedPlaneTotalPrice)
        {
            CartPage.Should().HavePlaneTotalPriceAt(planePosition, expectedPlaneTotalPrice);
        }

        [Then(@"the (.*) plane's quantity in the cart should be (.*)")]
        public void ThenTheStPlaneSQuantityShouldBe(int planePosition, int expectedPlaneQuantity)
        {
            CartPage.Should().HavePlaneQuantityAt(planePosition, expectedPlaneQuantity);
        }


        #region Private members

        AirplaneListPage AirplaneListPage { get; set; }
        PlaneDetailPage SelectedPlaneDetailPage { get; set; }
        CartPage CartPage { get; set; }

        #endregion
    }
}
