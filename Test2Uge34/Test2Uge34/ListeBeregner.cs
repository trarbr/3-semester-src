using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test2Uge34
{
    class ListeBeregner
    {
        public static double Gennemsnit(List<double> talliste)
        {
            double average = 0;
            double sum = 0;
            double numbers = 0;

            foreach (double number in talliste)
            {
                sum += number;
                numbers++;
            }

            average = sum / numbers;

            return average;
        }

        public static double FindNextSmallestNumber(List<double> numbers)
        {
            double smallestNumber = findSmallestNumber(numbers);
            List<double> biggerNumbers = new List<double>(numbers);
            biggerNumbers.Remove(smallestNumber);
            double nextSmallestNumber = findSmallestNumber(biggerNumbers);

            return nextSmallestNumber;
        }

        public static double CalculateLowerQuartile(List<double> numbers)
        {
            List<double> numbersWorkingCopy = new List<double>(numbers);
            int numbersInQuartile = numbers.Count / 4;
            double currentSmallestNumber;

            List<double> lowestNumbers = new List<double>();

            for (int i = 0; i < numbersInQuartile; i++)
            {
                currentSmallestNumber = findSmallestNumber(numbers);
                lowestNumbers.Add(currentSmallestNumber);
                numbers.Remove(currentSmallestNumber);
            }

            double lowestQuartile = Gennemsnit(lowestNumbers);

            return lowestQuartile;
        }

        private static double findSmallestNumber(List<double> numbers)
        {
            double smallestNumber = numbers[0];

            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i] < smallestNumber)
                {
                    smallestNumber = numbers[i];
                }
            }

            return smallestNumber;
        }
    }
}
