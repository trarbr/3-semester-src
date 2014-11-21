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
            TestMatrixGraph();

            Console.ReadLine();
        }

        static void TestDictGraph()
        {
            DictGraph g = new DictGraph();

            var odense = g.AddVertex("Odense");
            var kerteminde = g.AddVertex("Kerteminde");
            var svendborg = g.AddVertex("Svendborg");
            var ringe = g.AddVertex("Ringe");
            var bogense = g.AddVertex("Bogense");
            var otterup = g.AddVertex("Otterup");
            var middelfart = g.AddVertex("Middelfart");
            var assens = g.AddVertex("Assens");
            var nyborg = g.AddVertex("Nyborg");

            g.AddEdge(odense, kerteminde, 33);
            g.AddEdge(odense, svendborg, 90);
            g.AddEdge(odense, ringe, 45);
            g.AddEdge(odense, bogense, 49);
            g.AddEdge(odense, otterup, 22);
            g.AddEdge(odense, middelfart, 67);
            g.AddEdge(odense, assens, 59);
            g.AddEdge(odense, nyborg, 58);

            g.AddEdge(svendborg, ringe, 56);
            g.AddEdge(svendborg, nyborg, 45);

            g.AddEdge(bogense, otterup, 67);

            g.AddEdge(middelfart, assens, 61);
            g.AddEdge(middelfart, bogense, 38);

            Console.WriteLine(g.GetAsString());

            var breadth = g.BreadthFirstTraversal();

            foreach (var vertex in breadth)
            {
                Console.WriteLine(vertex.Name);
            }

            Console.WriteLine("==========");

            var depth = g.RecursiveDepthFirstTraversal();

            foreach (var vertex in depth)
            {
                Console.WriteLine(vertex.Name);
            }
        }

        static void TestMatrixGraph()
        {
            MatrixGraph g = new MatrixGraph();

            string s = g.GetMatrixAsString();

            File.WriteAllText("graph_as_matrix.txt", s);

            Console.WriteLine(s);
            var cities = g.GetBreadthFirstTraversal();

            foreach (var city in cities)
            {
                Console.WriteLine(city);
            }
        }
    }
}
