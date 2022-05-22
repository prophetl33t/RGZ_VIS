using System;
using System.Collections.Generic;

namespace RacesDBGui.Model
{
    public partial class Tournament
    {
        public long TournamentId { get; set; }
        public string? Name { get; set; }
        public long? Year { get; set; }
    }
}
