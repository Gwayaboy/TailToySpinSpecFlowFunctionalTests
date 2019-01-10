using OpenQA.Selenium;
using System;
using TestStack.Seleno.PageObjects;

namespace FunctionalTests.Pages
{
    public class PlaneDetailPage : Page
    {
        public string Description
        {
            get
            {
                return Find.Element(By.CssSelector(".product-overview h2")).Text;
            }
        }

        public CartPage AddToCart()
        {
            return Navigate.To<CartPage>(By.CssSelector(".product-overview form input.add-cart"));
        }
    }
}