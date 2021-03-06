﻿using OpenQA.Selenium;
using System.Linq;
using TestStack.Seleno.PageObjects;

namespace FunctionalTests.Pages
{
    public class CartPage : Page
    {
        public int NumberOfItems => Find.Elements(By.CssSelector(".shopping-cart tbody tr")).Count();
        
        public string PlaneDescriptionAt(int position = 1)
        {
            return Find.Element(By.CssSelector($".shopping-cart tbody tr:nth-child({position}) strong")).Text;
        }

        public CartPage ClearCart()
        {
            var elements = Find.Elements(By.CssSelector(".shopping-cart tbody tr input[type=button].remove")).ToList();

            foreach (var removeItemButton in elements)
            {
                removeItemButton.Click();
            }

            return this;
        }
        public string PlaneTotalPriceAt(int position)
        {
            return Find.Element(By.CssSelector($".shopping-cart tbody tr:nth-child({position}) td:last-child")).Text;
        }

        public int QuantityAt(int position)
        {
            var rawQuantity = Find
                                .Element(By.CssSelector($".shopping-cart tbody tr:nth-child({position}) input[type=text].quantity"))
                                .GetAttribute("value");
            return int.TryParse(rawQuantity, out var result) ? result : 0;
        }

        public CartPage UpdateQuantity(string newQuantity, int position)
        {
            Execute.Script($"var qtyInput = document.getElementsByClassName('quantity')[{position-1}]; " +
                           $"qtyInput.setAttribute('value','{newQuantity}');" +
                           $"qtyInput.form.submit();" +
                           $"return 0;");
            return this;
        }
    }
}
