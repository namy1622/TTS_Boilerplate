using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.Carts.Dto
{
  public class CartUpdateInput
  {
    public int Quantity { set; get; }
    public int IdProduct { set; get; }
  }
}
