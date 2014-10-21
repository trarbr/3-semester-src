using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ValutaWebForm.ValutaService;

namespace ValutaWebForm
{
    public class ValutaServiceContainer
    {
        private static ValutaServiceClient valutaService;
        private static readonly object padlock = new object();

        public static ValutaServiceClient ValutaService
        {
            get
            {
                lock (padlock)
                {
                    if (valutaService == null)
                    {
                        valutaService = new ValutaServiceClient();
                    }
                }
                return valutaService;
            }
        }
    }
}