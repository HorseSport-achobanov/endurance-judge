﻿using EnduranceJudge.Application.Contracts;
using EnduranceJudge.Application.Core.Services;
using EnduranceJudge.Core.ConventionalServices;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Core.Services;
using EnduranceJudge.Domain.State;
using System.Text.RegularExpressions;

namespace EnduranceJudge.Gateways.Persistence.Contracts
{
    public class Storage : IStorage
    {
        private const string STORAGE_FILE_NAME = "endurance-judge-data";

        private string storageFilePath;
        private readonly IState state;
        private readonly IEncryptionService encryption;
        private readonly IFileService file;
        private readonly IJsonSerializationService serialization;

        public Storage(
            IState state,
            IEncryptionService encryption,
            IFileService file,
            IJsonSerializationService serialization)
        {
            this.state = state;
            this.encryption = encryption;
            this.file = file;
            this.serialization = serialization;
        }

        public IStorageResult Initialize(string directoryPath)
        {
            this.storageFilePath = BuildStorageFilePath(directoryPath);

            if (this.file.Exists(this.storageFilePath))
            {
                this.Restore();
                return StorageResult.Existing;
            }
            else
            {
                this.Create();
                return StorageResult.New;
            }
        }

        public void Snapshot() => this.Create();

        private void Restore()
        {
            var contents = this.file.Read(this.storageFilePath);
            // Normalize countries due to change in code
            contents = this.NormalizeStorageFileContents(contents);
            var state = this.serialization.Deserialize<State>(contents);
            this.state.MapFrom(state);
        }

        private void Create()
        {
            var serialized = this.serialization.Serialize(this.state);
            this.file.Create(this.storageFilePath, serialized);
        }

        private string NormalizeStorageFileContents(string contents)
        {
            var correctCountryData = "Country\":{\"IsoCode\":\"BUL\",\"Name\":\"Bulgaria\",\"Id\":1}";
            var regex = new Regex("Country\":{\"IsoCode\":\"BUL\",\"Name\":\"Bulgaria\",\"Id\":[0-9]*}");
            return regex.Replace(contents, correctCountryData);
        }

        private static string BuildStorageFilePath(string directory) => $"{directory}\\{STORAGE_FILE_NAME}";
    }

    public interface IStorage : IStorageInitializer, IPersistence, ISingletonService
    {
    }
}
