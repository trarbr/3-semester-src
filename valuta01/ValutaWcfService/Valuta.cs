using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ValutaWcfService
{
    [DataContract]
    public class Valuta
    {
        private object valutaLock; // use this to lock on getters and setters - weird in DTO?

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Iso { get; set; }
        [DataMember]
        public decimal ExchangeRate { get; set; }

        public Valuta(string name, string iso, decimal exchangeRate)
        {
            Name = name;
            Iso = iso;
            ExchangeRate = exchangeRate;
        }
    }
}