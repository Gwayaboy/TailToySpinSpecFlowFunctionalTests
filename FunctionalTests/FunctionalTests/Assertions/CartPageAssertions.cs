using FluentAssertions;
using FluentAssertions.Execution;
using FunctionalTests.Pages;
using System;

namespace FunctionalTests.Assertions
{
    public class CartPageAssertions : PageAssertions<CartPage>
    {
        public CartPageAssertions(CartPage page) : base(page) { }

        protected override string Context => "Cart page";

        public AndConstraint<CartPageAssertions> HaveNumberOfPlanes(int expectedNumberOfPlanes)
        {
            var actualNumberOfPlanes = Subject.NumberOfItems;

            Execute
                .Assertion
                .ForCondition(actualNumberOfPlanes == expectedNumberOfPlanes)
                .FailWith($"Expected number of plane(s) in cart to be {expectedNumberOfPlanes}, but was {Subject.NumberOfItems}");

            return new AndConstraint<CartPageAssertions>(this);
        }

        public AndConstraint<CartPageAssertions> HavePlaneDescriptionAt(int planePosition, string expectedPlanDescription)
        {
            var actualPlaneDescription = Subject.PlaneDescriptionAt(planePosition);

            Execute
              .Assertion
              .ForCondition(string.Equals(actualPlaneDescription, expectedPlanDescription, StringComparison.InvariantCultureIgnoreCase))
              .FailWith($"Expected the {Ordinal(planePosition)} plane's description to be \"{expectedPlanDescription}\", but was \"{actualPlaneDescription}\"");

            return new AndConstraint<CartPageAssertions>(this);

        }

        public AndConstraint<CartPageAssertions> HavePlaneTotalPriceAt(int planePosition, string expectedTotalPrice)
        {
            var actualPlaneTotalPrice = Subject.PlaneTotalPriceAt(planePosition);

            Execute
              .Assertion
              .ForCondition(string.Equals(actualPlaneTotalPrice, expectedTotalPrice, StringComparison.InvariantCultureIgnoreCase))
              .FailWith($"Expected the {Ordinal(planePosition)} plane's total price to be \"{expectedTotalPrice}\", but was \"{actualPlaneTotalPrice}\"");

            return new AndConstraint<CartPageAssertions>(this);
        }

        public AndConstraint<CartPageAssertions> HavePlaneQuantityAt(int planePosition, int expectedQuantity)
        {
            var actualPlaneQuantity = Subject.QuantityAt(planePosition);

            Execute
              .Assertion
              .ForCondition(actualPlaneQuantity == expectedQuantity)
              .FailWith($"Expected the {Ordinal(planePosition)} plane's quantity to be \"{expectedQuantity}\", but was \"{actualPlaneQuantity}\"");

            return new AndConstraint<CartPageAssertions>(this);
        }
    }

}

