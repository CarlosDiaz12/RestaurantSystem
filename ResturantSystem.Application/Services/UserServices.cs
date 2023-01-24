using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestaurantSystem.Core.CustomEntities;
using RestaurantSystem.Infrastructure.Data;
using RestaurantSystem.Shared.DTOs.Rol;
using RestaurantSystem.Shared.DTOs.Table;
using RestaurantSystem.Shared.DTOs.User;
using ResturantSystem.Application.Interfaces;

namespace ResturantSystem.Application.Services
{
    public class UserServices : IUserService
    {
        private AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRolServices _rolServices;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly IConfiguration _configuration;

        public UserServices(
            AppDbContext context,
            IUserStore<ApplicationUser> userStore,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IRolServices rolServices)
        {
            _context = context;
            _userManager = userManager;
            _rolServices = rolServices;
            _userStore = userStore;
            _configuration = configuration;
            _emailStore = GetEmailStore();
        }

        public async Task Create(CreateOrEditUserDto dto)
        {
            dto.PassWord = _configuration.GetValue<string>("DefaultPasswordUser");
            ApplicationUser newUser = new ApplicationUser
            {
                Names = dto.Names,
                LastsNames = dto.LastsNames,
                Email = dto.Email,
                IsFirstLogin = true
            };

            await _userStore.SetUserNameAsync(newUser, newUser.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(newUser, newUser.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(newUser, dto.PassWord);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
      
              var user = await _userManager.FindByNameAsync(dto.Email);
              await _rolServices.AddRolToUser(new RolUser() { IdUser = user.Id, Rol = dto.Rol });


        }

        public async Task Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
                throw new Exception("Usuario no encontrado");

            await _userManager.DeleteAsync(user);
        }

        public async Task<CreateOrEditUserDto> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var rolUser = await _userManager.GetRolesAsync(user);
            CreateOrEditUserDto response = new CreateOrEditUserDto()
            {
                Id = user.Id,
                Names = user.Names,
                LastsNames = user.LastsNames,
                Email = user.Email,
                Rol = rolUser.Count <= 0 ? null : rolUser.First()
            };

            return response;
        }

        public async Task<IEnumerable<UserTableDataDTO>> GetUsers()
        {
            return await _context.Users.Select(u => new UserTableDataDTO
            {
                Id = u.Id,
                Email=u.Email,
                UserName=u.UserName,
                Names = u.Names,
                LastsNames = u.LastsNames
            }).ToListAsync();
        }

        public async Task Update(CreateOrEditUserDto dto)
        {
            var userUpdate = await _userManager.FindByIdAsync(dto.Id);
            if(userUpdate != null)
            {
                userUpdate.Names = dto.Names;
                userUpdate.LastsNames = dto.LastsNames;
                userUpdate.Email = dto.Email;
                userUpdate.UserName = dto.Email;


                await _userStore.UpdateAsync(userUpdate, CancellationToken.None);
                await _emailStore.UpdateAsync(userUpdate, CancellationToken.None);
                await _userManager.UpdateAsync(userUpdate);

                //actualizamos rol
                RolUser rolUser = new RolUser();
                rolUser.IdUser = dto.Id;
                rolUser.Rol = dto.Rol;
                await _rolServices.UpdateRolUser(rolUser);
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
