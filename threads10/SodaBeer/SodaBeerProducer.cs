using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SodaBeer
{
    class SodaBeerProducer
    {
        private volatile bool productionRunning;
        private int nextBeerSerialNumber;
        private int nextSodaSerialNumber;
        private Conveyor conveyor;
        private Random bottleTypeGenerator;
        private int sleepTime;

        public SodaBeerProducer(Conveyor conveyor, int sleepTime = 50)
        {
            this.conveyor = conveyor;
            nextBeerSerialNumber = 1;
            nextSodaSerialNumber = 1;
            bottleTypeGenerator = new Random();
            this.sleepTime = sleepTime;
        }

        public void StartProduction()
        {
            productionRunning = true;

            while (productionRunning)
            {
                BottleType bottleType = getRandomBottleType();
                int serialNumber = getNextSerialNumber(bottleType);
                Bottle bottle = new Bottle(bottleType, serialNumber);
                conveyor.Enqueue(bottle);

                Thread.Sleep(sleepTime);
            }

            Console.WriteLine("==> SodaBeerProducer: shutdown completed.");
        }

        public void StopProduction()
        {
            Console.WriteLine("==> SodaBeerProducer: shutdown forced.");
            Thread.Sleep(2 * sleepTime);
            productionRunning = false;
        }

        private int getNextSerialNumber(BottleType bottleType)
        {
            int nextSerialNumber = 0;

            if (bottleType == BottleType.Soda)
            {
                nextSerialNumber = nextSodaSerialNumber;
                nextSodaSerialNumber++;
            }
            else
            {
                nextSerialNumber = nextBeerSerialNumber;
                nextBeerSerialNumber++;
            }

            return nextSerialNumber;
        }

        private BottleType getRandomBottleType()
        {
            int random = bottleTypeGenerator.Next(0, 2);
            BottleType bottleType = BottleType.Beer;

            if (random == 1)
            {
                bottleType = BottleType.Soda;
            }

            return bottleType;
        }
    }
}
