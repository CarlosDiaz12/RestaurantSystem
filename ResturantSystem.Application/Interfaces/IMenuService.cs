using RestaurantSystem.Core.Entities;
using RestaurantSystem.Shared.DTOs.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantSystem.Application.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> GetAllMenus();
        Task<Menu> GetById(int id);
        Task CreateMenu(CreateOrEditMenuDto dto);
        Task UpdateMenu(CreateOrEditMenuDto dto);
        Task DeleteMenu(int id);
    }
}
