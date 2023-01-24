using AutoMapper;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Shared.DTOs.Menu;
using ResturantSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantSystem.Application.Services
{
    public class MenuService: IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MenuService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateMenu(CreateOrEditMenuDto dto)
        {
            Menu Menu = _mapper.Map<Menu>(dto);
            await _unitOfWork.MenuRepository.Insert(Menu);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteMenu(int id)
        {
            await _unitOfWork.MenuRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task<IEnumerable<Menu>> GetAllMenus()
        {
            return _unitOfWork.MenuRepository.GetAll();
        }

        public Task<Menu> GetById(int id)
        {
            return _unitOfWork.MenuRepository.GetById(id);
        }

        public async Task UpdateMenu(CreateOrEditMenuDto dto)
        {
            Menu Menu = _mapper.Map<Menu>(dto);
            _unitOfWork.MenuRepository.Update(Menu);
            await _unitOfWork.SaveChangesAsync();
        }

       
    }
}
