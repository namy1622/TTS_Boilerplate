using System.Collections.Generic;
using TTS_boilerplate.Carts.Dto;

namespace TTS_boilerplate.Web.Models.Carts
{
    public class CartListViewModel
    {
        public IReadOnlyList<CartItemDto> CartList{ get; set; }
    }
}
