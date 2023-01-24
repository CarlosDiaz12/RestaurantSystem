using RestaurantSystem.Core.Entities;
using RestaurantSystem.Shared.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantSystem.Application.Interfaces
{
    public interface IMenuItemCategoryService
    {
        Task<IEnumerable<MenuItemCategory>> GetMenuItemCategories();
        Task<MenuItemCategory> GetById(int id);
        Task CreateCategory(CreateOrEditCategoryDto dto);
        Task UpdateCategory(CreateOrEditCategoryDto dto);
        Task DeleteCategory(int id);
    }
}
