using RestaurantSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Shared.DTOs.MenuItem

{
    public class CreateOrEditMenuItemDto
    {
        public int? Id { get; set; }
        public int MenuId { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
