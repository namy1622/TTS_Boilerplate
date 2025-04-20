using Abp.AspNetCore.Mvc.ViewComponents;

namespace TTS_boilerplate.Web.Views
{
    public abstract class TTS_boilerplateViewComponent : AbpViewComponent
    {
        protected TTS_boilerplateViewComponent()
        {
            LocalizationSourceName = TTS_boilerplateConsts.LocalizationSourceName;
        }
    }
}
