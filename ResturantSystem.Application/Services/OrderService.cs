using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RestaurantSystem.Core.CustomEntities;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Enums;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Infrastructure.Util;
using RestaurantSystem.Shared.DTOs;
using RestaurantSystem.Shared.DTOs.Order;
using ResturantSystem.Application.Exceptions;
using ResturantSystem.Application.Interfaces;

namespace ResturantSystem.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ListOrderDto>> GetOrders(GetOrdersFilter filter)
        {
            var query = PredicateBuilder.True<Order>();

            if(filter.Status.HasValue)
                query = query.And(x => x.Status == filter.Status.Value);
            
            if(filter.Fecha.HasValue)
                query = query.And(x => x.Date.Date == filter.Fecha.Value.Date);

            string includeProps = $"{nameof(Order.Cheff)},{nameof(Order.OrderDetails)},{nameof(Order.Table)},{nameof(Order.OrderDetails)}.{nameof(OrderDetail.MenuItem)}";
            var orders = await _unitOfWork.OrderRepository.GetAll(query, includeProperties: includeProps);
            var result = _mapper.Map<IEnumerable<ListOrderDto>>(orders);
            return result;
        }

        public async Task<int> PlaceOrder(PlaceOrderDto dto)
        {
            if (dto.Items is null || dto.Items.Count == 0)
                throw new BusinessException("Debe seleccionar al menos 1 item");

            // mapear los items a su respectivo tipo en base de datos: calcular cantidad y precio
            var items = await CreateOrderDetails(dto.Items);
            var order = _mapper.Map<Order>(dto);
            order.Date = DateTime.Now;
            order.Status = OrderStatusEnum.PENDING;

            double total = items.Sum(x => x.Total);

            order.OrderDetails = items;
            double taxes = (total * Constants.ItbisPercentage);
            var cargos = new List<double> { taxes };

            // agregar cargos adicionales
            if (dto.TableId != 0)
            {
                double tipCharge = (total * Constants.TipPercentage);
                cargos.Add(tipCharge);
            } else
            {
                order.TableId = null;
            }

            order.SubTotal = total;
            order.Total = total + (cargos.Sum());

            await _unitOfWork.OrderRepository.Insert(order);
            await _unitOfWork.SaveChangesAsync();

            return order.Id;
        }

        public async Task UpdateStatusOrder(EditStatusOrderDto dto)
        {
            var orderUpdate = await _unitOfWork.OrderRepository.GetById(dto.OrderId);
            orderUpdate.CheffId = dto.userId;
            orderUpdate.Status = (OrderStatusEnum)dto.statusOrderId;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateOrderStatus(int orderId, int statusId)
        {
            var order = await _unitOfWork.OrderRepository.GetById(orderId);
            if (order == null)
                throw new BusinessException("La orden no existe.");

            order.Status = (OrderStatusEnum)statusId;
            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<List<OrderDetail>> CreateOrderDetails(List<OrderItemDto> orderItemDtos)
        {
            var result = new List<OrderDetail>();

            foreach (var item in orderItemDtos)
            {
                // busco el item en el menu
                var menuItem = await _unitOfWork.MenuItemRepository.GetById(item.ItemId);
                if (menuItem == null)
                    throw new BusinessException($"MenuItem: {item.ItemId} no existe.");

                if (item.Quantity <= 0)
                    throw new BusinessException($"La cantidad del item: {menuItem.Description} debe ser mayor a 0.");

                double total = item.Quantity * menuItem.Price;

                var orderDetail = new OrderDetail
                {
                    MenuItemId = item.ItemId,
                    Total = total,
                    UnitPrice = menuItem.Price,
                    Quantity = item.Quantity,
                    DateCreated = DateTime.Now,
                };

                result.Add(orderDetail);
            }
            return result;
        }

        public async Task<IEnumerable<ListOrderDto>> GetOrders()
        {
            string includeProps = $"{nameof(Order.Cheff)},{nameof(Order.OrderDetails)},{nameof(Order.Table)},{nameof(Order.OrderDetails)}.{nameof(OrderDetail.MenuItem)}";
            var orders = await _unitOfWork.OrderRepository.GetAll(includeProperties: includeProps);
            var result = _mapper.Map<IEnumerable<ListOrderDto>>(orders);
            return result;
        }
    }
}
