using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Product.Data;
using Product.Repository;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var configuration = builder.Configuration;

builder.Services.AddDbContext<DataContext>(options=> options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDataContext>(provider => provider.GetService<DataContext>());
builder.Services.AddScoped<IProductRepository,ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
