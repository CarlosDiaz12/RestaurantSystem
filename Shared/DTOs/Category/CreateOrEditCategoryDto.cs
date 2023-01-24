using RestaurantSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Shared.DTOs.Category
{
    public class CreateOrEditCategoryDto
    {
        public int? Id { get; set; }
        public string Description { get; set; }
    }
}
