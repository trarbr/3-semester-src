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
        private int sleepTime;

        public SodaBeerConsumer(string name, Conveyor conveyor, int sleepTime=100)
        {
            this.name = name;
            this.conveyor = conveyor;
            this.sleepTime = sleepTime;
        }

        public void StartProduction()
        {
            productionRunning = true;

            while (productionRunning)
            {
                Bottle bottle = conveyor.Dequeue();

                Console.WriteLine(String.Format("{0}Consumer got {1} number {2}.", name, 
                    bottle.BottleType, bottle.SerialNumber));

                Thread.Sleep(sleepTime);
            }
        }

        public void StopProduction()
        {
            productionRunning = false;
        }
    }
}
