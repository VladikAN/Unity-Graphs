using System;
using System.Collections.Generic;

namespace Graphs.Core.Domain
{
    public class PathFinder
    {
        private WaypointManager _manager;

        public PathFinder(Connector[] connectors)
        {
            Update(connectors);
        }

        public void Update(Connector[] connectors)
        {
            if (connectors == null || connectors.Length == 0)
            {
                throw new Exception("Connectors cannot be empty");
            }

            _manager = new WaypointManager(connectors);
        }

        public Waypoint[] Find(Waypoint start, Waypoint goal)
        {
            _manager.RefreshAll();
            var startPoint = _manager.Search[start.Name];
            var goalPoint = _manager.Search[goal.Name];

            var current = startPoint;
            while (current != goalPoint)
            {
                current.CloseForUpdates();
                MakeStep(current);

                current = _manager.GetNextOpen();
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
                    result.Add(_manager.Original[p.Name]);
                    p = p.Best;
                }

                result.Reverse();
                return result.ToArray();
            }

            return null;
        }

        private void MakeStep(TempWaypoint point)
        {
            var connectors = _manager.GetConnectors(point);
            foreach (var connector in connectors)
            {
                var p = connector.PointAName == point.Name ? connector.PointB : connector.PointA;
                var next = _manager.Search[p.Name];

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
    }
}