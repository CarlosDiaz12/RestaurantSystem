using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Enums;
using RestaurantSystem.Shared.DTOs;
using RestaurantSystem.Shared.DTOs.Order;
using RestaurantSystem.Shared.Model;
using ResturantSystem.Application.Interfaces;

namespace RestaurantSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]GetOrdersFilter filter)
        {
            try
            {
                var result = await _orderService.GetOrders(filter);
                return Ok(new ApiResponse<IEnumerable<ListOrderDto>>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<IEnumerable<Order>>
                {
                    ErrorMessage = ex.Message,
                    Success = false
                });
            }
        }

        [HttpPut("{orderId}/change-status")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromQuery] int status)
        {
            try
            {
                await _orderService.UpdateOrderStatus(orderId, status);
                return Ok(new ApiResponse<bool>());
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<int>
                {
                    ErrorMessage = ex.Message,
                    Success = false
                });
            }
        }

        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder(PlaceOrderDto obj)
        {
            try
            {
                var result = await _orderService.PlaceOrder(obj);
                return Ok(new ApiResponse<int>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<int>
                {
                    ErrorMessage = ex.Message,
                    Success = false
                });
            }
        }


        [HttpPost("updateStatus")]
        public async Task<IActionResult> updateStatus(EditStatusOrderDto obj)
        {
            try
            {
                await _orderService.UpdateStatusOrder(obj);
                return Ok(new ApiResponse<int>());
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<int>
                {
                    ErrorMessage = ex.Message,
                    Success = false
                });
            }
        }


    }
}
