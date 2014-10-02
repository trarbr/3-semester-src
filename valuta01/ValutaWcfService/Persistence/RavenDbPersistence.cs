using Raven.Client;
using Raven.Client.Embedded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValutaWcfService.Persistence
{
    public class RavenDbPersistence : IPersistence
    {
        private IDocumentStore store;

        public RavenDbPersistence()
        {
            store = new EmbeddableDocumentStore{RunInMemory = true};
        }

        public void Initialize()
        {
            store.Initialize();
        }

        public void InsertValuta(Valuta valuta)
        {
            using (IDocumentSession session = store.OpenSession())
            {
                session.Store(valuta);
                session.SaveChanges();
            }
        }

        public List<Valuta> GetAllValutas()
        {
            List<Valuta> valutas;

            using (IDocumentSession session = store.OpenSession())
            {
                // This only fetches a max of 128 documents
                // RavenDB wants pagination
                valutas = session.Query<Valuta>().ToList();
            }

            return valutas;
        }

        public void UpdateValuta(Valuta valuta)
        {
            using (IDocumentSession session = store.OpenSession())
            {
                Valuta oldValuta = session.Load<Valuta>(0); // how to handle ID?
                oldValuta.Name = valuta.Name;
                oldValuta.Iso = valuta.Iso;
                oldValuta.ExchangeRate = valuta.ExchangeRate;
                session.SaveChanges();
            }
        }
    }
}