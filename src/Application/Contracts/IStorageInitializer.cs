namespace EnduranceJudge.Application.Contracts
{
    public interface IStorageInitializer
    {
        IStorageResult Initialize(string directoryPath);
    }
}
