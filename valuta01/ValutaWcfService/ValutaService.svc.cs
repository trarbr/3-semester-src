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
        }

        public decimal FromDkkToEur(decimal dkkAmount)
        {
            decimal euroAmount = 0m;
            HttpContext.Current.Application.Lock();
            decimal dkkToEurRate = findExchangeRate("EUR");
            euroAmount = dkkAmount / dkkToEurRate * 100;
            HttpContext.Current.Application.UnLock();
            

            return euroAmount;
        }

        public decimal GetExchangeRate(string iso)
        {
            decimal exchangeRate;
            HttpContext.Current.Application.Lock();
            exchangeRate = findExchangeRate(iso);
            HttpContext.Current.Application.UnLock();

            return exchangeRate;
        }

        public Valuta[] GetValutas()
        {
            Valuta[] valutasArray;
            HttpContext.Current.Application.Lock();
            valutasArray = valutas.ToArray();
            HttpContext.Current.Application.UnLock();

            return valutasArray;
        }

        public decimal ConvertFromIsoToIso(string fromIso, string toIso, decimal amount)
        {
            decimal newAmount;
            HttpContext.Current.Application.Lock();
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

            HttpContext.Current.Application.UnLock();

            return newAmount;
        }

        public string[] GetDoneConversions()
        {
            string[] conversionsArray;
            HttpContext.Current.Application.Lock();
            conversionsArray = conversions.ToArray();
            HttpContext.Current.Application.UnLock();

            return conversionsArray;
        }

        // try and add a optimistic offline lock here
        // return false if update fails
        public void SetValutaExchangeRate(Valuta valuta)
        {
            HttpContext.Current.Application.Lock();
            Valuta actualValuta = findValuta(valuta.Iso);
            actualValuta.ExchangeRate = valuta.ExchangeRate;
            HttpContext.Current.Application.UnLock();
        }

        // check that the iso is not in the list before adding
        // if it's already there, return false
        public void AddValuta(Valuta valuta)
        {
            HttpContext.Current.Application.Lock();
            valutas.Add(valuta);
            HttpContext.Current.Application.UnLock();
        }

        private Valuta findValuta(string iso)
        {
            Valuta foundValuta = null;
            int i = 0;
            while (foundValuta == null && i < valutas.Count)
            {
                if (valutas[i].Iso.Equals(iso))
                {
                    foundValuta = valutas[i];
                }
                i++;
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
