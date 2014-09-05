using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThermometerUI
{
    class ThermometerMonitor
    {
        public delegate void TemperatureAlert();
        public event TemperatureAlert MaxAllowedTemperatureReached;
        public event TemperatureAlert MinAllowedTemperatureReached;

        private int _currentTemperature;
        private int _minTemperature;
        private int _maxTemperature;
        private int _minAllowedTemperature;
        private int _maxAllowedTemperature;
        private object temperatureLock;

        public int CurrentTemperature
        {
            get
            {
                lock (temperatureLock)
                {
                    return _currentTemperature;
                }
            }
            set
            {
                lock (temperatureLock)
                {
                    _currentTemperature = value;
                    if (_currentTemperature > _maxTemperature)
                    {
                        _maxTemperature = _currentTemperature;

                    }
                    else if (_currentTemperature < _minTemperature)
                    {
                        _minTemperature = _currentTemperature;

                    }
                }

                if (value > _maxAllowedTemperature &&
                    MaxAllowedTemperatureReached != null)
                {
                    MaxAllowedTemperatureReached();
                }
                else if (value < _minAllowedTemperature &&
                    MinAllowedTemperatureReached != null)
                {
                    MinAllowedTemperatureReached();
                }
            }
        }

        public int MinTemperature
        {
            get 
            {
                lock (temperatureLock)
                {
                    return _minTemperature;   
                }
            }
            private set
            {
                lock (temperatureLock)
                {
                    _minTemperature = value;
                }
            }
        }

        public int MaxTemperature
        {
            get 
            {
                lock (temperatureLock)
                {
                    return _maxTemperature;    
                }
            }
            private set
            {
                lock (temperatureLock)
                {
                    _maxTemperature = value;
                }
            }
        }

        public int MinAllowedTemperature
        {
            get 
            {
                lock (temperatureLock)
                {
                    return _minAllowedTemperature;
                }
            }
            set 
            {
                lock (temperatureLock)
                {
                    _minAllowedTemperature = value; 
                }
            }
        }

        public int MaxAllowedTemperature
        {
            get 
            {
                lock (temperatureLock)
                {
                    return _maxAllowedTemperature;
                }
            }
            set 
            {
                lock (temperatureLock)
                {
                    _maxAllowedTemperature = value; 
                }
            }
        }

        public void Clear()
        {
            int currentTemperature = CurrentTemperature;
            MinTemperature = currentTemperature;
            MaxTemperature = currentTemperature;
        }
        
        public ThermometerMonitor()
        {
            temperatureLock = new Object();

            MaxAllowedTemperature = 100;
            MinAllowedTemperature = 0;
        }
    }
}
