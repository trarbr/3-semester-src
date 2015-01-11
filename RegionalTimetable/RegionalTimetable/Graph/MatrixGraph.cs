using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetableApp.Graph
{
    class MatrixGraph
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

        public List<Tuple<int, string, string>> GetMinimumSpanningTreePrim()
        {
            List<Tuple<int, string, string>> minimumSpanningTree = new List<Tuple<int, string, string>>();
            List<string> visitedCities = new List<string>();

            // set a starting city
            string startingCity = "Svendborg";
            visitedCities.Add(startingCity);

            while (minimumSpanningTree.Count < cities.Length - 1)
            {
                int shortestTime = int.MaxValue;
                Tuple<int, string, string> shortestEdge = null;

                // for each city in visitedcities, find the shortest edge
                foreach (string city in visitedCities)
                {
                    // get the index (the row to check)
                    int from = Array.IndexOf(cities, city);

                    // loop over all the columns in that row
                    for (int to = 0; to < cities.Length; to++)
                    {
                        int currentTime = adjencyMatrix[from, to];
                        if (currentTime < shortestTime && currentTime >= 0)
                        // if the column has a weight that is lower than shortestTime...
                        {
                            var currentEdge = new Tuple<int, string, string>(currentTime, cities[from], cities[to]);
                            // ...make sure the edge has not been added yet...
                            if (!listContainsEdge(minimumSpanningTree, currentEdge))
                            {
                                // ...and set that as the shortestTime
                                shortestTime = currentTime;
                                shortestEdge = currentEdge;
                            }
                        }
                    }
                }
                // when done, add the shortest edge to the minimumSpanningTree, and the visited city
                minimumSpanningTree.Add(shortestEdge);
                visitedCities.Add(shortestEdge.Item3);
            }

            return minimumSpanningTree;
        }

        private bool edgesAreEqual(Tuple<int, string, string> edge1, Tuple<int, string, string> edge2)
        {
            bool areEqual = false;
            if (edge1.Item2 == edge2.Item2 && edge1.Item3 == edge2.Item3)
            {
                areEqual = true;
            }
            else if (edge1.Item2 == edge2.Item3 && edge1.Item3 == edge2.Item2)
            {
                areEqual = true;
            }

            return areEqual;
        }

        private bool listContainsEdge(List<Tuple<int, string, string>> edges, Tuple<int, string, string> edge)
        {
            bool isInList = false;

            foreach(var item in edges)
            {
                if (edgesAreEqual(item, edge))
                {
                    isInList = true;
                    break;
                }
            }

            return isInList;
        }

        public List<Tuple<int, int>> GetMinimumSpanningTreeKruskal()
        {
            // This is a homecooked version - try making an "official" one with union-set datastructure
            List<Tuple<int, int>> minimumSpanningTree = new List<Tuple<int, int>>();
            List<Tuple<int, int>> uniqueEdges = new List<Tuple<int, int>>();
            List<Tuple<int, int>> allEdges = new List<Tuple<int, int>>();

            // Create a sorted list of all edges
            for (int from = 0; from < cities.Length; from++)
            {
                for (int to = 0; to < cities.Length; to++)
                {
                    if (adjencyMatrix[from, to] > -1)
                    {
                        var currentEdge = new Tuple<int, int>(from, to);
                        var reverseEdge = new Tuple<int, int>(to, from);
                        if (!allEdges.Contains(currentEdge))
                        {
                            allEdges.Add(currentEdge);
                            allEdges.Add(reverseEdge);
                            uniqueEdges.Add(currentEdge);
                        }
                    }
                }
            }
            uniqueEdges.Sort( (a, b) => 
                adjencyMatrix[a.Item1, a.Item2].CompareTo(adjencyMatrix[b.Item1, b.Item2]));

            int[] trees = new int[cities.Length];
            int nextTree = 0;
            for (int i = 0; i < trees.Length; i++)
            {
                trees[i] = -1;
            }
            int edgeNumber = 0;
            while (edgeNumber < uniqueEdges.Count && minimumSpanningTree.Count < (cities.Length - 1))
            {
                var smallestEdge = uniqueEdges[edgeNumber];

                if (trees[smallestEdge.Item1] == -1 && trees[smallestEdge.Item2] == -1)
                {
                    minimumSpanningTree.Add(smallestEdge);
                    // both vertices are not in a tree
                    trees[smallestEdge.Item1] = nextTree;
                    trees[smallestEdge.Item2] = nextTree;
                    nextTree++;
                }
                else if (trees[smallestEdge.Item1] == -1)
                {
                    minimumSpanningTree.Add(smallestEdge);
                    // from should be added to the to tree
                    trees[smallestEdge.Item1] = trees[smallestEdge.Item2];
                }
                else if (trees[smallestEdge.Item2] == -1)
                {
                    minimumSpanningTree.Add(smallestEdge);
                    // to should be added to the from tree
                    trees[smallestEdge.Item2] = trees[smallestEdge.Item1];
                }
                else if (trees[smallestEdge.Item1] != trees[smallestEdge.Item2])
                {
                    // both vertices are in a tree, but not the same, so join the trees
                    for (int i = 0; i < trees.Length; i++)
                    {
                        if (trees[i] == trees[smallestEdge.Item2])
                        {
                            trees[i] = trees[smallestEdge.Item1];
                        }
                    }
                }

                edgeNumber++;
            }

            return minimumSpanningTree;
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
