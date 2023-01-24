using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ResturantSystem.Application.Interfaces;
using ResturantSystem.Application.Services;

namespace ResturantSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IUserService, UserServices>();
            services.AddScoped<IRolServices, RolServices>();
            services.AddScoped<IMenuItemCategoryService, MenuItemCategoryService>();
            services.AddScoped<IMenuItemService, MenuItemService>();

            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IInvoiceService, InvoiceService>();

            return services;
        }
    }
}
