namespace RestaurantSystem.Server
{
    public static class DependencyInjection
    {
       public static IServiceCollection AddServerServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
