using RestaurantSystem.Core.CustomEntities;
using RestaurantSystem.Core.Entities.Base;
using RestaurantSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Core.Entities
{
    public class Order: BaseEntity
    {
        public int? TableId { get; set; }
        public Table Table { get; set; }
        public string ClientName { get; set; }
        public DateTime Date { get; set; }
        public string WaiterId { get; set; }
        public ApplicationUser Waiter { get; set; }
        public string CheffId { get; set; }
        public ApplicationUser Cheff { get; set; }
        public OrderStatusEnum Status { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
