﻿using Raven.Client;
using Raven.Client.Document;
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
        private bool runsInMemory;

        public RavenDbPersistence(bool runInMemory)
        {
            if (runInMemory)
            {
                store = new EmbeddableDocumentStore { RunInMemory = true };
                runsInMemory = true;
            }
            else
            {
                //store = new EmbeddableDocumentStore { DataDirectory = "Data" };
                store = new DocumentStore 
                { 
                    Url = "http://localhost:8080",
                    DefaultDatabase = "ValutaDB"
                };
            }
        }

        public void Initialize()
        {
            store.Initialize();
            if (runsInMemory)
            {
                // feed it starter values from fakepersistence
                IPersistence fakes = new FakePersistence();
                fakes.Initialize();
                var valutas = fakes.GetAllValutas();
                foreach (var valuta in valutas)
                {
                    saveValuta(valuta);
                }
            }
        }

        public void InsertValuta(Valuta valuta)
        {
            saveValuta(valuta);
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
            saveValuta(valuta);
        }

        private void saveValuta(Valuta valuta)
        {
            using (IDocumentSession session = store.OpenSession())
            {
                session.Store(valuta);
                session.SaveChanges();
            }
        }
    }
}