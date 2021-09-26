using System.Reflection;
using Microsoft.EntityFrameworkCore;
using EnduranceJudge.Core.Services;
using EnduranceJudge.Gateways.Persistence.Entities.Athletes;
using EnduranceJudge.Gateways.Persistence.Entities.Competitions;
using EnduranceJudge.Gateways.Persistence.Entities.Countries;
using EnduranceJudge.Gateways.Persistence.Entities.EnduranceEvents;
using EnduranceJudge.Gateways.Persistence.Entities.Horses;
using EnduranceJudge.Gateways.Persistence.Entities.Participants;
using EnduranceJudge.Gateways.Persistence.Entities.ParticipantsInCompetitions;
using EnduranceJudge.Gateways.Persistence.Entities.ParticipationsInPhases;
using EnduranceJudge.Gateways.Persistence.Entities.Personnels;
using EnduranceJudge.Gateways.Persistence.Entities.Phases;
using EnduranceJudge.Gateways.Persistence.Entities.PhasesForCategories;
using EnduranceJudge.Gateways.Persistence.Entities.ResultsInPhases;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;

namespace EnduranceJudge.Gateways.Persistence
{
    public class EnduranceJudgeDbContext : DbContext
    {
        public EnduranceJudgeDbContext()
        {
        }

        public EnduranceJudgeDbContext(DbContextOptions options, IDateTimeService dateTime)
            : base(options)
        {
        }

        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<EnduranceEventEntity> EnduranceEvents { get; set; }
        public DbSet<CompetitionEntity> Competitions { get; set; }
        public DbSet<PhaseEntity> Phases { get; set; }
        public DbSet<PhaseForCategoryEntity> PhasesForCategories { get; set; }
        public DbSet<PersonnelEntity> Personnel { get; set; }
        public DbSet<AthleteEntity> Athletes { get; set; }
        public DbSet<HorseEntity> Horses { get; set; }
        public DbSet<ParticipantEntity> Participants { get; set; }
        public DbSet<ParticipantInCompetitionEntity> ParticipantsInCompetitions { get; set; }
        public DbSet<ParticipationInPhaseEntity> ParticipationsInPhases { get; set; }
        public DbSet<ResultInPhaseEntity> ResultsInPhases { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<EnduranceEventEntity>()
                .HasMany(x => x.Competitions)
                .WithOne(y => y.EnduranceEvent)
                .HasForeignKey(y => y.EnduranceEventId);

            builder.Entity<EnduranceEventEntity>()
                .HasMany(x => x.Personnel)
                .WithOne(y => y.EnduranceEvent)
                .HasForeignKey(y => y.EnduranceEventId);

            builder.Entity<EnduranceEventEntity>()
                .HasOne(x => x.Country)
                .WithMany(y => y.EnduranceEvents)
                .HasForeignKey(y => y.CountryIsoCode);

            builder.Entity<CompetitionEntity>()
                .HasMany(c => c.Phases)
                .WithOne(p => p.Competition)
                .HasForeignKey(p => p.CompetitionId);

            builder.Entity<PhaseEntity>()
                .HasMany(p => p.PhasesForCategories)
                .WithOne(pfc => pfc.Phase)
                .HasForeignKey(pfc => pfc.PhaseId);

            builder.Entity<CompetitionEntity>()
                .HasMany(c => c.ParticipantsInCompetitions)
                .WithOne(pic => pic.Competition)
                .HasForeignKey(pic => pic.CompetitionId);

            builder.Entity<HorseEntity>()
                .HasOne(p => p.Participant)
                .WithOne(h => h.Horse)
                .HasForeignKey<ParticipantEntity>(h => h.HorseId);

            builder.Entity<ParticipantEntity>()
                .HasMany(p => p.ParticipantsInCompetitions)
                .WithOne(pic => pic.Participant)
                .HasForeignKey(pic => pic.ParticipantId);

            builder.Entity<AthleteEntity>()
                .HasOne(a => a.Participant)
                .WithOne(p => p.Athlete)
                .HasForeignKey<ParticipantEntity>(p => p.AthleteId);

            builder.Entity<AthleteEntity>()
                .HasOne(a => a.Country)
                .WithMany(c => c.Athletes)
                .HasForeignKey(a => a.CountryIsoCode);

            builder.Entity<CountryEntity>()
                .HasKey(x => x.IsoCode);

            builder.Entity<ParticipantEntity>()
                .HasMany(pip => pip.ParticipationsInPhases)
                .WithOne(pic => pic.Participant)
                .HasForeignKey(pip => pip.ParticipantId);

            builder.Entity<ParticipationInPhaseEntity>()
                .HasOne(p => p.Phase)
                .WithMany(pic => pic.ParticipationsInPhases)
                .HasForeignKey(p => p.PhaseId);

            builder.Entity<ResultInPhaseEntity>()
                .HasOne(r => r.ParticipationInPhase)
                .WithOne(p => p.ResultInPhase)
                .HasForeignKey<ParticipationInPhaseEntity>(p => p.ResultInPhaseId);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            base.OnConfiguring(optionsBuilder);
        }
    }
}
