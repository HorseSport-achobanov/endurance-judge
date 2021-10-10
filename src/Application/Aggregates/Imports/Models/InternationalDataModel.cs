using EnduranceJudge.Domain.Aggregates.Import.Models;
using System.Collections.Generic;

namespace EnduranceJudge.Application.Aggregates.Imports.Models
{
    public class InternationalDataModel
    {
        public HorseSportShowEntriesShowVenue Event { get; init; }
        public List<HorseSportShowEntriesEvent> Competitions { get; init; } = new();
        public List<HorseSportShowEntriesHorse> Horses { get; init; } = new();
        public List<HorseSportShowEntriesAthlete> Athletes { get; init; } = new();
        public List<HorseSportShowEntriesEventAthleteEntry> Participants { get; init; } = new();
    }
}
