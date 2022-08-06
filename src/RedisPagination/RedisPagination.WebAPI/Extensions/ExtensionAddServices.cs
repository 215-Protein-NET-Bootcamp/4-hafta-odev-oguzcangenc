using AutoMapper;
using RedisPagination.Business;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace RedisPagination.WebAPI.Extensions
{
    public static class ExtensionAddServices
    {
        public static void AddAutoMapperDependecyInjection(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            builder.Services.AddSingleton(mapperConfig.CreateMapper());
        }
        public static void AddRedisDependencyInjection(this IServiceCollection services, IConfiguration Configuration)
        {
            var configurationOptions = new ConfigurationOptions();
            configurationOptions.EndPoints.Add(Configuration["Redis:Host"], Convert.ToInt32(Configuration["Redis:Port"]));
            int.TryParse(Configuration["Redis:DefaultDatabase"], out int defaultDatabase);
            configurationOptions.DefaultDatabase = defaultDatabase;
            services.AddStackExchangeRedisCache(options =>
            {
                options.ConfigurationOptions = configurationOptions;
                options.InstanceName = Configuration["Redis:InstanceName"];
            });
        }        
    }

    
}
