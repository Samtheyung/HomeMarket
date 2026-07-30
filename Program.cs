using HomeMarket.DbProfile;
using HomeMarket.Mappings;
using HomeMarket.Repository.Implementations;
using HomeMarket.Services.Implementations;
using HomeMarket.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAutoMapper(typeof(Mappings));
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<Mappings>();
});
// Add services to the container.
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<INotificationService, NotificationService>();


builder.Services.AddScoped<IImageService, ImageService>();


builder.Services.AddScoped<IDashboardService, DashboardService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddControllers();

// Register DbContext and connect to SQL Server
builder.Services.AddDbContext<HomeMarketDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HomeMarketDB")));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
