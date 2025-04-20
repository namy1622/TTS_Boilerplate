using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Json;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Http;

namespace TTS_boilerplate.Localization
{
    public static class TTS_boilerplateLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(TTS_boilerplateConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(TTS_boilerplateLocalizationConfigurer).GetAssembly(),
                        "TTS_boilerplate.Localization.SourceFiles"
                    )
                )
            );

            // JSON localization (thêm mới)
            //localizationConfiguration.Sources.Add(
            //    new DictionaryBasedLocalizationSource(TTS_boilerplateConsts.LocalizationSourceName,
            //        new JsonEmbeddedFileLocalizationDictionaryProvider(
            //            typeof(TTS_boilerplateLocalizationConfigurer).GetAssembly(),
            //            "TTS_boilerplate.Localization.JsonSource"
            //        )
            //    )
            //);
        }
    }
}
