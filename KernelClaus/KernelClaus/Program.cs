using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KernelClaus
{
    class Program
    {
        static int elfCounter;
        static int reindeerCounter;
        static Mutex theDoor;
        static Mutex elfMutex;
        static SemaphoreSlim santaSemaphore;
        static SemaphoreSlim elfSemaphore;
        static SemaphoreSlim reindeerSemaphore;
        
        static void Main(string[] args)
        {
            theDoor = new Mutex();
            elfMutex = new Mutex();
            santaSemaphore = new SemaphoreSlim(0);
            elfSemaphore = new SemaphoreSlim(0);
            reindeerSemaphore = new SemaphoreSlim(0);

            Thread santaThread = new Thread(goSanta);
            santaThread.Start();

            //for (int i = 0; i < 9; i++)
            //{
            //    Thread.Sleep(500);
            //    new Thread(goReindeer).Start();
            //}

            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(1000);
                new Thread(goElves).Start();
            }


            Console.ReadLine();
        }

        static void goSanta()
        {
            santaSemaphore.Wait();
            Console.WriteLine("Santa got signal!");
            theDoor.WaitOne();
            if (reindeerCounter == 9)
            {
                reindeerCounter = 0;
                prepSleigh();
                reindeerSemaphore.Release(9);
            }
            else
            {
                elfSemaphore.Release(3);
                helpElves();
            }

            theDoor.ReleaseMutex();
        }

        static void goReindeer()
        {
            theDoor.WaitOne();
            reindeerCounter++;
            if (reindeerCounter == 9)
            {
                santaSemaphore.Release();
            }
            theDoor.ReleaseMutex();
            reindeerSemaphore.Wait();
            getHitched();
        }

        static void goElves()
        {
            elfMutex.WaitOne();
            theDoor.WaitOne();
            elfCounter++;   
            if (elfCounter == 3)
            {
                santaSemaphore.Release();
                Console.WriteLine("3 elves");
            }
            else
            {
                elfMutex.ReleaseMutex();
            }
            theDoor.ReleaseMutex();
            elfSemaphore.Wait();
            getHelp();
            theDoor.WaitOne();
            elfCounter--;
            if (elfCounter == 0)
            {
                elfMutex.ReleaseMutex();
            }
            theDoor.ReleaseMutex();
            Console.WriteLine("Elf done");
        }

        private static void getHelp()
        {
            Console.WriteLine("Getting help");
        }

        private static void helpElves()
        {
            Console.WriteLine("Helping elves");
        }

        private static void prepSleigh()
        {
            Console.WriteLine("Prepping sleigh");
        }

        private static void getHitched()
        {
            Console.WriteLine("Getting hitched");
        }
    }
}
