using RegionalTimetableApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetableApp.Graph
{
    public class DictGraph
    {
        private Dictionary<Vertex, List<Edge>> graph;

        public DictGraph()
        {
            graph = new Dictionary<Vertex, List<Edge>>();
        }

        internal static DictGraph CreateGraphFromRegionalTimetable(RegionalTimetable regionalTimetable)
        {
            DictGraph graph = new DictGraph();

            foreach (var timetable in regionalTimetable.Timetables)
            {
                // add the first city as vertex
                Vertex fromVertex = graph.AddVertex(timetable.Departures[0].City);
                string fromTime = timetable.Departures[0].Time;

                for (int i = 1; i < timetable.Departures.Count; i++)
                {
                    // add the next city as vertex
                    Vertex toVertex = graph.AddVertex(timetable.Departures[i].City);
                    string toTime = timetable.Departures[i].Time;

                    // add edge between the two of them
                    int travelTime = calculateTravelTimeInMinutes(fromTime, toTime);
                    graph.AddEdge(fromVertex, toVertex, travelTime);

                    fromVertex = toVertex;
                    fromTime = toTime;
                }
            }

            return graph;
        }

        private static int calculateTravelTimeInMinutes(string fromTime, string toTime)
        {
            DateTime from = dateTimeFromString(fromTime);
            DateTime to = dateTimeFromString(toTime);

            // Handle times crossing midnight
            if (from > to)
            {
                to = to.AddDays(1);
            }

            var time = to - from;

            return (int)time.TotalMinutes;
        }

        private static DateTime dateTimeFromString(string time)
        {
            int hours = int.Parse(time.Substring(0, 2));
            int minutes = int.Parse(time.Substring(3, 2));

            return DateTime.MinValue.Add(new TimeSpan(hours, minutes, 0));
        }

        public Vertex AddVertex(string name)
        {
            // check if vertex already exists - assuming all vertices must have unique names 
            var vertex = graph.Keys.FirstOrDefault<Vertex>(v => v.Name == name);
            if (vertex == null)
            {
                vertex = new Vertex() { Name = name };
                var edges = new List<Edge>();
                graph.Add(vertex, edges);
            }

            return vertex;
        }

        public Edge AddEdge(Vertex from, Vertex to, int weight)
        {
            Edge edge = null;

            // Test that the vertices exist
            List<Edge> fromEdges;
            List<Edge> toEdges; 
            bool gotFrom = graph.TryGetValue(from, out fromEdges);
            bool gotTo = graph.TryGetValue(to, out toEdges);

            if (gotFrom && gotTo)
            {
                edge = new Edge() { From = from, To = to, Weight = weight };

                fromEdges.Add(edge);
                toEdges.Add(edge);
            }

            return edge;
        }

        public Tuple<List<Vertex>, List<Edge>> RecursiveMinimumSpanningTreePrim(
            List<Vertex> vertices = null, List<Edge> edges = null)
        {
            if (vertices == null)
            {
                vertices = new List<Vertex>();
                edges = new List<Edge>();

                // pick random starting vertex
                var random = new Random();
                var startingNumber = random.Next(0, graph.Keys.Count);
                var startingVertex = graph.Keys.ToArray<Vertex>()[startingNumber];
                //startingVertex = graph.Keys.ToArray<Vertex>()[0];

                // find shortest path from starting vertex
                var connectedEdges = graph[startingVertex];
                var cheapestEdge = connectedEdges.Min<Edge>();

                edges.Add(cheapestEdge);
                vertices.Add(cheapestEdge.To);
                vertices.Add(cheapestEdge.From);
            }
            else
            {
                // make a list of all edges that we are connected to...
                var connectedEdges = new HashSet<Edge>();
                foreach (var vertex in vertices)
                {
                    foreach (var edge in graph[vertex])
                    {
                        // ...but we have not travelled yet...
                        if (!edges.Contains(edge))
                        {
                            connectedEdges.Add(edge);
                        }
                    }
                }

                //  ...and filter those whose To and From are already in vertices
                var newEdges = connectedEdges.Where<Edge>(
                    e => !(vertices.Contains(e.From) && vertices.Contains(e.To))).ToList<Edge>();

                // now get the cheapest edge...
                var cheapestEdge = newEdges.Min<Edge>();
                edges.Add(cheapestEdge);

                // ...and add the vertex which is not already in the list
                if (vertices.Contains(cheapestEdge.To))
                {
                    vertices.Add(cheapestEdge.From);
                }
                else
                {
                    vertices.Add(cheapestEdge.To);
                }
            }

            // recurse
            if (vertices.Count == graph.Keys.Count)
            {
                return new Tuple<List<Vertex>,List<Edge>>(vertices, edges);
            }
            else
            {
                return RecursiveMinimumSpanningTreePrim(vertices, edges);
            }
        }

        public List<Vertex> BreadthFirstTraversal()
        {
            var traversalResult = new List<Vertex>();
            var traversalQueue = new Queue<Vertex>();
            var startVertex = graph.Keys.First<Vertex>();
            traversalQueue.Enqueue(startVertex);

            while (traversalQueue.Count != 0)
            {
                var vertex = traversalQueue.Dequeue();
                if (traversalResult.Contains(vertex))
                    continue;

                foreach (var edge in graph[vertex])
                {
                    var to = edge.GetOtherVertex(vertex);
                    traversalQueue.Enqueue(to);
                }

                traversalResult.Add(vertex);
            }

            return traversalResult;
        }

        public List<Vertex> RecursiveDepthFirstTraversal(Vertex currentVertex = null, 
            List<Vertex> traversalResult = null)
        {
            traversalResult = traversalResult == null ? new List<Vertex>() : traversalResult;
            currentVertex = currentVertex == null ? graph.Keys.First<Vertex>() : currentVertex;

            traversalResult.Add(currentVertex);

            foreach (var edge in graph[currentVertex])
            {
                var nextVertex = edge.GetOtherVertex(currentVertex);
                if (!traversalResult.Contains(nextVertex))
                    RecursiveDepthFirstTraversal(edge.GetOtherVertex(currentVertex), traversalResult);
            }

            return traversalResult;
        }

        public string GetAsString()
        {
            StringBuilder graphStringBuilder = new StringBuilder();

            foreach (var vertex in graph)
            {
                graphStringBuilder.AppendFormat("{0}: ", vertex.Key.Name);
                foreach (var edge in vertex.Value)
                {
                    var to = edge.GetOtherVertex(vertex.Key);
                    graphStringBuilder.AppendFormat("{0} ({1}) ", to.Name, edge.Weight);
                }
                graphStringBuilder.AppendLine();
            }

            return graphStringBuilder.ToString();
        }
    }
}
