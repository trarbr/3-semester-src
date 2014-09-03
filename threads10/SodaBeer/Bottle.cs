using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaBeer
{
    class Bottle
    {
        public BottleType BottleType { get; private set; }
        public int SerialNumber { get; private set; }

        public Bottle(BottleType bottleType, int serialNumber)
        {
            BottleType = bottleType;
            SerialNumber = serialNumber;
        }
    }
}
