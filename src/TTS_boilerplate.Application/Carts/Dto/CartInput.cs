using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_boilerplate.Carts.Dto
{
    public class CartInput : PagedAndSortedResultRequestDto
    {
        public int idProduct { set; get; }
        public int idUser { set; get; }

        public string Status { set; get; }
  }
}
