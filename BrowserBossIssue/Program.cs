using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nito.BrowserBoss;

namespace BrowserBossIssue
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = ReadIpLeakNetDnsResults();

            Console.WriteLine("Here");
        }

        public static IEnumerable<string> ReadIpLeakNetDnsResults()
        {
            IEnumerable<string> els;
            try
            {
                Boss.Url = "https://ipleak.net";

                int maxTries = 15;

                while (true)
                {
                    Boss.Session.Timeout = TimeSpan.FromSeconds(5);
                    try
                    {
                        Boss.FindElements("#dnsplaceholder_waits div");
                    }
                    catch (InvalidDataException e)
                    {
                        break;
                    }
                }

                els =
                    Boss.FindElements("#dnsplaceholder_results div.location")
                        .Select(x => x.ToString().Trim(' ', '"', '\''))
                        .Distinct();
            }
            finally
            {
                Boss.Browser.WebDriver.Close();
            }
            return els;
        }
    }
}
