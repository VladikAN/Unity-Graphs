using System;
using System.Collections.Generic;
using Graphs.Internal;

namespace Graphs.Domain
{
    public class PathFinder
    {
        private WaypointManager _manager;

        public PathFinder(Connector[] connectors)
        {
            InitOrUpdate(connectors);
        }

        public void InitOrUpdate(Connector[] connectors)
        {
            if (connectors == null || connectors.Length == 0)
            {
                throw new Exception("Connectors cannot be empty");
            }

            _manager = new WaypointManager(connectors);
        }

        public Waypoint[] Find(Waypoint start, Waypoint end)
        {
            _manager.RefreshAll();
            var startPoint = _manager.FindOnGoing(start.Name);
            var endPoint = _manager.FindOnGoing(end.Name);

            var current = startPoint;
            while (current != null && current != endPoint)
            {
                current.CloseForUpdates();
                _manager.UpdateWeights(current);
                current = _manager.GetNextOpen();
            }

            // exit if path was not founded
            if (current != endPoint)
            {
                return null;
            }
            
            // restore founded path
            var result = new List<Waypoint>();
            var p = endPoint;
            while (p != null)
            {
                result.Add(_manager.FindOriginal(p.Name));
                p = p.Best;
            }

            result.Reverse();
            return result.ToArray();
        }
    }
}