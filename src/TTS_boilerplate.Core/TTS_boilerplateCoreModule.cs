using Abp.Localization;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Security;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using TTS_boilerplate.Authorization.Roles;
using TTS_boilerplate.Authorization.Users;
using TTS_boilerplate.Configuration;
using TTS_boilerplate.Localization;
using TTS_boilerplate.MultiTenancy;
using TTS_boilerplate.Timing;

namespace TTS_boilerplate
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class TTS_boilerplateCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            TTS_boilerplateLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = TTS_boilerplateConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
            
            Configuration.Localization.Languages.Add(new LanguageInfo("fa", "فارسی", "famfamfam-flags ir"));
            Configuration.Localization.Languages.Add(new LanguageInfo("vi", "VietNam", "famfamfam-flags vn"));


            Configuration.Settings.SettingEncryptionConfiguration.DefaultPassPhrase = TTS_boilerplateConsts.DefaultPassPhrase;
            SimpleStringCipher.DefaultPassPhrase = TTS_boilerplateConsts.DefaultPassPhrase;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TTS_boilerplateCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
