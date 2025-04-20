using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Models;
using TTS_boilerplate.TaskAppService.Dto;
using Task = TTS_boilerplate.Models.Task;

namespace TTS_boilerplate.TaskAppService
{
    public interface ITaskAppService :IApplicationService
    {
        Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input);

        System.Threading.Tasks.Task Create(CreateTaskInput input);
    }

}
