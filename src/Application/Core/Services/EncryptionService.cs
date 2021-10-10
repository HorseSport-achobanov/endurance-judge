using EnduranceJudge.Core.ConventionalServices;
using Microsoft.AspNetCore.DataProtection;

namespace EnduranceJudge.Application.Core.Services
{
    public class EncryptionService : IEncryptionService
    {
        private const string KEY = "E546C8DF278CD5931069B522E695D4F2";
        private readonly IDataProtector dataProtector;

        public EncryptionService(IDataProtectionProvider dataProtectionProvider)
        {
            this.dataProtector = dataProtectionProvider.CreateProtector(KEY);
        }

        public string Encrypt(string content)
        {
            var encrypted = this.dataProtector.Protect(content);
            return encrypted;
        }

        public string Decrypt(string encrypted)
        {
            var decrypted = this.dataProtector.Unprotect(encrypted);
            return decrypted;
        }
    }

    public interface IEncryptionService : IService
    {
        string Encrypt(string content);
        string Decrypt(string encrypted);
    }
}
