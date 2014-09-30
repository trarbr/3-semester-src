using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;

namespace ValutaWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Required)]
    public class ValutaService : IValutaService
    {
        private Valuta[] valutas;

        private List<string> conversions
        {
            get
            {
                if (HttpContext.Current.Session["conversions"] == null)
                {
                    HttpContext.Current.Session["conversions"] = new List<string>();
                }
                return (List<string>)HttpContext.Current.Session["conversions"];
            }
            set
            {
                HttpContext.Current.Session["conversions"] = value;
            }
        }

        public ValutaService()
        {
            valutas = new Valuta[]
            {
                new Valuta("Canada", "CAD", 492.27m),
                new Valuta("Euro", "EUR", 745.99m),
                new Valuta("Storbritannien", "GBP", 947.99m),
                new Valuta("Norge", "NOK", 90.34m),
                new Valuta("Sverige", "SEK", 78.21m),
                new Valuta("Amerika", "USD", 524.02m),
            };
        }

        public decimal FromDkkToEur(decimal dkkAmount)
        {
            decimal dkkToEurRate = findExchangeRate("EUR");
            decimal euroAmount = dkkAmount / dkkToEurRate * 100;

            return euroAmount;
        }

        public decimal GetExchangeRate(string iso)
        {
            decimal exchangeRate = findExchangeRate(iso);

            return exchangeRate;
        }

        public Valuta[] GetValutas()
        {
            return valutas;
        }

        public decimal ConvertFromIsoToIso(string fromIso, string toIso, decimal amount)
        {
            decimal newAmount = amount * findExchangeRate(fromIso) / findExchangeRate(toIso);

            conversions.Add(String.Format("{0} {1} {2} {3}", 
                amount.ToString("N2"), fromIso, newAmount.ToString("N2"), toIso));
            HttpContext.Current.Session["conversions"] = conversions;

            return newAmount;
        }

        public string[] GetDoneConversions()
        {
            string[] conversionsArray = new string[conversions.Count];

            for (int i = 0; i < conversions.Count; i++)
            {
                conversionsArray[i] = conversions[i];
            }

            return conversionsArray;
        }

        private decimal findExchangeRate(string iso)
        {
            decimal exchangeRate = 0;

            foreach (Valuta valuta in valutas)
            {
                if (valuta.Iso.Equals(iso))
                {
                    exchangeRate = valuta.ExchangeRate;
                    break;
                }
            }

            return exchangeRate;
        }
    }
}
