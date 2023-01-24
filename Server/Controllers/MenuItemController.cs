using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Shared.DTOs.MenuItem;
using RestaurantSystem.Shared.DTOs.Table;
using RestaurantSystem.Shared.Model;
using ResturantSystem.Application.Interfaces;

namespace RestaurantSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService menuItemService;
        public MenuItemController(IMenuItemService service)
        {
            menuItemService = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await menuItemService.GetMenuItem();
            return Ok(new ApiResponse<IEnumerable<MenuItem>>(response));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrEditMenuItemDto dto)
        {
            await menuItemService.CreateMenuItem(dto);
            return Ok(new ApiResponse<bool>());
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var response = await menuItemService.GetById(Id);
            return Ok(new ApiResponse<MenuItem>(response));
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CreateOrEditMenuItemDto dto)
        {
            await menuItemService.UpdateMenuItem(dto);
            return Ok(new ApiResponse<bool>());
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await menuItemService.DeleteMenuItem(Id);
            return Ok(new ApiResponse<MenuItemCategory>());
        }
    }
}
