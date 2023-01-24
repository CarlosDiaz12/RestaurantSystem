using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Shared.DTOs.Category;
using RestaurantSystem.Shared.DTOs.MenuItem;
using RestaurantSystem.Shared.DTOs.Table;
using RestaurantSystem.Shared.Model;
using ResturantSystem.Application.Interfaces;

namespace RestaurantSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMenuItemCategoryService categoryService;
        public CategoryController(IMenuItemCategoryService service)
        {
            categoryService = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await categoryService.GetMenuItemCategories();
            return Ok(new ApiResponse<IEnumerable<MenuItemCategory>>(response));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrEditCategoryDto dto)
        {
            await categoryService.CreateCategory(dto);
            return Ok(new ApiResponse<bool>());
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var response = await categoryService.GetById(Id);
            return Ok(new ApiResponse<MenuItemCategory>(response));
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CreateOrEditCategoryDto dto)
        {
            await categoryService.UpdateCategory(dto);
            return Ok(new ApiResponse<bool>());
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await categoryService.DeleteCategory(Id);
            return Ok(new ApiResponse<bool>());
        }
    }
}

