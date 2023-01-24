using RestaurantSystem.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Core.Entities
{
    public class MenuItem: BaseEntity
    {
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public MenuItemCategory Category { get; set; }
    }
}
