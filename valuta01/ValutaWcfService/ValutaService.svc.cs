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
        private List<Valuta> valutas
        {
            get
            {
                if (HttpContext.Current.Application["valutas"] == null)
                {
                    HttpContext.Current.Application["valutas"] = new List<Valuta>()
                    {
                        new Valuta("Canada", "CAD", 492.27m),
                        new Valuta("Euro", "EUR", 745.99m),
                        new Valuta("Storbritannien", "GBP", 947.99m),
                        new Valuta("Norge", "NOK", 90.34m),
                        new Valuta("Sverige", "SEK", 78.21m),
                        new Valuta("Amerika", "USD", 524.02m),
                    };
                }
                return (List<Valuta>)HttpContext.Current.Application["valutas"];
            }
            set
            {
                HttpContext.Current.Application["valutas"] = value;
            }
        }
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

        // use this as a coarse grained lock
        // could have one coarse grained lock and one lock for each of valutas and conversions
        // might be useful for reducing the scope of each lock, but also a hassle to implement correctly
        private object serviceLock; 

        public ValutaService()
        {
            serviceLock = new object();
        }

        public decimal FromDkkToEur(decimal dkkAmount)
        {
            decimal euroAmount = 0m;
            lock (serviceLock)
            {
                decimal dkkToEurRate = findExchangeRate("EUR");
                euroAmount = dkkAmount / dkkToEurRate * 100;
            }

            return euroAmount;
        }

        public decimal GetExchangeRate(string iso)
        {
            decimal exchangeRate;
            lock (serviceLock)
            {
                exchangeRate = findExchangeRate(iso);
            }

            return exchangeRate;
        }

        public Valuta[] GetValutas()
        {
            Valuta[] valutasArray;
            lock (serviceLock)
            {
                valutasArray = valutas.ToArray();
            }

            return valutasArray;
        }

        public decimal ConvertFromIsoToIso(string fromIso, string toIso, decimal amount)
        {
            decimal newAmount;
            lock (serviceLock)
            {
                try
                {
                    newAmount = amount * findExchangeRate(fromIso) / findExchangeRate(toIso);
                }
                catch (DivideByZeroException)
                {
                    newAmount = 0m;
                }

                conversions.Add(String.Format("{0} {1} {2} {3}",
                    amount.ToString("N2"), fromIso, newAmount.ToString("N2"), toIso));
                HttpContext.Current.Session["conversions"] = conversions;
            }

            return newAmount;
        }

        public string[] GetDoneConversions()
        {
            string[] conversionsArray;
            lock (serviceLock)
            {
                conversionsArray = conversions.ToArray();
            }

            return conversionsArray;
        }

        public void SetValutaExchangeRate(Valuta valuta)
        {
            lock (serviceLock)
            {
                Valuta actualValuta = findValuta(valuta.Iso);
                actualValuta.ExchangeRate = valuta.ExchangeRate;
            }
        }

        public void AddValuta(Valuta valuta)
        {
            lock (serviceLock)
            {
                valutas.Add(valuta);
            }
        }

        private Valuta findValuta(string iso)
        {
            Valuta foundValuta = null;

            foreach (Valuta valuta in valutas)
            {
                if (valuta.Iso.Equals(iso))
                {
                    foundValuta = valuta;
                    break;
                }
            }

            return foundValuta;
        }

        private decimal findExchangeRate(string iso)
        {
            decimal exchangeRate = 0m;

            Valuta valuta = findValuta(iso);
            if (valuta != null)
            {
                exchangeRate = valuta.ExchangeRate;
            }

            return exchangeRate;
        }
    }
}
