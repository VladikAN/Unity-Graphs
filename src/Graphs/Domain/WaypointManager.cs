using System.Collections.Generic;
using System.Linq;

namespace Graphs.Domain
{
    public class WaypointManager
    {
        public WaypointManager(Connector[] connectors)
        {
            Connectors = connectors;

            Original = connectors.Select(x => x.PointA)
                .Union(connectors.Select(x => x.PointB))
                .Distinct()
                .ToDictionary(x => x.Name, x => x);

            Search = Original.ToDictionary(x => x.Key, x => new TempWaypoint(x.Value));
        }

        public Connector[] Connectors { get; private set; }
        public IDictionary<string, Waypoint> Original { get; private set; }
        public IDictionary<string, TempWaypoint> Search { get; private set; }

        public void RefreshAll()
        {
            foreach (var searchWaypoint in Search)
            {
                searchWaypoint.Value.Refresh();
            }
        }

        public TempWaypoint GetNextOpen()
        {
            return Search.Values
                .Where(x => !x.Closed && x.Weight >= 0)
                .OrderBy(x => x.Weight)
                .FirstOrDefault();
        }

        public Connector[] GetConnectors(Waypoint point)
        {
            return Connectors
                .Where(x =>
                    (x.PointAName == point.Name && !x.PointB.Blocked)
                    || (x.PointBName == point.Name && !x.PointA.Blocked))
                .ToArray();
        }
    }
}