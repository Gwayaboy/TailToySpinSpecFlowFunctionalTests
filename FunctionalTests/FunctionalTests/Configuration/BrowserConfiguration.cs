using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalTests.Configuration
{
    public class RemoteBrowserConfiguration
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public string OS { get; set; }

        public string OSVersion { get; set; }


        public string Username { get; set; }

        public RemoteBrowserConfiguration(string browserName, string browserVersion, string os, string osVersion)
        {
            Name = browserName;
            Version = browserVersion;
            OS = os;
            OSVersion = osVersion;
        }
    }
}
