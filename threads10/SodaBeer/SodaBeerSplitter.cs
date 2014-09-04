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
        private Conveyor sodaBeerConveyor;
        private Conveyor sodaConveyor;
        private Conveyor beerConveyor;
        private int sleepTime;

        public SodaBeerSplitter(Conveyor sodaBeerConveyor, Conveyor sodaConveyor, 
            Conveyor beerConveyor, int sleepTime = 50)
        {
            this.sodaBeerConveyor = sodaBeerConveyor;
            this.sodaConveyor = sodaConveyor;
            this.beerConveyor = beerConveyor;
            this.sleepTime = sleepTime;
        }

        public void StartProduction()
        {
            productionRunning = true;

            while (productionRunning)
            {
                Bottle bottle = sodaBeerConveyor.Dequeue();

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

            Console.WriteLine("==> SodaBeerSplitter: shutdown completed.");
        }

        public void EmergencyStopProduction()
        {
            productionRunning = false;
        }

        public void StopProduction()
        {
            Console.WriteLine("==> SodaBeerSplitter: shutdown requested.");
            while (sodaBeerConveyor.CurrentQueueSize != 0)
            {
                // wait until conveyor is empty
                Thread.Sleep(sleepTime);
            }

            productionRunning = false;
        }
    }
}
