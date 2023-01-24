using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Shared.DTOs.Rol;
using RestaurantSystem.Shared.Model;
using ResturantSystem.Application.Interfaces;

namespace RestaurantSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolServices _RolServices;

        public RolController(IRolServices rolServices)
        {
            _RolServices = rolServices;
        }


        [HttpGet]
        [Route("GetRoles")]
        public  IActionResult Get()
        {
            try
            {
                var response = _RolServices.getRoles();
                return Ok(new ApiResponse<IEnumerable<IdentityRole>>(response));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpPost]
        [Route("AddRol")]
        public async Task<IActionResult> AddRol([FromBody] IdentityRole payload)
        {
            try
            {
                await _RolServices.addRol(payload);
                return Ok(new ApiResponse<bool>());
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse<bool>
                {
                    ErrorMessage = e.Message,
                    Success = false
                });
            }
        }

        [HttpPost]
        [Route("AddRolToUser")]
        public async Task<IActionResult> AddRolToUser([FromBody] RolUser payload)
        {
            await _RolServices.AddRolToUser(payload);
            return Ok(new ApiResponse<bool>());
        }

        [HttpPut]
        [Route("UpdateRolUser")]
        public async Task<IActionResult> UpdateRolUser([FromBody] RolUser payload)
        {
            await _RolServices.UpdateRolUser(payload);
            return Ok(new ApiResponse<bool>());
        }


        [HttpPut]
        [Route("UpdateRol")]
        public async Task<IActionResult> UpdateRol([FromBody] IdentityRole rol)
        {
            await _RolServices.editRol(rol);
            return Ok(new ApiResponse<bool>());
        }

        [HttpGet]
        [Route("GetRolById/{idRol}")]
        public async Task<IActionResult> Get(string idRol)
        {
            var response = await _RolServices.getRolById(idRol);
            return Ok(new ApiResponse<IdentityRole>(response));
        }

    }
}
