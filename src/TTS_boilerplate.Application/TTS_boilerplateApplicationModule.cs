

using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.MailKit;
using Abp.Modules;
using Abp.Net.Mail.Smtp;
using Abp.Reflection.Extensions;
using OfficeOpenXml;
using System;
using TTS_boilerplate.Authorization;
using Microsoft.Extensions.Configuration;

namespace TTS_boilerplate
{
    [DependsOn(
        typeof(TTS_boilerplateCoreModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpMailKitModule)
        )]
    public class TTS_boilerplateApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<TTS_boilerplateAuthorizationProvider>();

            // thêm lincense cho epplus --xuất excel
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            //Configuration.ReplaceService<ISmtpEmailSenderConfiguration, CustomSmtpEmailSenderConfiguration>(DependencyLifeStyle.Transient);


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

//typeof(AbpMailKitModule)