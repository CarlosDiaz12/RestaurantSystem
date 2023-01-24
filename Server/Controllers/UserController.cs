using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Core.CustomEntities;
using RestaurantSystem.Shared.DTOs.Table;
using RestaurantSystem.Shared.DTOs.User;
using RestaurantSystem.Shared.Model;
using ResturantSystem.Application.Interfaces;
using ResturantSystem.Application.Services;

namespace RestaurantSystem.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServices;
        public UserController(IUserService userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _userServices.GetUsers();
                return Ok(new ApiResponse<IEnumerable<dynamic>>(response));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrEditUserDto payload)
        {
            try
            {
                await _userServices.Create(payload);
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

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(string Id)
        {
            var response = await _userServices.GetById(Id);
            return Ok(new ApiResponse<CreateOrEditUserDto>(response));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CreateOrEditUserDto dto)
        {
            await _userServices.Update(dto);
            return Ok(new ApiResponse<bool>());
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            await _userServices.Delete(Id);
            return Ok(new ApiResponse<ApplicationUser>());
        }

    }
}
