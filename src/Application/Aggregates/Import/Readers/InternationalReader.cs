﻿using EnduranceJudge.Application.Core.Services;
using EnduranceJudge.Core.ConventionalServices;
using EnduranceJudge.Domain.Aggregates.Import.Models;
using EnduranceJudge.Domain.Aggregates.Import.Models.International;
using System.Linq;

namespace EnduranceJudge.Application.Aggregates.Import.Readers
{
    public class InternationalReader : IInternationalReader
    {
        private readonly IXmlSerializationService xmlSerialization;

        public InternationalReader(IXmlSerializationService xmlSerialization)
        {
            this.xmlSerialization = xmlSerialization;
        }

        public InternationalData Read(string filePath)
        {
            var importData = this.xmlSerialization.Deserialize<HorseSport>(filePath);
            var showEntries = importData?.Items?.FirstOrDefault() as HorseSportShowEntries;
            if (showEntries == null)
            {
                return null;
            }

            var showData = showEntries.Show.FirstOrDefault();
            var venueData = showData?.Venue.FirstOrDefault();
            var athletesData = showEntries.Athlete.ToList();
            var horseData = showEntries.Horse.ToList();
            var eventData = showEntries.Event.ToList();

            var data = new InternationalData
            {
                Event = venueData,
                Competitions = eventData,
                Athletes = athletesData,
                Horses = horseData,
            };

            foreach (var competitionData in eventData)
            {
                var participantsData = competitionData.AthleteEntry.ToList();
                data.Participants.AddRange(participantsData);
            }

            return data;
        }
    }

    public interface IInternationalReader : IService
    {
        InternationalData Read(string filePath);
    }
}