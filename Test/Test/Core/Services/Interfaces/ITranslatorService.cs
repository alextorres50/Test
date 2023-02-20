using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Test.Core.Services.Interfaces
{
    public interface ITranslatorService
    {
        CultureInfo GetCultureInfo();
        string GetActualLanguage();
        Task SetCultureInfo(string culture);
        Task LoadLanguage();
        List<CultureInfo> Languages { get; }
    }
}

