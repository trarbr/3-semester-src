using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValutaWcfService.Persistence
{
    interface IPersistence
    {
        void Initialize();
        void InsertValuta(Valuta valuta);
        void UpdateValuta(Valuta valuta);
        List<Valuta> GetAllValutas();
    }
}
