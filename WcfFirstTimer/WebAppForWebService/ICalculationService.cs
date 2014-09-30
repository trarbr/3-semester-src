using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebAppForWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICalculationService" in both code and config file together.
    [ServiceContract]
    public interface ICalculationService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        int IntegerAddition(int x, int y);

        [OperationContract]
        int IntegerSubtraction(int x, int y);
    }
}
