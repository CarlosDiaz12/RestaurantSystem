using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Infrastructure.Data;
using RestaurantSystem.Infrastructure.Repositories;
using RestaurantSystem.Infrastructure.Repositories.Base;
using RestaurantSystem.Infrastructure.Util.Reports;

namespace RestaurantSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Main db context
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Generic repository
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            // UnitOfWork
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // reports
            services.AddScoped<IReportGenerator, PDFReportGenerator>();
            return services;
        }
    }
}
