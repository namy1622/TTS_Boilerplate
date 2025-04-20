using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace TTS_boilerplate.Web.Views
{
    public abstract class TTS_boilerplateRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected TTS_boilerplateRazorPage()
        {
            LocalizationSourceName = TTS_boilerplateConsts.LocalizationSourceName;
        }
    }
}
