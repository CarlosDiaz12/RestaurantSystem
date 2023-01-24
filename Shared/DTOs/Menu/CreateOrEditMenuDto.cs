using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Shared.DTOs.Menu
{
   public class CreateOrEditMenuDto
    {
        public int? Id { get; set; }
        public string Description { get; set; }
    }
}
