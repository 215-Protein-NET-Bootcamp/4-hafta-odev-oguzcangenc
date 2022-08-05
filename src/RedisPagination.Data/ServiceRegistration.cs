using Microsoft.Extensions.DependencyInjection;

namespace RedisPagination.Data
{
    public static class ServiceRegistration
    {
        public static void AddDataLayerServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
