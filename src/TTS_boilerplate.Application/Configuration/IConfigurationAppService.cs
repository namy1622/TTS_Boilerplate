using System.Threading.Tasks;
using TTS_boilerplate.Configuration.Dto;

namespace TTS_boilerplate.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
