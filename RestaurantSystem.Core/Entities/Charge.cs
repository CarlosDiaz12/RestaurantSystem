using RestaurantSystem.Core.Entities.Base;
using RestaurantSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Core.Entities
{
    public class Charge: BaseEntity
    {
        public ChargeTypeEnum Type { get; set; }
        public double Amount { get; set; }
    }
}
