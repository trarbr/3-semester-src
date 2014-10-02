using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValutaWcfService.Persistence
{
    public class FakePersistence : IPersistence
    {
        private List<Valuta> valutas;

        public void Initialize()
        {
            valutas = new List<Valuta>()
            {
                new Valuta("Canada", "CAD", 492.27m),
                new Valuta("Euro", "EUR", 745.99m),
                new Valuta("Storbritannien", "GBP", 947.99m),
                new Valuta("Norge", "NOK", 90.34m),
                new Valuta("Sverige", "SEK", 78.21m),
                new Valuta("Amerika", "USD", 524.02m),
            };
        }

        public void InsertValuta(Valuta valuta)
        {
        }

        public List<Valuta> GetAllValutas()
        {
            return valutas;
        }

        public void UpdateValuta(Valuta valuta)
        {
        }
    }
}