using System;
using System.Collections.Generic;

namespace RacesDBGui.Model
{
    public partial class Track
    {
        public Track()
        {
            Races = new HashSet<Race>();
        }

        public long Id { get; set; }
        public double? Length { get; set; }
        public string? Name { get; set; }
        public string? RoadDescription { get; set; }

        internal virtual ICollection<Race> Races { get; set; }
    }
}
