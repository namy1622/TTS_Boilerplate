using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.SendMail.Dto;

namespace TTS_boilerplate.SendMail
{
    public class SendMailAppService : TTS_boilerplateAppServiceBase, ISendMailAppService
    {
        public readonly TaskManagerAppService _taskManagerAppService;
        public SendMailAppService(TaskManagerAppService taskManagerAppService) {
            _taskManagerAppService = taskManagerAppService;
        }

        public System.Threading.Tasks.Task SendMail()
        {
            var taskmail = new TaskMail
            {
                Id = Guid.NewGuid(),
                Name = "Fix login bug",
                Description = "Fix the issue with login on mobile devices",
                DueDate = DateTime.Now
            };

            var personmail = new PersonMail
            {
                Id = Guid.NewGuid(),
                FullName = "Nguyễn Văn A",
                EmailAddress = "doantuannam2206.sp@gmail.com"
            };
            _taskManagerAppService.Assgin(taskmail, personmail);
            return Task.CompletedTask;
        }
    }
}
