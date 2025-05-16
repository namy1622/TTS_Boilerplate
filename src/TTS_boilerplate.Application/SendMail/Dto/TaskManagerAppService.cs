using Abp.Application.Services;
using Abp.Domain.Services;
using Abp.Net.Mail;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Models;

namespace TTS_boilerplate.SendMail.Dto
{
    public class TaskManagerAppService : IDomainService //, ITaskManagerAppService
    {
        private readonly IEmailSender _emailSender;

        public TaskManagerAppService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async System.Threading.Tasks.Task Assgin(TaskMail task, PersonMail personmail)
        {
            try { 
            task.AssignedTo = personmail;
                Console.Write(_emailSender.ToString());
                //await _emailSender.SendAsync(
                //    to: personmail.EmailAddress,
                //    subject: "Task Assigned",
                //    body: $"You have been assigned a new task: {task.Name}.\n\n" +
                //          $"Description: {task.Description}\n\n" +
                //          $"Due Date: {task.DueDate.ToShortDateString()}.\n\n" +
                //          $"Please check your task list for more details.",
                //    isBodyHtml: true
                //    );
                await _emailSender.SendAsync(
                    "doantuannam2206.sp@gmail.com", // To
                    "Test email from ABP",          // Subject
                    "This is a test email.",        // Body
                    false                           // isBodyHtml
                );
            }
            catch (Exception ex)
        {
            throw new UserFriendlyException("Lỗi gửi email: " + ex.ToString());
        }

}

        public async System.Threading.Tasks.Task SendTestEmail()
        {
            await _emailSender.SendAsync(
                "doantuannam2206.sp@gmail.com", // To
                "Test email from ABP",          // Subject
                "This is a test email.",        // Body
                false                           // isBodyHtml
            );
        }

        public void Send()
        {
            var task = new TaskMail
            {
                Id = Guid.NewGuid(),
                Name = "Fix login bug",
                Description = "Fix the issue with login on mobile devices",
                DueDate = DateTime.Now
            };

            var person = new PersonMail
            {
                Id = Guid.NewGuid(),
                FullName = "Nguyễn Văn A",
                EmailAddress = "doantuannam2206.sp@gmail.com"
            };

            Assgin(task, person);

        }
    }


    public class TaskMail
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PersonMail AssignedTo { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
    public class PersonMail
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
    }

}
