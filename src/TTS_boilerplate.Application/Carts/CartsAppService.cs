using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Carts.Dto;
using TTS_boilerplate.Core;

namespace TTS_boilerplate.Carts
{
    public class CartsAppService : ICartsAppService
    {
        private readonly IRepository<CartItem> _cartRepository;

        public CartsAppService(IRepository<CartItem> cartRepository)
        {
            _cartRepository = cartRepository;
        }
        //----------------------------------
        public async Task DeleteCartItem(int IdCartItem)
        {
            var cartItem = await _cartRepository.FirstOrDefaultAsync(c => c.Id == IdCartItem);
            if (cartItem != null)
            {
                await _cartRepository.DeleteAsync(cartItem);
            }

        }
        //-----------------------------------
        //update quantity tron Cart
        public async Task UpdateQuantityFromCart(CartUpdateInput input)
        {
            var cartItem = await _cartRepository.GetAll()
                                .Where(c => c.ProductId == input.IdProduct && c.Status.ToString() == "InCart").FirstOrDefaultAsync(c => c.ProductId == input.IdProduct);

            if (cartItem != null)
            {
                cartItem.Quantity = input.Quantity;
                await _cartRepository.UpdateAsync(cartItem);
            }
        }
    }
}
