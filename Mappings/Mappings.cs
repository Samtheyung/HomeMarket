using AutoMapper;
using HomeMarket.DTOs.Category;
using HomeMarket.DTOs.Customer;
using HomeMarket.DTOs.Dashboard;
using HomeMarket.DTOs.Order;
using HomeMarket.DTOs.Product;
using HomeMarket.Models.DbModels;

namespace HomeMarket.Mappings
{
    public class Mappings : Profile
    {
        public Mappings() {
            
            //Category Mappings
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();

            //Customer Mappings
            CreateMap<CustomerDto, Customers>().ReverseMap();
            CreateMap<Customers, CustomerDto>().ReverseMap();
            CreateMap<CreateCustomerDto, Customers>().ReverseMap();
            CreateMap<Customers, CreateCustomerDto>().ReverseMap();

            //Product Mappings
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductDto, Product>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<UpdateProductDto, Product>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();

            //Order Mappings
            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<CreateOrderDto, Order>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<OrderConfirmationDto, Order>().ReverseMap();
            CreateMap<Order, OrderConfirmationDto>().ReverseMap();

            //Order Item Mappings
            CreateMap<OrderItemDto, OrderItem>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();

            //Dashboard Mappings
            //CreateMap<DashboardDto, Dashboard>().ReverseMap();

        }
    }
}
