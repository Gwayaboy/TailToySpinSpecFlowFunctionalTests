using FunctionalTests.Configuration;
using FunctionalTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace FunctionalTests.Transformation
{
    [Binding]
    public class Transforms
    {
        [StepArgumentTransformation]
        public AirplaneType ToAirplaneType(string airplaineDescription)
        {
            return
                EnumExtensions
                    .GetAll<AirplaneType>()
                    .FirstOrDefault(t => t.GetDescription().Equals(airplaineDescription, StringComparison.InvariantCultureIgnoreCase));
        }

        [StepArgumentTransformation]
        public LocalBrowserType ToBrowserType(string browserName)
        {
            return
                EnumExtensions
                    .GetAll<LocalBrowserType>()
                    .FirstOrDefault(t => t.ToString().Equals(browserName.Replace(" ",""), StringComparison.InvariantCultureIgnoreCase));
        }

        [StepArgumentTransformation]
        public int ToCardinal(string ordinal)
        {
            var value = new Regex(@"\d+").Match(ordinal).Value;

            if (string.IsNullOrWhiteSpace(value))
            {
                return 1;
            }

            return int.Parse(value);
        }

        [StepArgumentTransformation]
        public IEnumerable<Plane> PlaneCollectionTransfrom(Table table)
        {
            foreach(var row in table.Rows)
            {
                yield return new Plane(ToAirplaneType(row["Type"]), row["Description"]);
            }
        }

        [StepArgumentTransformation]
        public RemoteBrowserConfiguration BrowserConfiguration(string browserName, string browserVersion, string os, string osVersion)
        {
            return new RemoteBrowserConfiguration(browserName, browserVersion, os, osVersion);
        }
    }
}
