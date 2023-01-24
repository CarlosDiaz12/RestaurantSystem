using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Core.CustomEntities;
using RestaurantSystem.Infrastructure.Data;
using RestaurantSystem.Shared.DTOs.Rol;
using ResturantSystem.Application.Interfaces;


namespace ResturantSystem.Application.Services
{
    public class RolServices : IRolServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private AppDbContext _context;

        public RolServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task AddRolToUser(RolUser payload)
        {
            ApplicationUser currentUser = await _userManager.FindByIdAsync(payload.IdUser);
            await _userManager.AddToRoleAsync(currentUser, payload.Rol);
        }

        public async Task UpdateRolUser(RolUser payload)
        {
            ApplicationUser currentUser = await _userManager.FindByIdAsync(payload.IdUser);
            IList<string> roles = await _userManager.GetRolesAsync(currentUser);

            if(roles.Count > 0)
            {
                foreach (string role in roles)
                {
                    await _userManager.RemoveFromRoleAsync(currentUser, role);
                }
            }
            await _userManager.AddToRoleAsync(currentUser, payload.Rol);
        }

        public IEnumerable<IdentityRole> getRoles()
        {
            var roles = _roleManager.Roles.AsEnumerable();
            return roles;
        }

        public async Task addRol(IdentityRole rol)
        {

            await _roleManager.CreateAsync(rol);
        }

        public async Task editRol(IdentityRole rol)
        {
           var rolUpdate = await _roleManager.FindByIdAsync(rol.Id);
           rolUpdate.Name = rol.Name;

           await _roleManager.UpdateAsync(rolUpdate);

        }

        public async Task<IdentityRole> getRolById(string idRol)
        {
            return await _roleManager.FindByIdAsync(idRol);
        }

    }
}
