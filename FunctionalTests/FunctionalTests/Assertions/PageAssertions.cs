using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.Seleno.PageObjects;

namespace FunctionalTests.Assertions
{

    public class PageAssertions<TPage> : ReferenceTypeAssertions<TPage, PageAssertions<TPage>>
        where TPage : Page, new()
    {
        protected override string Context => "Page";

        public PageAssertions(TPage page)
        {
            Subject = page;
        }

        public void HavePageTitle(string expectedTitle)
        {
            var actualPageTilte = Subject.Title;
            Execute
               .Assertion
               .ForCondition(string.Equals(actualPageTilte, expectedTitle, StringComparison.InvariantCultureIgnoreCase))
               .FailWith($"Expected page's title to be {expectedTitle}, but was {actualPageTilte}");

        }

        public void HaveUrlStartingWith(string expectedUrl)
        {
            var actualUrl = Subject.Url;
            Execute
               .Assertion
               .ForCondition(actualUrl != null &&  actualUrl.StartsWith(expectedUrl, StringComparison.InvariantCultureIgnoreCase))
               .FailWith($"Expected page's url to be {expectedUrl}, but was {actualUrl}");

        }


        protected string Ordinal(int number)
        {
            if (number < 0) return number.ToString();
            long rem = number % 100;
            if (rem >= 11 && rem <= 13) return number + "th";
            switch (number)
            {
                case 1:
                    return number + "st";
                case 2:
                    return number + "nd";
                case 3:
                    return number + "rd";
                default:
                    return number + "th";
            }
        }
    }
}

