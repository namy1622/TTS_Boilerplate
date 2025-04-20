using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TTS_boilerplate.EntityFrameworkCore;
using TTS_boilerplate.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace TTS_boilerplate.Web.Tests
{
    [DependsOn(
        typeof(TTS_boilerplateWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class TTS_boilerplateWebTestModule : AbpModule
    {
        public TTS_boilerplateWebTestModule(TTS_boilerplateEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TTS_boilerplateWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(TTS_boilerplateWebMvcModule).Assembly);
        }
    }
}