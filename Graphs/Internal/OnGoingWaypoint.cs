using Graphs.Domain;

namespace Graphs.Internal
{
    internal class OnGoingWaypoint : Waypoint
    {
        public OnGoingWaypoint(Waypoint waypoint) : base(waypoint.Name)
        {
            Refresh();
        }

        public double Weight { get; private set; }
        public OnGoingWaypoint Best { get; private set; }
        public bool Closed { get; private set; }

        public void SetBestNeighbor(OnGoingWaypoint point, double weight)
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