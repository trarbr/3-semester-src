using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetable
{
    class Graph
    {
        private string[] cities = new string[] {"Odense", "Middelfart", "Assens", "Ringe", 
            "Svendborg", "Nyborg", "Kerteminde", "Bogense", "Otterup"};

        private int[,] adjencyMatrix = new int[,] { 
            {-1, 67, 59, 45, 90, 58, 33, 49, 22}, 
            {67, -1, 61, -1, -1, -1, -1, 38, -1},
            {59, 61, -1, 43, -1, -1, -1, -1, -1},
            {45, -1, 43, -1, 56, -1, -1, -1, -1},
            {90, -1, -1, 56, -1, 45, -1, -1, -1},
            {58, -1, -1, -1, 45, -1, -1, -1, -1},
            {33, -1, -1, -1, -1, -1, -1, -1, -1},
            {49, 38, -1, -1, -1, -1, -1, -1, 67},
            {22, -1, -1, -1, -1, -1, -1, 67, -1}
        };

        public List<string> GetBreadthFirstTraversal()
        {
            List<string> visitedNodes = new List<string>();
            Queue<string> nodesToVisit = new Queue<string>();

            string startingCity = cities[7];
            nodesToVisit.Enqueue(startingCity);

            while (nodesToVisit.Count != 0)
            {
                string city = nodesToVisit.Dequeue();
                // if the dequeued city has been visited, don't visit it again
                if (!visitedNodes.Contains(city))
                {
                    // Get the index of the city so it can be looked up in the matrix
                    int cityIndex = Array.IndexOf<string>(cities, city);

                    for (int i = 0; i < cities.Length; i++)
                    {
                        // for each city that has a connection, add it to the queue
                        if (adjencyMatrix[cityIndex, i] != -1)
                        {
                            nodesToVisit.Enqueue(cities[i]);
                        }
                    }
                    visitedNodes.Add(city);
                }
            }

            return visitedNodes;
        }

        public string GetMatrixAsString()
        {
            StringBuilder matrixAsString = new StringBuilder();
            int padding = 11;

            matrixAsString.Append("".PadLeft(padding));
            for (int i = 0; i < cities.Length; i++)
            {
                matrixAsString.Append(cities[i].PadLeft(padding));
            }
            matrixAsString.AppendLine();

            for (int i = 0; i < cities.Length; i++)
            {
                matrixAsString.Append(cities[i].PadLeft(padding));
                for (int j = 0; j < cities.Length; j++)
                {
                    matrixAsString.Append(adjencyMatrix[i, j].ToString().PadLeft(padding));
                }
                matrixAsString.AppendLine();
            }

            return matrixAsString.ToString();
        }
    }
}
