using System;
using System.Collections.Generic;

namespace RacesDBGui.Model
{
    public partial class Race
    {
        public Race()
        {
            RiderRaceStats = new HashSet<RiderRaceStat>();
        }

        public double? Duration { get; set; }
        public double? VictoryMargin { get; set; }
        public double? AvgSpeed { get; set; }
        public long? Laps { get; set; }
        public long? Track { get; set; }
        public long Id { get; set; }
        public long? Tournament { get; set; }

        internal virtual Track? TrackNavigation { get; set; }
        internal virtual ICollection<RiderRaceStat> RiderRaceStats { get; set; }
    }
}
