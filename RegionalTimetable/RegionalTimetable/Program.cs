using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetable
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph g = new Graph();

            //string s = g.GetMatrixAsString();

            //File.WriteAllText("lookatme.txt", s);

            //Console.WriteLine(s);
            g.PrintWidthFirstTraversal();

            Console.ReadLine();
        }
    }
}
