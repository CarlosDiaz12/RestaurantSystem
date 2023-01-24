using RestaurantSystem.Core.CustomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RestaurantSystem.Shared.DTOs
{
    public class ListOrderDto
    {
        public int Id { get; set; }
        public bool ShowDetails { get; set; }
        public int? TableId { get; set; }
        public string ClientName { get; set; }
        public DateTime Date { get; set; }
        public string WaiterId { get; set; }
        public string CheffId { get; set; }
        public string Status { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public RestaurantSystem.Core.Entities.Table Table { get; set; }
        public ApplicationUser Cheff { get; set; }
        public virtual List<OrderDetailsDto> OrderDetails { get; set; }
    }

    public class OrderDetailsDto
    {
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }

        public RestaurantSystem.Core.Entities.MenuItem MenuItem { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Total { get; set; }
    }
}
