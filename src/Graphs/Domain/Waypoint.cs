namespace Graphs.Domain
{
    public class Waypoint
    {
        public Waypoint(string name, bool blocked = false)
        {
            Name = name;
            Blocked = blocked;
        }

        public string Name { get; }
        public bool Blocked { get; set; }
    }
}