using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SodaBeer
{
    class Conveyor
    {
        private Queue<Bottle> bottleQueue;
        private Object bottleQueueLock;
        private int maxQueueSize;
        private string name;

        public Conveyor(string name, int maxQueueSize)
        {
            bottleQueueLock = new Object();
            bottleQueue = new Queue<Bottle>();
            this.maxQueueSize = maxQueueSize;
            this.name = name;
        }

        public void Enqueue(Bottle bottle)
        {
            lock (bottleQueueLock)
            {
                while (bottleQueue.Count == maxQueueSize)
                {
                    // can't put bottle in
                    Console.WriteLine(String.Format("{0} is full! Queued {1} number {2}", name, 
                        bottle.BottleType, bottle.SerialNumber));
                    Monitor.Wait(bottleQueueLock);
                }

                bottleQueue.Enqueue(bottle);
                //Console.WriteLine(String.Format("{1} number {2} was enqueued at conveyor {0}", 
                //    name, bottle.BottleType, bottle.SerialNumber));

                if (bottleQueue.Count == 1)
                {
                    // we can now dequeue again
                    Monitor.PulseAll(bottleQueueLock);
                }
            }
        }

        public Bottle Dequeue()
        {
            lock (bottleQueueLock)
            {
                while (bottleQueue.Count == 0)
                {
                    // no bottles, can't dequeue
                    Monitor.Wait(bottleQueueLock);
                }

                Bottle bottle = bottleQueue.Dequeue();
                //Console.WriteLine(String.Format("{1} number {2} was dequeued from conveyor {0}", 
                //    name, bottle.BottleType, bottle.SerialNumber));

                if (bottleQueue.Count == (maxQueueSize - 1))
                {
                    // queue is no longer full
                    Console.WriteLine(String.Format("{0} is no longer full! Dequeued {1} number {2}", 
                        name, bottle.BottleType, bottle.SerialNumber));
                    Monitor.PulseAll(bottleQueueLock);
                }

                return bottle;
            }
        }
    }
}
