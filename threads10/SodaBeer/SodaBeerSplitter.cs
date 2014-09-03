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

        public SodaBeerSplitter(Conveyor sodaBeerConveyor, Conveyor sodaConveyor, Conveyor beerConveyor)
        {
            this.inputConveyor = sodaBeerConveyor;
            this.sodaConveyor = sodaConveyor;
            this.beerConveyor = beerConveyor;
        }

        public void StartProduction()
        {
            productionRunning = true;

            while (productionRunning)
            {
                Bottle bottle = inputConveyor.Dequeue();

                if (bottle.BottleType == BottleType.Soda)
                {
                    sodaConveyor.Enqueue(bottle);
                }
                else
                {
                    beerConveyor.Enqueue(bottle);
                }

                Thread.Sleep(500);
            }
        }

        public void StopProduction()
        {
            productionRunning = false;
        }
    }
}
