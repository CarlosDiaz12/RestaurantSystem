using AutoMapper;
using RestaurantSystem.Core.CustomEntities;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Shared.DTOs.Category;
using RestaurantSystem.Shared.DTOs.Menu;
using RestaurantSystem.Shared.DTOs;
using RestaurantSystem.Shared.DTOs.Order;
using RestaurantSystem.Shared.DTOs.Table;
using RestaurantSystem.Shared.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantSystem.Shared.DTOs.MenuItem;
using RestaurantSystem.Shared.OrderInvoice;

namespace RestaurantSystem.Infrastructure.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region INVOICE
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            CreateMap<InvoiceDetail, InvoiceDetailDto>().ReverseMap();

            CreateMap<Order, Invoice>()
                .ForMember(a => a.Id, b => b.Ignore())
                .ForMember(a => a.InvoiceDetails, b => b.MapFrom(b => b.OrderDetails))
                .ReverseMap();

            CreateMap<OrderDetail, InvoiceDetail>()
                 .ForMember(a => a.Id, b => b.Ignore())
                .ReverseMap();
            #endregion

            #region TABLE
            CreateMap<CreateOrEditTableDto, Table>().ReverseMap();
            CreateMap<CreateOrEditCategoryDto, MenuItemCategory>().ReverseMap();
            CreateMap<CreateOrEditMenuDto, Menu>().ReverseMap();
            CreateMap<CreateOrEditMenuItemDto, MenuItem>().ReverseMap();
            #endregion

            #region ORDER
            CreateMap<PlaceOrderDto, Order>()
                .ForMember(a => a.WaiterId, b => b.MapFrom(b => b.WaiterUserId))
                .ForMember(a => a.CheffId, b => b.MapFrom(b => b.CheffUserId));
            CreateMap<OrderItemDto, OrderDetail>();
            CreateMap<Order, ListOrderDto>()
                .ForMember(a => a.Status, b => b.MapFrom(b => b.Status.ToString()));
            CreateMap<OrderDetail, OrderDetailsDto>();
            #endregion
        }
    }
}
