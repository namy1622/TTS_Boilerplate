using System;

namespace TTS_boilerplate.Web.Models.Orders
{
    public class InitOrderInput
    {
        public int UserId { get; set; }
        public DateTime NowDate { get; set; }
    }
}
