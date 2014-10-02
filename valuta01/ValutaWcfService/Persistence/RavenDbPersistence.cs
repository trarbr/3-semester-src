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
            store = new EmbeddableDocumentStore { RunInMemory = true };
            //store = new EmbeddableDocumentStore
            //{
            //    DataDirectory = "Data",
            //};
        }

        public void Initialize()
        {
            store.Initialize();
            FakePersistence p = new FakePersistence();
            p.Initialize();
            List<Valuta> valutas = p.GetAllValutas();

            using (IDocumentSession session = store.OpenSession())
            {
                foreach (Valuta valuta in valutas)
                {
                    session.Store(valuta);
                }
                session.SaveChanges();
            }
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
                session.Store(valuta);
                session.SaveChanges();
            }
        }
    }
}