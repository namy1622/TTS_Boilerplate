using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Orders.Dto;

namespace TTS_boilerplate.Orders
{
    public interface IOrdersAppService : IApplicationService
    {
        Task InitOrder(OrderInput input);
        
        Task AddOrderItem_FromHomeToOrder(OrderItemInput input);

        Task<OrderItemDto> GetItemOrder(int idProduct);
  }
}
