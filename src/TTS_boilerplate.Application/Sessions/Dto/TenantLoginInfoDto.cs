using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using TTS_boilerplate.MultiTenancy;

namespace TTS_boilerplate.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
