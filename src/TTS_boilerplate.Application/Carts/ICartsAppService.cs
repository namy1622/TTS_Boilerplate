using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Carts.Dto;

namespace TTS_boilerplate.Carts
{
    public interface ICartsAppService : IApplicationService
    {
        Task UpdateQuantityFromCart(CartUpdateInput input);
        Task DeleteCartItem(int idProduct);
  }
}
