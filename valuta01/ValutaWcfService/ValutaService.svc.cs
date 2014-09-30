using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ValutaWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ValutaService : IValutaService
    {
        private Dictionary<string, decimal> exchangeRates;

        public ValutaService()
        {
            exchangeRates = new Dictionary<string, decimal>()
            {
                {"CAD", 492.27m},
                {"EUR", 745.99m},
                {"GBP", 947.53m},
                {"NOK", 90.34m},
                {"SEK", 78.21m},
                {"USD", 524.02m},
            };
        }

        public decimal FromDkkToEur(decimal dkkAmount)
        {
            decimal dkkToEurRate = exchangeRates["EUR"];
            decimal euroAmount = dkkAmount / dkkToEurRate * 100;

            return euroAmount;
        }


        public decimal GetExchangeRate(string iso)
        {
            decimal exchangeRate = 0;
            exchangeRates.TryGetValue(iso, out exchangeRate);

            return exchangeRate;
        }
    }
}
