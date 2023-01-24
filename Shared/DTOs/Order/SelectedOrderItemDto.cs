using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Shared.DTOs.Order
{
    public class SelectedOrderItemDto
    {
        public int ItemId { get; set; }
        public int SelectedQuantity { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
