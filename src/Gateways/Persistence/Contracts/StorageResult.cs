using EnduranceJudge.Application.Contracts;

namespace EnduranceJudge.Gateways.Persistence.Contracts
{
    public class StorageResult : IStorageResult
    {
        public static StorageResult New => new(false);
        public static StorageResult Existing => new(true);

        private StorageResult(bool IsExistingFile)
        {
            this.IsExistingFile = IsExistingFile;
        }

        public bool IsExistingFile { get; }
    }
}
