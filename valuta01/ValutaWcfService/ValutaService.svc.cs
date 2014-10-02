using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;

using ValutaWcfService.Persistence;

namespace ValutaWcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Required)]
    public class ValutaService : IValutaService
    {
        private IPersistence persistence
        {
            get
            {
                if (HttpContext.Current.Application["persistence"] == null)
                {
                    IPersistence persistence = new RavenDbPersistence();
                    persistence.Initialize();
                    HttpContext.Current.Application["persistence"] = persistence;
                }
                return (IPersistence)HttpContext.Current.Application["persistence"];
            }
        }
        private List<Valuta> valutas
        {
            get
            {
                // The service has its own cache of valutas that it keeps up to date 
                if (HttpContext.Current.Application["valutas"] == null)
                {
                    HttpContext.Current.Application["valutas"] = persistence.GetAllValutas();
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

        // Optimistic offline lock
        public bool SetValutaExchangeRate(Valuta valuta)
        {
            bool updated = false;
            HttpContext.Current.Application.Lock();
            Valuta actualValuta = findValuta(valuta.Iso);
            if (valuta.Version == actualValuta.Version)
            {
                valuta.Version++;
                actualValuta.Version = valuta.Version;
                actualValuta.ExchangeRate = valuta.ExchangeRate;
                persistence.UpdateValuta(valuta);
                updated = true;
            }
            HttpContext.Current.Application.UnLock();

            return updated;
        }

        // Only add valuta if it's not already there
        public bool AddValuta(Valuta valuta)
        {
            bool added = false;
            HttpContext.Current.Application.Lock();
            if (findValuta(valuta.Iso) == null)
            {
                valutas.Add(valuta);
                persistence.InsertValuta(valuta);
                added = true;
            }
            HttpContext.Current.Application.UnLock();

            return added;
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
