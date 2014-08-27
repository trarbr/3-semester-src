using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace threads04
{
    class Program
    {
        static int counter;

        static void Main(string[] args)
        {
            Thread thread1 = new Thread(addTwo);
            Thread thread2 = new Thread(SubtractOne);

            thread1.Start();
            thread2.Start();

            Console.ReadLine();
        }

        static void addTwo()
        {
            for (int i = 0; i < 100; i++)
            {
                lock ((object)counter)
                {
                    counter += 2;
                    Console.WriteLine(counter);
                }
                Thread.Sleep(100);
            }
        }

        static void SubtractOne()
        {
            for (int i = 0; i < 100; i++)
            {
                lock ((object)counter)
                {
                    counter += -1;
                    Console.WriteLine(counter);
                }
                Thread.Sleep(100);
            }
        }
    }
}
