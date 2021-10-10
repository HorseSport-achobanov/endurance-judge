using EnduranceJudge.Application.Contracts;
using EnduranceJudge.Application.Aggregates.Configurations.Contracts;
using EnduranceJudge.Application.Core.Exceptions;
using EnduranceJudge.Application.Core.Services;
using EnduranceJudge.Application.Aggregates.Imports.Services;
using EnduranceJudge.Core.ConventionalServices;
using EnduranceJudge.Domain.Aggregates.Import;
using EnduranceJudge.Domain.State.Countries;
using static EnduranceJudge.Localization.Strings.Application;

namespace EnduranceJudge.Application.Services
{
    public class ImportService : IImportService
    {
        private readonly IPersistence persistence;
        private readonly IFileService file;
        private readonly IInternationalReader internationalReader;
        private readonly INationalReader nationalReader;
        private readonly IQueries<Country> countries;

        public ImportService(
            IPersistence persistence,
            IFileService file,
            IInternationalReader internationalReader,
            INationalReader nationalReader,
            IQueries<Country> countries)
        {
            this.persistence = persistence;
            this.file = file;
            this.internationalReader = internationalReader;
            this.nationalReader = nationalReader;
            this.countries = countries;
        }

        public void Import(string filePath)
        {
            var fileExtension = this.file.GetExtension(filePath);
            if (fileExtension != ApplicationConstants.FileExtensions.Xml && fileExtension != ApplicationConstants.FileExtensions.SupportedExcel)
            {
                var message = string.Format(
                    UNSUPPORTED_IMPORT_FILE_TEMPLATE,
                    ApplicationConstants.FileExtensions.Xml,
                    ApplicationConstants.FileExtensions.SupportedExcel);

                throw new AppException(message);
            }

            ImportManager manager;
            if (fileExtension == ApplicationConstants.FileExtensions.Xml)
            {
                var data = this.internationalReader.Read(filePath);

                var country = this.countries.GetOne(x => x.IsoCode == data.Event?.CountryNOC);
                manager = new ImportManager(data.Event?.Name, country);
                manager.AddCompetitions(data.Competitions);
                manager.AddAthletes(data.Athletes);
                manager.AddHorses(data.Horses);
                manager.AddParticipants(data.Participants);
            }
            else
            {
                manager = new ImportManager();
                var horses = this.nationalReader.Read(filePath);
                foreach (var horse in horses)
                {
                    manager.AddHorse(horse.Name, horse.Breed, horse.FeiId, horse.Club);
                }
            }

            this.persistence.Snapshot();
        }
    }

    public interface IImportService : IService
    {
        void Import(string filePath);
    }
}
