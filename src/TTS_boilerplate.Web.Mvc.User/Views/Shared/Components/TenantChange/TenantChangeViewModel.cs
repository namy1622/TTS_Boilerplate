using Abp.AutoMapper;
using TTS_boilerplate.Sessions.Dto;

namespace TTS_boilerplate.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
