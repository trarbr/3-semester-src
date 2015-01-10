using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetableApp.Graph
{
    class ListGraph<T>
    {
        private List<T> vertices;
        private List<List<ListEdge<T>>> graph;

        public ListGraph()
        {
            vertices = new List<T>();
            graph = new List<List<ListEdge<T>>>();
        }

        public bool AddVertex(T vertex)
        {
            bool vertexAdded = false;

            if (!vertices.Contains(vertex))
            {
                vertices.Add(vertex);
                graph.Add(new List<ListEdge<T>>());
                vertexAdded = true;
            }

            return vertexAdded;
        }

        public bool AddEdge(int weight, T from, T to)
        {
            bool edgeAdded = false;
            int fromIndex = vertices.IndexOf(from);
            int toIndex = vertices.IndexOf(to);

            if (fromIndex != -1 && toIndex != -1)
            {
                graph[fromIndex].Add(new ListEdge<T>(weight, to));
                graph[toIndex].Add(new ListEdge<T>(weight, from));
                edgeAdded = true;
            }

            return edgeAdded;
        }

        public string GetAsString()
        {
            StringBuilder graphStringBuilder = new StringBuilder();

            for (int i = 0; i < vertices.Count; i++)
            {
                graphStringBuilder.AppendFormat("{0}: ", vertices[i].ToString());

                foreach (var edge in graph[i])
                {
                    graphStringBuilder.AppendFormat(" {0} |", edge);
                }
                graphStringBuilder.AppendLine();
            }

            return graphStringBuilder.ToString();
        }

        public List<T> BreadthFirstTraversal(T startingVertex)
        {
            // Make a list of vertices that have been traversed...
            var traversedVertices = new List<T>();
            // ...and a queue of vertices to visit.
            var verticesToVisit = new Queue<T>();

            // Add a starting vertex to visit
            verticesToVisit.Enqueue(startingVertex);

            // Loop until all vertices have been visited
            while (verticesToVisit.Count > 0)
            {
                // Dequeue the next vertex
                T vertex = verticesToVisit.Dequeue();

                // Make sure the vertex wasn't already visited
                if (!traversedVertices.Contains(vertex))
                {
                    int vertexIndex = vertices.IndexOf(vertex);

                    // Add all vertices that are connected to the vertex to the queue 
                    // of vertices to visit
                    foreach (var edge in graph[vertexIndex])
                    {
                        verticesToVisit.Enqueue(edge.To);
                    }

                    // Add the vertex to the list of traversed vertices
                    traversedVertices.Add(vertex);
                }
            }

            return traversedVertices;
        }

        // Same as BreadthFirst but uses stack instead
        public List<T> DepthFirstTraversal(T startingVertex)
        {
            // Make a list of vertices that have been traversed...
            var traversedVertices = new List<T>();
            // ...and a stack of vertices to visit.
            var verticesToVisit = new Stack<T>();

            // Add a starting vertex to visit
            verticesToVisit.Push(startingVertex);

            // Loop until all vertices have been visited
            while (verticesToVisit.Count > 0)
            {
                // Pop the next vertex
                T vertex = verticesToVisit.Pop();

                // Make sure the vertex wasn't already visited
                if (!traversedVertices.Contains(vertex))
                {
                    int vertexIndex = vertices.IndexOf(vertex);

                    // Add all vertices that are connected to the vertex to the stack 
                    // of vertices to visit
                    foreach (var edge in graph[vertexIndex])
                    {
                        verticesToVisit.Push(edge.To);
                    }
                    traversedVertices.Add(vertex);
                }
            }

            return traversedVertices;
        }

        public List<T> DepthFirstTraversalRecursive(T startingVertex)
        {
            var traversedVertices = new List<T>();
            dftRecurse(startingVertex, traversedVertices);

            return traversedVertices;
        }

        // Recursive helper method for depth first traversal
        private void dftRecurse(T currentVertex, List<T> traversedVertices)
        {
            // Add the current vertices to list of traversed vertices
            traversedVertices.Add(currentVertex);
            int currentVertexIndex = vertices.IndexOf(currentVertex);

            // For each edge on the current vertex...
            foreach (var edge in graph[currentVertexIndex])
            {
                var nextVertex = edge.To;
                // ...if the edge is connected to a non-traversed vertex...
                if (!traversedVertices.Contains(nextVertex))
                {
                    // ...traverse that vertex
                    dftRecurse(nextVertex, traversedVertices);
                }
            }
        }

        // Man vedligeholder hele tiden en liste med byer man har været i
        // Ud fra denne liste vælger man den forbundne kant, som har den mindste vægt og som
        // ikke leder til en by der allerede er forbundet
        public List<ListEdge<T>> MinimumSpanningTreePrim(T startingVertex)
        {
            List<ListEdge<T>> minimumSpanningTree = new List<ListEdge<T>>();
            List<T> visitedVertices = new List<T>();

            visitedVertices.Add(startingVertex);

            while (minimumSpanningTree.Count < vertices.Count - 1)
            {
                // make a list of all reachable edges, which are not already in the tree
                // and which lead to a unvisited vertex
                List<ListEdge<T>> reachableEdges = new List<ListEdge<T>>();
                foreach (var vertex in visitedVertices)
                {
                    int vertexIndex = vertices.IndexOf(vertex);
                    foreach (var edge in graph[vertexIndex])
                    {
                        if (!minimumSpanningTree.Contains(edge) && !visitedVertices.Contains(edge.To))
                        {
                            reachableEdges.Add(edge);
                        }
                    }
                }
                // sort the list
                var sortedEdges = reachableEdges.OrderBy<ListEdge<T>, int>(edge => edge.Weight);
                // pick the edge with the lowest weight
                var lightestEdge = sortedEdges.First();
                // add it to the minimum spanning tree
                minimumSpanningTree.Add(lightestEdge);
                visitedVertices.Add(lightestEdge.To);
            }

            return minimumSpanningTree;
        }

        public List<ListEdge<T>> MinimumSpanningTreeKruskal()
        {
            List<ListEdge<T>> minimumSpanningTree = new List<ListEdge<T>>();
            List<ListEdge<T>> allAddedEdges = new List<ListEdge<T>>();

            List<List<T>> trees = new List<List<T>>();
            while (minimumSpanningTree.Count < (vertices.Count - 1))
            {
                int smallestWeight = int.MaxValue;
                ListEdge<T> lightestEdge = null;
                T from = default(T);

                foreach (var vertex in vertices)
                {
                    int vertexIndex = vertices.IndexOf(vertex);
                    foreach (var edge in graph[vertexIndex])
                    {
                        if (edge.Weight < smallestWeight && !allAddedEdges.Contains(edge))
                        {
                            smallestWeight = edge.Weight;
                            lightestEdge = edge;
                            from = vertex;
                        }
                    }
                }

                allAddedEdges.Add(lightestEdge);
                var to = lightestEdge.To;
                int indexTo = vertices.IndexOf(to);
                var reverseEdge = graph[indexTo]
                    .Where(e => e.Weight == lightestEdge.Weight && e.To.Equals(from))
                    .First();
                allAddedEdges.Add(reverseEdge);

                // now grow trees
                bool treeFound = false;
                foreach (var tree in trees)
                {
                    bool fromInTree = tree.Contains(from);
                    bool toInTree = tree.Contains(to); 
                    if (fromInTree && toInTree)
                    {
                        // adding the edge will create a cycle, so do nothing
                        treeFound = true;
                        break;
                    }
                    else if (fromInTree)
                    {
                        minimumSpanningTree.Add(lightestEdge);
                        tree.Add(to);
                        treeFound = true;
                        break;
                    }
                    else if (toInTree)
                    {
                        minimumSpanningTree.Add(lightestEdge);
                        tree.Add(from);
                        treeFound = true;
                        break;
                    }
                }
                if (!treeFound)
                {
                    minimumSpanningTree.Add(lightestEdge);
                    // create new tree
                    List<T> tree = new List<T>();
                    tree.Add(from);
                    tree.Add(to);
                }
            }

            return minimumSpanningTree;
        }

        public Tuple<int, List<T>> ShortestPathDijkstra(T origin, T destination)
        {
            // previous is used to build shortestPath
            var previous = new Dictionary<T, T>();

            // Create the set of unvisited vertices
            var verticesToVisit = new List<T>(vertices);
            verticesToVisit.Remove(origin);

            // Create the initial set of weight values, initially they are all infinite
            // except for origin, which is 0
            var lowestWeightToVertex = new Dictionary<T, int>();
            foreach (var vertex in vertices)
            {
                lowestWeightToVertex[vertex] = int.MaxValue;
            }
            lowestWeightToVertex[origin] = 0;

            var currentVertex = origin;
            bool destinationReached = false;
            // Assume that the destination will be found
            while (!destinationReached)
            {
                int currentVertexIndex = vertices.IndexOf(currentVertex);
                // for each connected edge, if edge.To has not been visited, 
                // calculate the weight from origin to edge.To
                // and if the new weight is lower than the one in the weight dict, replace it
                foreach (var edge in graph[currentVertexIndex])
                {
                    if (verticesToVisit.Contains(edge.To))
                    {
                        int newWeightToVertex = lowestWeightToVertex[currentVertex] + edge.Weight;
                        if (newWeightToVertex < lowestWeightToVertex[edge.To])
                        {
                            lowestWeightToVertex[edge.To] = newWeightToVertex;
                            previous[edge.To] = currentVertex;
                        }
                    }
                }
                verticesToVisit.Remove(currentVertex);

                // determine which vertex to travel to next. Following must be true:
                // 1. Vertex not visited before (not in verticesToVisit)
                // 2. Has the lowest weight in the weight dict
                int minimumWeight = int.MaxValue;
                var nextVertex = default(T);
                foreach (var vertexWeightPair in lowestWeightToVertex)
                {
                    if (vertexWeightPair.Value < minimumWeight && verticesToVisit.Contains(vertexWeightPair.Key))
                    {
                        minimumWeight = vertexWeightPair.Value;
                        nextVertex = vertexWeightPair.Key;
                    }
                }

                // if the next vertex is the destination, we are done.
                if (nextVertex.Equals(destination))
                {
                    destinationReached = true;
                }

                currentVertex = nextVertex;
            }

            // construct shortestPath here, using sp = prev[destination] + prev[prev[destination]]
            var shortestPath = new List<T>();
            var v = destination;
            while (!v.Equals(origin))
            {
                shortestPath.Add(v);
                v = previous[v];
            }
            shortestPath.Add(origin);
            shortestPath.Reverse();

            return new Tuple<int, List<T>>(lowestWeightToVertex[destination], shortestPath);
        }

        public class ListEdge<E>
        {
            public readonly int Weight;
            public readonly E To;

            public ListEdge(int weight, E to)
            {
                Weight = weight;
                To = to;
            }

            public override string ToString()
            {
                return string.Format("{0} ({1})", To, Weight);
            }
        }
    }
}
