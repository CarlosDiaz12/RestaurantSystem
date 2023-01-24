using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Shared.DTOs.Order
{
    public class OrderItemDto
    {
        public OrderItemDto()
        {

        }

        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
