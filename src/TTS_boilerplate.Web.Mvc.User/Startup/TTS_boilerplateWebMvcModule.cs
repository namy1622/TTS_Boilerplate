using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TTS_boilerplate.Configuration;

namespace TTS_boilerplate.Web.Startup
{
    [DependsOn(typeof(TTS_boilerplateWebCoreModule))]
    public class TTS_boilerplateWebMvcModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public TTS_boilerplateWebMvcModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<TTS_boilerplateNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TTS_boilerplateWebMvcModule).GetAssembly());
        }
    }
}
