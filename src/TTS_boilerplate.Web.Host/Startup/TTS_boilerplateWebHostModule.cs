using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TTS_boilerplate.Configuration;

namespace TTS_boilerplate.Web.Host.Startup
{
    [DependsOn(
       typeof(TTS_boilerplateWebCoreModule))]
    public class TTS_boilerplateWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public TTS_boilerplateWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TTS_boilerplateWebHostModule).GetAssembly());
        }
    }
}
