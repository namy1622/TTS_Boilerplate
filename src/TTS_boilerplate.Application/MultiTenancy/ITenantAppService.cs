using Abp.Application.Services;
using TTS_boilerplate.MultiTenancy.Dto;

namespace TTS_boilerplate.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

