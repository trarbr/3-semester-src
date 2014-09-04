using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SodaBeer
{
    class SodaBeerSplitter
    {
        private volatile bool productionRunning;
        private Conveyor inputConveyor;
        private Conveyor sodaConveyor;
        private Conveyor beerConveyor;
        private int sleepTime;

        public SodaBeerSplitter(Conveyor sodaBeerConveyor, Conveyor sodaConveyor, 
            Conveyor beerConveyor, int sleepTime = 50)
        {
            this.inputConveyor = sodaBeerConveyor;
            this.sodaConveyor = sodaConveyor;
            this.beerConveyor = beerConveyor;
            this.sleepTime = sleepTime;
        }

        public void StartProduction()
        {
            productionRunning = true;

            while (productionRunning)
            {
                Bottle bottle = inputConveyor.Dequeue();

                Console.WriteLine("Splitter took {0} number {1}", bottle.BottleType, 
                    bottle.SerialNumber);

                if (bottle.BottleType == BottleType.Soda)
                {
                    sodaConveyor.Enqueue(bottle);
                }
                else
                {
                    beerConveyor.Enqueue(bottle);
                }

                Thread.Sleep(sleepTime);
            }
        }

        public void StopProduction()
        {
            productionRunning = false;
        }
    }
}
