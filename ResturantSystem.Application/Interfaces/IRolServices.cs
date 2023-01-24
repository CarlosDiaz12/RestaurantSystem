
using Microsoft.AspNetCore.Identity;
using RestaurantSystem.Shared.DTOs.Rol;

namespace ResturantSystem.Application.Interfaces
{
    public interface IRolServices
    {
        Task AddRolToUser(RolUser payload);
        Task UpdateRolUser(RolUser payload);

        IEnumerable<IdentityRole> getRoles();

        Task addRol(IdentityRole rol);

        Task editRol(IdentityRole rol);

        Task<IdentityRole> getRolById(string idRol);

    }
}
