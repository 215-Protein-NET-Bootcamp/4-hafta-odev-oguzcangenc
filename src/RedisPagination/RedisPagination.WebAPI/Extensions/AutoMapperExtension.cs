using AutoMapper;
using RedisPagination.Business;

namespace RedisPagination.WebAPI.Extensions
{
    public static class AutoMapperExtension
    {
        public static void AddAutoMapperDependecyInjection(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            builder.Services.AddSingleton(mapperConfig.CreateMapper());
        }
    }
}
