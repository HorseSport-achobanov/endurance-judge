﻿using System;

namespace EnduranceJudge.Domain.AggregateRoots.Ranking;

public interface ICompetitionData
{
    public string EventName { get; }
    public string PopulatedPlace { get; }
    public string CountryName { get; }
    public string Organizer { get; } // ?
    public string ChiefStewardName { get; }
    public DateTime DateNow { get; }
    public string Name { get; }
    public DateTime CompetitionDate { get; }
    public double CompetitionLengthInKm { get; }
    public string PresidentGroundJuryName { get; }
}
