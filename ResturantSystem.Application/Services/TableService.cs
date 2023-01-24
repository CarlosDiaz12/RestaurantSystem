using AutoMapper;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Shared.DTOs.Table;
using ResturantSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantSystem.Application.Services
{
    public class TableService : ITableService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public TableService(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            unitOfWork = UnitOfWork;
            mapper = Mapper;
        }
        public async Task CreateTable(CreateOrEditTableDto dto)
        {
            Table table = mapper.Map<Table>(dto);
            await unitOfWork.TableRepository.Insert(table);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTable(int id)
        {
            await unitOfWork.TableRepository.Delete(id);
            await unitOfWork.SaveChangesAsync();
        }

        public Task<Table> GetById(int id)
        {
           return unitOfWork.TableRepository.GetById(id);
        }

        public Task<IEnumerable<Table>> GetTables()
        {
            return unitOfWork.TableRepository.GetAll();
        }

        public async Task UpdateTable(CreateOrEditTableDto dto)
        {
            Table table = mapper.Map<Table>(dto);
            unitOfWork.TableRepository.Update(table);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
