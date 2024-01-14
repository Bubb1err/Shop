using CatalogAPI.Data;
using CatalogAPI.Data.Repositories.Implementations;
using CatalogAPI.Data.Repositories.Intefaces;
using CatalogAPI.Grpc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();

builder.Services.AddGrpc();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CatalogDbContext>(options =>
{
  options.UseNpgsql(builder.Configuration.GetConnectionString("CatalogDatabase"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGrpcService<CatalogService>();
app.MapControllers();

app.Run();
