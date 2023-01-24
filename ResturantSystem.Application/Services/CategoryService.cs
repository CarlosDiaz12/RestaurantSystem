using AutoMapper;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Shared.DTOs.Category;
using ResturantSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantSystem.Application.Services
{
    public class MenuItemCategoryService : IMenuItemCategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public MenuItemCategoryService(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            unitOfWork = UnitOfWork;
            mapper = Mapper;
        }
        public async Task CreateCategory(CreateOrEditCategoryDto dto)
        {
            MenuItemCategory category = mapper.Map<MenuItemCategory>(dto);
            await unitOfWork.MenuItemCategoryRepository.Insert(category);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            await unitOfWork.MenuItemCategoryRepository.Delete(id);
            await unitOfWork.SaveChangesAsync();
        }

        public Task<MenuItemCategory> GetById(int id)
        {
            return unitOfWork.MenuItemCategoryRepository.GetById(id);
        }

        public Task<IEnumerable<MenuItemCategory>> GetMenuItemCategories()
        {
            return unitOfWork.MenuItemCategoryRepository.GetAll();
        }

        public async Task UpdateCategory(CreateOrEditCategoryDto dto)
        {
            MenuItemCategory category = mapper.Map<MenuItemCategory>(dto);
            unitOfWork.MenuItemCategoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync();
        }

    }
}
