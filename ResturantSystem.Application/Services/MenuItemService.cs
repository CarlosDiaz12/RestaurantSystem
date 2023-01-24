using AutoMapper;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Shared.DTOs.MenuItem;
using ResturantSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantSystem.Application.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public MenuItemService(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            unitOfWork = UnitOfWork;
            mapper = Mapper;
        }

        public async Task CreateMenuItem(CreateOrEditMenuItemDto dto)
        {
            MenuItem menuItem = mapper.Map<MenuItem>(dto);
            await unitOfWork.MenuItemRepository.Insert(menuItem);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteMenuItem(int id)
        {
            await unitOfWork.MenuItemRepository.Delete(id);
            await unitOfWork.SaveChangesAsync();
        }


        public Task<MenuItem> GetById(int id)
        {
            return unitOfWork.MenuItemRepository.GetById(id);
        }


        public Task<IEnumerable<MenuItem>> GetMenuItem()
        {
            return unitOfWork.MenuItemRepository.GetAll();
        }

        public async Task UpdateMenuItem(CreateOrEditMenuItemDto dto)
        {
            MenuItem menuItem = mapper.Map<MenuItem>(dto);
            unitOfWork.MenuItemRepository.Update(menuItem);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
