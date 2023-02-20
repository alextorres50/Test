using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Test.Assets.i18n;
using Test.Core.Services.Interfaces;

namespace Test.Core.Services
{
	public class TranslatorService : ITranslatorService
    {
        private readonly ISettingsService _settingsService;
        public TranslatorService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            LoadLanguages();
        }
        private List<CultureInfo> languages;
        public List<CultureInfo> Languages
        {

            get
            {

                LoadLanguages();
                return languages;
            }
        }

        private void LoadLanguages()
        {

            languages = new List<CultureInfo>();
            languages.Add(CultureInfo.GetCultureInfo("en-US"));
            languages.Add(CultureInfo.GetCultureInfo("es-US"));


        }

        public async Task SetCultureInfo(string culture)
        {

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            CultureInfo.DefaultThreadCurrentCulture = Thread.CurrentThread.CurrentUICulture;
            CultureInfo.DefaultThreadCurrentUICulture = Thread.CurrentThread.CurrentUICulture;
            CultureInfo.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            CultureInfo.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
            AppResources.Culture = Thread.CurrentThread.CurrentUICulture;
            _settingsService.SetString<string>("language", culture);

        }

        public async Task LoadLanguage()
        {


            Thread.CurrentThread.CurrentUICulture = new CultureInfo(GetActualLanguage());
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            CultureInfo.DefaultThreadCurrentCulture = Thread.CurrentThread.CurrentUICulture;
            CultureInfo.DefaultThreadCurrentUICulture = Thread.CurrentThread.CurrentUICulture;
            CultureInfo.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            CultureInfo.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
            AppResources.Culture = Thread.CurrentThread.CurrentUICulture;
        }

        public CultureInfo GetCultureInfo()
        {
            CultureInfo cultureInfo = null;

            cultureInfo = Thread.CurrentThread.CurrentUICulture;

            return cultureInfo;
        }
        public string GetActualLanguage()
        {

            return _settingsService.GetString<string>("language");

        }
    }
}


