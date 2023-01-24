using RestaurantSystem.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Core.Entities
{
    public class Table: BaseEntity
    {
        public string Description { get; set; }
        public int SeatsQuantity { get; set; }
    }
}
