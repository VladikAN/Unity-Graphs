using System;

namespace Graphs.Core.Domain
{
    public class Connector
    {
        public Connector(Waypoint pointA, Waypoint pointB, double weight)
        {
            if (pointA == null || pointB == null)
            {
                throw new Exception("All connections must be specified");
            }

            if (weight < 0)
            {
                throw new Exception("Only positive weight supported");
            }

            PointA = pointA;
            PointB = pointB;
            Weight = weight;
        }

        public Waypoint PointA { get; }
        public Waypoint PointB { get; }
        public double Weight { get; }
    }
}