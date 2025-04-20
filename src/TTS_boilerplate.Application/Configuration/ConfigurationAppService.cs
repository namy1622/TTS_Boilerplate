using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using TTS_boilerplate.Configuration.Dto;

namespace TTS_boilerplate.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : TTS_boilerplateAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
