using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Models;
using Task = TTS_boilerplate.Models.Task;

namespace TTS_boilerplate.TaskAppService.Dto
{

    public class GetAllTasksInput
    {
        public TaskState? State { get; set; }
    }

    [AutoMapFrom(typeof(Task))]
    public class TaskListDto : EntityDto, IHasCreationTime
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { set; get; }

        public TaskState State { get; set; }

        //---

        public Guid? AsignedPersonId { get; set; }

        public string AssignedPersonName { get; set; }

        //--
    }

 
}
