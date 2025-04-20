using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TTS_boilerplate.Authorization;

namespace TTS_boilerplate
{
    [DependsOn(
        typeof(TTS_boilerplateCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class TTS_boilerplateApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<TTS_boilerplateAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(TTS_boilerplateApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
