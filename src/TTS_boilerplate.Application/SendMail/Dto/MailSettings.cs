using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.SendMail.Dto
{
    public class MailSettings
    {
        public string Mail { get; set; }         // Địa chỉ email gửi
        public string DisplayName { get; set; }  // Tên hiển thị
        public string Password { get; set; }     // Mật khẩu hoặc App Password
        public string Host { get; set; }         // smtp.gmail.com
        public int Port { get; set; }            // 587
    }
}
