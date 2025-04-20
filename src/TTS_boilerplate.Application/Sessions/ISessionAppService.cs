using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TTS_boilerplate.Sessions.Dto;
using TTS_boilerplate.TaskAppService.Dto;

namespace TTS_boilerplate.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        //Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input);

    }
}
