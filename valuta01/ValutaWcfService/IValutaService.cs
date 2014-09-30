using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ValutaWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IValutaService
    {
        [OperationContract]
        decimal FromDkkToEur(decimal dkkAmount);

        [OperationContract]
        decimal GetExchangeRate(string iso);
    }
}
