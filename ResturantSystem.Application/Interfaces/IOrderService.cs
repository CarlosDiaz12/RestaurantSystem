using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Enums;
using RestaurantSystem.Shared.DTOs;
using RestaurantSystem.Shared.DTOs.Order;
using RestaurantSystem.Shared.DTOs.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantSystem.Application.Interfaces
{
    public interface IOrderService
    {
        Task<int> PlaceOrder(PlaceOrderDto dto);
        Task<IEnumerable<ListOrderDto>> GetOrders(GetOrdersFilter filter);

        Task UpdateStatusOrder(EditStatusOrderDto dto);

        Task UpdateOrderStatus(int orderId, int statusId);
        Task<IEnumerable<ListOrderDto>> GetOrders();
    }
}
