namespace Graphs.Domain
{
    public class TempWaypoint : Waypoint
    {
        public TempWaypoint(Waypoint waypoint) : base(waypoint.Name)
        {
            Weight = -1;
            Best = null;
            Closed = false;
        }

        public double Weight { get; private set; }
        public TempWaypoint Best { get; private set; }
        public bool Closed { get; private set; }

        public void SetBestNeighbor(TempWaypoint point, double weight)
        {
            Weight = weight;
            Best = point;
        }

        public void Refresh()
        {
            Weight = -1;
            Best = null;
            Closed = false;
        }

        public void CloseForUpdates()
        {
            Closed = true;
        }
    }
}