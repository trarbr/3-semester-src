using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThermometerUI
{
    class TemperatureGenerator
    {
        private volatile bool generatingTemperatures;
        private ThermometerMonitor thermometer;

        public TemperatureGenerator(ThermometerMonitor thermometer)
        {
            this.thermometer = thermometer;
        }

        public void Generate()
        {
            generatingTemperatures = true;

            Random generator = new Random();

            while (generatingTemperatures)
            {
                thermometer.CurrentTemperature = generator.Next(-20, 120);

                Thread.Sleep(100);
            }
        }

        public void StopGenerating()
        {
            generatingTemperatures = false;
        }


    }
}
