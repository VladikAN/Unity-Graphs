using System.Collections.Generic;
using System.Linq;

namespace Graphs.Core.Domain
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

            Search = Original.ToDictionary(x => x.Key, x => new SearchWaypoint(x.Value));
        }

        public Connector[] Connectors { get; private set; }
        public IDictionary<string, Waypoint> Original { get; private set; }
        public IDictionary<string, SearchWaypoint> Search { get; private set; }

        public SearchWaypoint GetNext()
        {
            return Search.Values
                .Where(x => !x.Closed && x.Weight >= 0)
                .OrderBy(x => x.Weight)
                .FirstOrDefault();
        }
    }
}