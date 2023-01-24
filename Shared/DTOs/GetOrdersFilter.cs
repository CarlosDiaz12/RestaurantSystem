using RestaurantSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Shared.DTOs
{
    public class GetOrdersFilter
    {
        public DateTime? Fecha { get; set; }
        public OrderStatusEnum? Status { get; set; }
    }
}
