using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Shared.DTOs.Table
{
    public class CreateOrEditTableDto
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public int SeatsQuantity { get; set; }
    }
}
