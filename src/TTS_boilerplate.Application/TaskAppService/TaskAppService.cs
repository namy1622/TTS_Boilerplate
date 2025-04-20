using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTS_boilerplate.Models;
using System.Threading.Tasks;

//using System.Threading.Tasks;

using TTS_boilerplate.TaskAppService.Dto;
using Task = TTS_boilerplate.Models.Task;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;


namespace TTS_boilerplate.TaskAppService
{
    // TaskAppService: tầng trung gian giữa giao diện UI với domain(Database)
    //...SẻviceBase: kế thừa từ lớp ABP, cung cấp tiện ích: ObjectMapper, Localization,...
    public class TaskAppService : TTS_boilerplateAppServiceBase, ITaskAppService
    {
        private readonly IRepository<Task> _taskRepository; // 1 repo(kho lưu trữ) để tương tác với Entity task trong db

        public TaskAppService(IRepository<Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }


        public async Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input)
        {
            var tasks =  _taskRepository
                        .GetAll()
                        .Include(t => t.AssignedPerson) // loading dl liên quan từ entity Person
                        .AsQueryable() // Chuyển đổi thành IQueryable trước khi dùng WhereIf và OrderByDescending
                        .WhereIf(input.State.HasValue, t => t.State == input.State.Value)
                        .OrderByDescending(t => t.CreationTime)
                        .ToList();



            return new ListResultDto<TaskListDto>(
                ObjectMapper.Map<List<TaskListDto>>(tasks)
                );
        }

        public async System.Threading.Tasks.Task Create(CreateTaskInput input)
        {
            var task = ObjectMapper.Map<Task>(input); // chuyển đổi dto CreateTaskInput -> entity Task
            await _taskRepository.InsertAsync(task);  // chèn entity task vào db 
        }
    }
}
