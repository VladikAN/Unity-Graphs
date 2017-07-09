using System.Collections.Generic;
using System.Linq;
using Graphs.Domain;

namespace Graphs.Internal
{
    internal class WaypointManager
    {
        public WaypointManager(Connector[] connectors)
        {
            Connectors = connectors;

            Original = connectors.Select(x => x.PointA)
                .Union(connectors.Select(x => x.PointB))
                .Distinct()
                .ToDictionary(x => x.Name, x => x);

            OnGoing = Original.ToDictionary(x => x.Key, x => new OnGoingWaypoint(x.Value));
        }

        private Connector[] Connectors { get; }
        private IDictionary<string, Waypoint> Original { get; }
        private IDictionary<string, OnGoingWaypoint> OnGoing { get; }

        public Waypoint FindOriginal(string name)
        {
            return Original.ContainsKey(name) ? Original[name] : null;
        }
        
        public OnGoingWaypoint FindOnGoing(string name)
        {
            return OnGoing.ContainsKey(name) ? OnGoing[name] : null;
        }
        
        public void RefreshAll()
        {
            foreach (var point in OnGoing)
            {
                point.Value.Refresh();
            }
        }

        public void UpdateWeights(OnGoingWaypoint point)
        {
            var connectors = GetConnectors(point);
            foreach (var connector in connectors)
            {
                var p = connector.PointAName == point.Name ? connector.PointB : connector.PointA;
                var next = FindOnGoing(p.Name);

                if (next.Closed)
                {
                    continue;
                }

                var weight = point.Weight + connector.Weight;
                if (next.Weight < 0 || next.Weight > weight)
                {
                    next.SetBestNeighbor(point, weight);
                }
            }
        }

        public OnGoingWaypoint GetNextOpen()
        {
            return OnGoing.Values
                .Where(x => !x.Closed && x.Weight >= 0)
                .OrderBy(x => x.Weight)
                .FirstOrDefault();
        }

        private Connector[] GetConnectors(Waypoint point)
        {
            return Connectors
                .Where(x =>
                    (x.PointAName == point.Name && !x.PointB.Blocked)
                    || (x.PointBName == point.Name && !x.PointA.Blocked))
                .ToArray();
        }
    }
}