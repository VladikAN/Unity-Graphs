using System;

namespace Graphs.Domain
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
            PointAName = pointA.Name;
            PointB = pointB;
            PointBName = pointB.Name;
            Weight = weight;
        }

        public Waypoint PointA { get; }
        public string PointAName { get; }
        public Waypoint PointB { get; }
        public string PointBName { get; }
        public double Weight { get; }

        public bool Equals(string pointA, string pointB)
        {
            return (PointAName == pointA && PointBName == pointB) || (PointAName == pointB && PointBName == pointA);
        }
    }
}