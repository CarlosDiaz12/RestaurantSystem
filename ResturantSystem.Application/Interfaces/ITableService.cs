using RestaurantSystem.Core.Entities;
using RestaurantSystem.Shared.DTOs.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantSystem.Application.Interfaces
{
    public interface ITableService
    {
        Task<IEnumerable<Table>> GetTables();
        Task<Table> GetById(int id);
        Task CreateTable(CreateOrEditTableDto dto);
        Task UpdateTable(CreateOrEditTableDto dto);
        Task DeleteTable(int id);
    }
}
