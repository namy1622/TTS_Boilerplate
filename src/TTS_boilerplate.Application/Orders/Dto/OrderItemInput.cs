using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.Orders.Dto
{
  public class OrderItemInput
  {
        public int idProduct { set; get; }
        public int idUser { set; get; }

        public string Status { set; get; }
    }
}
