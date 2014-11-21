using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetable
{
    public class DictGraph
    {
        private Dictionary<Vertex, List<Edge>> graph;

        public DictGraph()
        {
            graph = new Dictionary<Vertex, List<Edge>>();
        }

        public Vertex AddVertex(string name)
        {
            var vertex = new Vertex() { Name = name };
            var edges = new List<Edge>();

            graph.Add(vertex, edges);

            return vertex;
        }

        public Edge AddEdge(Vertex from, Vertex to, int weight)
        {
            var edge = new Edge() { From = from, To = to, Weight = weight };

            graph[from].Add(edge);
            graph[to].Add(edge);

            return edge;
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
