using System;
using System.Collections.Generic;

namespace RacesDBGui.Model
{
    public partial class Rider
    {
        public Rider()
        {
            RiderRaceStats = new HashSet<RiderRaceStat>();
        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public long? Age { get; set; }

        internal virtual ICollection<RiderRaceStat> RiderRaceStats { get; set; }
    }
}
