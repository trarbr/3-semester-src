﻿using System;
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

        public List<Tuple<int, string, string>> GetMinimumSpanningTreeKruskal()
        {
            // This is a homecooked version - try making an "official" one with union-set datastructure
            List<Tuple<int, string, string>> minimumSpanningTree = new List<Tuple<int, string, string>>();
            List<Tuple<int, string, string>> allEdges = new List<Tuple<int, string, string>>();

            // Create a sorted list of all edges
            // NOTE: Instead of using a listContainsEdge function
            // you can use two lists, one called allEdges and one called uniqueEdges
            // and then add both [weight, i, j] and [weight, j, i] to allEdges but only one to
            // uniqueEdges
            for (int i = 0; i < cities.Length; i++)
            {
                for (int j = 0; j < cities.Length; j++)
                {
                    int currentWeight = adjencyMatrix[i, j];
                    var currentEdge = new Tuple<int, string, string>(currentWeight, cities[i], cities[j]);
                    // if edge exists (not -1) and it has not been added
                    if (currentWeight != -1 && !listContainsEdge(allEdges, currentEdge))
                    {
                        allEdges.Add(currentEdge);
                    }
                }
            }
            allEdges.Sort( (a, b) => a.Item1.CompareTo(b.Item1) );

            List<List<string>> trees = new List<List<string>>();
            int k = 0;
            while (k < allEdges.Count && minimumSpanningTree.Count < (cities.Length - 1))
            {
                var smallestEdge = allEdges[k];

                bool treeFound = false;
                foreach (var tree in trees)
                {
                    bool item2InTree = tree.Contains(smallestEdge.Item2);
                    bool item3InTree = tree.Contains(smallestEdge.Item3);
                    if (item2InTree && item3InTree)
                    {
                        // adding the edge will create a cycle, so do nothing
                        treeFound = true;
                        break;
                    }
                    else if (item2InTree)
                    {
                        minimumSpanningTree.Add(smallestEdge);
                        // add item3 to the tree
                        tree.Add(smallestEdge.Item3);
                        treeFound = true;
                        break;
                    }
                    else if (item3InTree)
                    {
                        minimumSpanningTree.Add(smallestEdge);
                        // add item2 to the tree
                        tree.Add(smallestEdge.Item2);
                        treeFound = true;
                        break;
                    }
                }
                if (!treeFound)
                {
                    minimumSpanningTree.Add(smallestEdge);
                    // create new tree
                    List<string> tree = new List<string>();
                    tree.Add(smallestEdge.Item2);
                    tree.Add(smallestEdge.Item3);
                    trees.Add(tree);
                }

                k++;
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
