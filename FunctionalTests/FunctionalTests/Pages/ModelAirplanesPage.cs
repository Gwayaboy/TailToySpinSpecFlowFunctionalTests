using OpenQA.Selenium;
using System;
using System.Linq;
using TestStack.Seleno.PageObjects;

namespace FunctionalTests.Pages
{
    public class AirplaneListPage : Page
    {
        public int NumberOfPlane => Find.Elements(By.CssSelector(".product-listing li")).Count();

        public PlaneDetailPage GoToPlaneDetailsAt(int position)
        {
            return Navigate.To<PlaneDetailPage>(By.CssSelector($"ul.product-listing li:nth-child({position}) a.view"));
        }

        public PlaneDetailPage GoToPlaneDetailsFrom(string description)
        {
            return Navigate.To<PlaneDetailPage>(By.CssSelector($"ul.product-listing  a[title=\"{description}\"]"));
        }
    }
}