using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1Uge34
{
    public class Person
    {
        private double _weight;
        private double _height;

        public string Name { get; private set; }
        public double Weight
        {
            get { return _weight; }
            private set
            {
                if (value < 0)
                {
                    _weight = 0;
                }
                else
                {
                    _weight = value;
                }
            }
        }
        public double Height
        {
            get { return _height; }
            private set
            {
                if (value < 0)
                {
                    _height = 0;
                }
                else
                {
                    _height = value;
                }
            }
        }

        public Person(string name, double weight, double height)
        {
            Name = name;
            Weight = weight;
            Height = height;
        }

        // Name of method should start with Verb, but assignment says it must be BMI - sorry!
        public double BMI()
        {
            return Weight / (Math.Pow(Height, 2));
        }

        public string GiveWeightAdvice()
        {
            string advice = "";

            double bmi = BMI();

            if (bmi < 18.5)
            {
                advice = "Underweight";
            }
            else if (18.5 < bmi && bmi < 25)
            {
                advice = "Normal weight";
            }
            else if (25 < bmi && bmi < 30)
            {
                advice = "Overweight";
            }
            else if (30 < bmi)
            {
                advice = "Very overweight";
            }

            return advice;
        }
    }
}
