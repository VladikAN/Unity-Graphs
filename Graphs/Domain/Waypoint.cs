using System;

namespace Graphs.Domain
{
    public class Waypoint
    {
        public Waypoint(string name, bool blocked = false)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Please specify waypoint name");
            }
            
            Name = name;
            Blocked = blocked;
        }

        public string Name { get; }
        public bool Blocked { get; set; }
    }
}