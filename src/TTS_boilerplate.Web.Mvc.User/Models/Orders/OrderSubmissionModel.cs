using System.Collections.Generic;

namespace TTS_boilerplate.Web.Models.Orders
{
    public class OrderSubmissionModel
    {
        public List<OrderItemSubmissionModel> Items { get; set; }
        public CustomerInfoModel CustomerInfo { get; set; }
    }

    public class OrderItemSubmissionModel
    {
        public int ProductId { get; set; }
        public string NameProduct { get; set; }
        public string ImgPath { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }
       
        public string CartId { get; set; }
    }
    public class CustomerInfoModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

    }
}