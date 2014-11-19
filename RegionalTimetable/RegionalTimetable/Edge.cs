using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalTimetable
{
    public class Edge
    {
        public Vertex From { get; set; }
        public Vertex To { get; set; }
        public int Weight { get; set; }

        public Vertex GetOtherVertex(Vertex vertex)
        {
            if (From.Name == vertex.Name)
            {
                return To;
            }
            else
            {
                return From;
            }
        }
    }
}
