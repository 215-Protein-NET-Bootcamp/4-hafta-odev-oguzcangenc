using Microsoft.EntityFrameworkCore;
using RedisPagination.Business;
using RedisPagination.Core;
using RedisPagination.Core.Extensions;
using RedisPagination.Data;
using RedisPagination.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IRelatePaginationUri>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new RelatePaginationUri(uri);
});
builder.Services.AddControllers();
builder.Services.AddAutoMapperDependecyInjection(builder);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataLayerServiceRegistration();
builder.Services.AddBussinessLayerServiceRegistration();

builder.Services.AddDbContext<AppEfDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"));


});

builder.Services.Configure<RedisConfig>(builder.Configuration.GetSection("Config"));
builder.Services.Configure<string>(builder.Configuration.GetSection("ConnectionStrings:PostgreSQL"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
