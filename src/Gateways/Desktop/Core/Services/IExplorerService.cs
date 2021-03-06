using EnduranceJudge.Core.ConventionalServices;

namespace EnduranceJudge.Gateways.Desktop.Core.Services;

public interface IExplorerService : ITransientService
{
    string SelectDirectory();

    string SelectFile();
}
