using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Shared.DTOs.Table;
using RestaurantSystem.Shared.Model;
using ResturantSystem.Application.Interfaces;

namespace RestaurantSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService tableService;
        public TableController(ITableService service)
        {
            tableService = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await tableService.GetTables();
            return Ok(new ApiResponse<IEnumerable<Table>>(response));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrEditTableDto dto)
        {
            await tableService.CreateTable(dto);
            return Ok(new ApiResponse<bool>());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var response = await tableService.GetById(Id);
            return Ok(new ApiResponse<Table>(response));
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CreateOrEditTableDto dto)
        {
            await tableService.UpdateTable(dto);
            return Ok(new ApiResponse<bool>());
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            await tableService.DeleteTable(Id);
            return Ok(new ApiResponse<Table>());
        }
    }
}
