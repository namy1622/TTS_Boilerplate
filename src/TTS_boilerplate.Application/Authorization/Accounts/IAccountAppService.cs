using System.Threading.Tasks;
using Abp.Application.Services;
using TTS_boilerplate.Authorization.Accounts.Dto;

namespace TTS_boilerplate.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
