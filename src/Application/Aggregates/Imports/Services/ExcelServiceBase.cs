using EnduranceJudge.Core.ConventionalServices;
using OfficeOpenXml;
using System;
using System.IO;

namespace EnduranceJudge.Application.Core.Services
{
    public abstract class ExcelServiceBase : IService, IDisposable
    {
        protected ExcelServiceBase()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        protected ExcelPackage Excel { get; private set; }

        protected void Initialize(FileInfo file)
        {
            this.Excel = new ExcelPackage(file);
        }

        public void Dispose()
        {
            this.Excel?.Dispose();
        }
    }
}
