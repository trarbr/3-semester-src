using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThermometerUI
{
    class TemperatureSensor
    {
        private volatile bool on;
        private ThermometerMonitor thermometer;

        public TemperatureSensor(ThermometerMonitor thermometer)
        {
            this.thermometer = thermometer;
        }

        public void On()
        {
            on = true;

            Random generator = new Random();

            while (on)
            {
                thermometer.CurrentTemperature = generator.Next(-20, 120);

                Thread.Sleep(100);
            }
        }

        public void Off()
        {
            on = false;
        }


    }
}
