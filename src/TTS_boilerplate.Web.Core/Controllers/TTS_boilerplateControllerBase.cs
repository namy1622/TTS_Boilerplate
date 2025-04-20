using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace TTS_boilerplate.Controllers
{
    public abstract class TTS_boilerplateControllerBase: AbpController
    {
        protected TTS_boilerplateControllerBase()
        {
            LocalizationSourceName = TTS_boilerplateConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
