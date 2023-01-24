using RestaurantSystem.Core.Entities;
using RestaurantSystem.Shared.DTOs.MenuItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantSystem.Application.Interfaces
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItem>> GetMenuItem();
        Task<MenuItem> GetById(int id);
        Task CreateMenuItem(CreateOrEditMenuItemDto dto);
        Task UpdateMenuItem(CreateOrEditMenuItemDto dto);
        Task DeleteMenuItem(int id);
    }
}
