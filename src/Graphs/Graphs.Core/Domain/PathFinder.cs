using System.Collections.Generic;
using System.Linq;

namespace Graphs.Core.Domain
{
    public class PathFinder
    {
        public Waypoint[] Find(Connector[] connectors, Waypoint start, Waypoint goal)
        {
            if (connectors == null || connectors.Length == 0)
            {
                return null;
            }

            var manager = new WaypointManager(connectors);
            var startPoint = manager.Search[start.Name];
            var goalPoint = manager.Search[goal.Name];

            var current = startPoint;
            while (current != goalPoint)
            {
                current.Close();
                update(manager, current);

                current = manager.GetNext();
                if (current == null)
                {
                    break;
                }
            }

            if (current == goalPoint)
            {
                var result = new List<Waypoint>();
                var p = goalPoint;
                while (p != null)
                {
                    result.Add(manager.Original[p.Name]);
                    p = p.Best;
                }

                result.Reverse();
                return result.ToArray();
            }

            return null;
        }

        private void update(WaypointManager manager, SearchWaypoint point)
        {
            var neighbors = manager.Connectors.Where(x => x.PointA.Name == point.Name || x.PointB.Name == point.Name);
            foreach (var connector in neighbors)
            {
                var p = connector.PointA.Name == point.Name ? connector.PointB : connector.PointA;
                var next = manager.Search[p.Name];

                if (next.Closed)
                {
                    continue;
                }

                var weight = point.Weight + connector.Weight;
                if (next.Weight < 0 || next.Weight > weight)
                {
                    next.SetBest(point, weight);
                }
            }
        }
    }
}