using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace threads03
{
    class Thermo
    {

        public void Run()
        {
            int alarmCounter = 0;
            while (alarmCounter < 3)
            {
                int temperature = generateTemperature();
                Console.WriteLine("Temperature: {0}", temperature);
                if (temperature < 0 || temperature > 100)
                {
                    Console.WriteLine("Alarm");
                    alarmCounter++;
                }

                Thread.Sleep(2000);
            }
        }

        private int generateTemperature()
        {
            Random generator = new Random();

            int temperature = generator.Next(-20, 120);

            return temperature;
        }
    }
}
