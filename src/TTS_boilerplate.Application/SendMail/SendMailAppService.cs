using Abp.Application.Services;
using Abp.Net.Mail;

//using Abp.Net.Mail;
using Abp.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly IConfiguration _configuration;
        private readonly ILogger<SendMailAppService> _logger;
        private readonly IEmailSender _emailSender;

        public SendMailAppService(IConfiguration configuration, ILogger<SendMailAppService> logger, TaskManagerAppService taskManagerAppService, IEmailSender emailSender = null)
        {
            _taskManagerAppService = taskManagerAppService;
            _configuration = configuration;
            _logger = logger;
            _emailSender = emailSender;
        }

        public async System.Threading.Tasks.Task SendMail()
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
            //ValidateEmailConfiguration();
            await _taskManagerAppService.Assgin(taskmail, personmail);
            //await _taskManagerAppService.SendTestEmail();
            
        }

        public async Task SendTestEmailAsync()
        {
            try
            {
                var fromAddress = _configuration["AbpMail:DefaultFromAddress"];
                var fromName = _configuration["AbpMail:DefaultFromDisplayName"];

                await _emailSender.SendAsync(
                    "doantuannam2206.sp@gmail.com",  // Email người nhận
                    "Test Email",                    // Tiêu đề
                    "Đây là nội dung email test..."  // Nội dung
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gửi email");
                throw new UserFriendlyException("Lỗi gửi email: " + ex.Message);
            }
        }

    }
}
