using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebAppForWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CalculationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CalculationService.svc or CalculationService.svc.cs at the Solution Explorer and start debugging.
    public class CalculationService : ICalculationService
    {
        public void DoWork()
        {
        }

        public int IntegerAddition(int x, int y)
        {
            return new Random().Next(x, y);
        }

        public int IntegerSubtraction(int x, int y)
        {
            return x - y;
        }
    }
}
