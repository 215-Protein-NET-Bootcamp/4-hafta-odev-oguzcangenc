using Microsoft.EntityFrameworkCore;
using RedisPagination.Core;
using RedisPagination.Core.Data;
using RedisPagination.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
