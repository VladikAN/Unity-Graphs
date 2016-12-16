namespace Graphs.Core.Domain
{
    public class SearchWaypoint : Waypoint
    {
        public SearchWaypoint(Waypoint waypoint) : base(waypoint.Name)
        {
            Weight = -1;
            Best = null;
            Closed = false;
        }

        public double Weight { get; private set; }
        public SearchWaypoint Best { get; private set; }
        public bool Closed { get; private set; }

        public void SetBest(SearchWaypoint point, double weight)
        {
            Weight = weight;
            Best = point;
        }

        public void Close()
        {
            Closed = true;
        }
    }
}