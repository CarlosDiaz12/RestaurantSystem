using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Shared.DTOs.Menu;
using RestaurantSystem.Shared.Model;
using ResturantSystem.Application.Interfaces;

namespace RestaurantSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _menuService.GetAllMenus();
            return Ok(new ApiResponse<IEnumerable<Menu>>(response));
        }
        

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrEditMenuDto dto)
        {
            await _menuService.CreateMenu(dto);
            return Ok(new ApiResponse<bool>(true));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CreateOrEditMenuDto dto)
        {
            await _menuService.UpdateMenu(dto);
            return Ok(new ApiResponse<bool>());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var response = await _menuService.GetById(Id);
            return Ok(new ApiResponse<Menu>(response));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _menuService.DeleteMenu(Id);
            return Ok(new ApiResponse<Menu>());
        }

    }
}
