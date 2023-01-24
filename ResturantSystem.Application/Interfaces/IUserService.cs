using Microsoft.AspNetCore.Identity;
using RestaurantSystem.Core.CustomEntities;
using RestaurantSystem.Shared.DTOs.Table;
using RestaurantSystem.Shared.DTOs.User;


namespace ResturantSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserTableDataDTO>> GetUsers();
        Task<CreateOrEditUserDto> GetById(string id);
        Task Create(CreateOrEditUserDto dto);
        Task Update(CreateOrEditUserDto dto);
        Task Delete(string id);
    }
}
