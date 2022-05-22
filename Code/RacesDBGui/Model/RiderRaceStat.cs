using System;
using System.Collections.Generic;

namespace RacesDBGui.Model
{
    public partial class RiderRaceStat
    {
        public long? StartPlace { get; set; }
        public long Id { get; set; }
        public long? Rider { get; set; }
        public long? Race { get; set; }
        public long? FinishPlace { get; set; }
        public string? TeamName { get; set; }
        public string? Chassis { get; set; }
        public string? Sponsor { get; set; }
        public long? Points { get; set; }
        public long? LapsLed { get; set; }
        public long? Laps { get; set; }

        internal virtual Race? RaceNavigation { get; set; }
        internal virtual Rider? RiderNavigation { get; set; }
    }
}
