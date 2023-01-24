using RestaurantSystem.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Core.Entities
{
    public class Menu: BaseEntity
    {
        public string Description { get; set; }
        public ICollection<MenuItem> Items { get; set; }
    }
}
