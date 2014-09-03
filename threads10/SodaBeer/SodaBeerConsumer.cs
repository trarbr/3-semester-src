using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SodaBeer
{
    class SodaBeerConsumer
    {
        private volatile bool productionRunning;
        private string name;
        private Conveyor conveyor;

        public SodaBeerConsumer(string name, Conveyor conveyor)
        {
            this.name = name;
            this.conveyor = conveyor;
        }

        public void StartProduction()
        {
            productionRunning = true;

            while (productionRunning)
            {
                Bottle bottle = conveyor.Dequeue();

                Console.WriteLine(String.Format("{0}Consumer got {1} number {2}.", name, 
                    bottle.BottleType, bottle.SerialNumber));

                Thread.Sleep(1000);
            }
        }

        public void StopProduction()
        {
            productionRunning = false;
        }
    }
}
