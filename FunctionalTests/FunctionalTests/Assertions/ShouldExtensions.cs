using FunctionalTests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.Seleno.PageObjects;

namespace FunctionalTests.Assertions
{
    public static class ShouldExtensions
    {
        public static PageAssertions<TPage> Should<TPage>(this TPage page)
            where TPage: Page, new()
        {
            return new PageAssertions<TPage>(page);
        }

        public static CartPageAssertions Should(this CartPage cartPage)
        {
            return new CartPageAssertions(cartPage);
        }
    }
}
