using RegionalTimetableApp.Graph;
using RegionalTimetableApp.LexicalAnalysis;
using RegionalTimetableApp.Model;
using RegionalTimetableApp.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetableApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestMatrixGraph();

            //TestDictGraph();

            //TestListGraph();

            //TestLexer();

            //TestParser();

            //TestItAll();

            //Console.WriteLine("Kruskal");

            TestMatrixGraphKruskal();

            //Console.WriteLine("Prim");

            //TestMatrixGraphPrim();

            Console.ReadLine();
        }

        private static void TestItAll()
        {
            var result = TestParser();

            if (result.Errors.Count == 0)
            {
                var graph = DictGraph.CreateGraphFromRegionalTimetable(result.RegionalTimetable);
                Console.WriteLine("Printing the generated graph:");
                Console.WriteLine(graph.GetAsString());
                Console.WriteLine("=========");
                Console.WriteLine("Printing hardcoded graph as control:");
                TestDictGraph();
            }
            else
            {
                Console.WriteLine("Error in parsing! Not attempting to make graph!");
            }
        }

        static ParseResult TestParser()
        {
            string filename = @"RKP.txt";
            var realTokenGenerator = new LexerTokenGenerator(new Lexer(new FileCharGenerator(filename)));
            var dummyTokenGenerator = new DummyTokenGenerator();
            var parser = new Parser(dummyTokenGenerator);

            var result = parser.Parse();

            if (result.Errors.Count > 0)
            {
                Console.WriteLine("Errors:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
            }
            else
            {
                Console.WriteLine("No errors!");
            }

            Console.WriteLine("Created objects:");
            foreach (var timetable in result.RegionalTimetable.Timetables)
            {
                Console.WriteLine(timetable);
            }


            return result;

        }

        static void TestLexer()
        {
            //var Lexer = new Lexer(new DummyTokenizer());
            string filename = @"C:\Users\troels\troe3159\3-semester\Hand-out\EBNF_GrafSproglaerer_RuteplanCase\RKP.txt";
            var lexer = new Lexer(new FileCharGenerator(filename));

            var tokens = lexer.GetTokens();
            foreach (var token in tokens)
            {
                if (token.Type != Token.TokenType.Whitespace)
                {
                    Console.WriteLine(token.Type + " " + token.Lexeme + " " + token.LineNo);
                }
            }
        }

        static void TestListGraph()
        {
            ListGraph<string> g = new ListGraph<string>();

            g.AddVertex("Odense");
            g.AddVertex("Kerteminde");
            g.AddVertex("Svendborg");
            g.AddVertex("Ringe");
            g.AddVertex("Bogense");
            g.AddVertex("Otterup");
            g.AddVertex("Middelfart");
            g.AddVertex("Assens");
            g.AddVertex("Nyborg");

            g.AddEdge(33, "Odense", "Kerteminde");
            g.AddEdge(90, "Odense", "Svendborg");
            g.AddEdge(45, "Odense", "Ringe");
            g.AddEdge(49, "Odense", "Bogense");
            g.AddEdge(22, "Odense", "Otterup");
            g.AddEdge(67, "Odense", "Middelfart");
            g.AddEdge(59, "Odense", "Assens");
            g.AddEdge(58, "Odense", "Nyborg");

            g.AddEdge(56, "Svendborg", "Ringe");
            g.AddEdge(45, "Svendborg", "Nyborg");

            g.AddEdge(67, "Bogense", "Otterup");

            g.AddEdge(61, "Middelfart", "Assens");
            g.AddEdge(38, "Middelfart", "Bogense");

            g.AddEdge(43, "Ringe", "Assens");

            Console.WriteLine("Graph as string: ");
            Console.WriteLine(g.GetAsString());

            Console.WriteLine("Breadth first traversal:");
            var bft = g.BreadthFirstTraversal("Svendborg");
            foreach (var vertex in bft)
            {
                Console.WriteLine(vertex);
            }

            Console.WriteLine("Recursive Depth first traversal:");
            var dftr = g.DepthFirstTraversalRecursive("Svendborg");
            foreach (var vertex in dftr)
            {
                Console.WriteLine(vertex);
            }

            Console.WriteLine("Depth first traversal:");
            var dft = g.DepthFirstTraversal("Svendborg");
            foreach (var vertex in dft)
            {
                Console.WriteLine(vertex);
            }

            Console.WriteLine("Minimum Spanning Tree Prim");
            var mstp = g.MinimumSpanningTreePrim("Svendborg");
            foreach (var vertex in mstp)
            {
                Console.WriteLine(vertex);
            }

            Console.WriteLine("Minimum Spanning Tree Kruskal");
            var mstk = g.MinimumSpanningTreeKruskal();
            foreach (var vertex in mstk)
            {
                Console.WriteLine(vertex);
            }

            Console.WriteLine("Dijkstra's shortest path:");
            var origin = "Nyborg";
            var destination = "Bogense";
            var sp = g.ShortestPathDijkstra(origin, destination);
            Console.WriteLine(string.Format("Shortest path from {0} to {1}: {2}", origin, destination, sp.Item1));
            Console.WriteLine("Vertices on the path: ");
            foreach (var vertex in sp.Item2)
            {
                Console.WriteLine(vertex);
            }
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

            g.AddEdge(ringe, assens, 43);

            Console.WriteLine(g.GetAsString());

            //var breadth = g.BreadthFirstTraversal();

            //foreach (var vertex in breadth)
            //{
            //    Console.WriteLine(vertex.Name);
            //}

            //Console.WriteLine("==========");

            //var depth = g.RecursiveDepthFirstTraversal();

            //foreach (var vertex in depth)
            //{
            //    Console.WriteLine(vertex.Name);
            //}

            //Console.WriteLine("==========");

            //var tree = g.RecursiveMinimumSpanningTreePrim();

            //var nodes = tree.Item1;
            //var edges = tree.Item2;

            //foreach (var edge in edges)
            //{
            //    Console.WriteLine("{0}, {1}, {2}", edge.To.Name, edge.From.Name, edge.Weight.ToString());
            //}
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

        static void TestMatrixGraphKruskal()
        {
            MatrixGraph g = new MatrixGraph();

            var mst = g.GetMinimumSpanningTreeKruskal();

            foreach (var item in mst)
            {
                Console.WriteLine(item.Item1.ToString() + " " + item.Item2.ToString() + " " + item.Item3.ToString());
            }
        }

        static void TestMatrixGraphPrim()
        {
            var g = new MatrixGraph();

            var mst = g.GetMinimumSpanningTreePrim();
            foreach (var item in mst)
            {
                Console.WriteLine(item.Item1.ToString() + " " + item.Item2.ToString() + " " + item.Item3.ToString());
            }
        }
    }
}
