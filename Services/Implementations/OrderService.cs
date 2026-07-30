using AutoMapper;
using HomeMarket.DTOs.Order;
using HomeMarket.Models.DbModels;
using HomeMarket.Repository.Implementations;
using HomeMarket.Services.Interfaces;

namespace HomeMarket.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomersRepository _customerRepository;
        private readonly IMapper _mapper;


        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            ICustomersRepository customerRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            var mappedOrders = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return mappedOrders;
        }


        public async Task<OrderConfirmationDto> PlaceOrderAsync(CreateOrderDto dto)
        {
            try
            {
                // 1. Find or create customer

                var customer = await _customerRepository.FindAsync(dto.Customer.Email, dto.Customer.PhoneNumber);


                if (customer == null)
                {
                    customer = _mapper.Map<Customers>(dto.Customer);

                    try
                    {
                        await _customerRepository.AddAsync(customer);
                    }
                    catch (Exception ex) 
                    {
                        throw new Exception($"Failed to add new customer\n{ex.StackTrace}");
                    }

                    
                }



                    decimal total = 0;


                var orderItems = new List<OrderItem>();


                // 2. Validate products

                foreach (var item in dto.Items)
                {
                    try
                    {
                        var product = await _productRepository.GetByIdAsync(item.ProductId);


                        if (product == null)
                            throw new Exception($"Product {item.ProductId} not found");


                        if (!product.IsAvailable)
                            throw new Exception($"{product.Name} is unavailable");



                        var orderItem = new OrderItem
                        {
                            ProductId = product.ProductId,
                            Quantity = item.Quantity,
                            UnitPrice = product.Price,
                            TotalPrice =
                                product.Price * item.Quantity
                        };


                        total += orderItem.TotalPrice;


                        orderItems.Add(orderItem);
                    }
                    catch (Exception ex) 
                    {
                        throw new Exception($"Failed to validate product {item.ProductId.ToString()}\n{ex.StackTrace}");
                    }
                   
                }



                // 3. Create order

                var order = new Order
                {
                    Customer = customer,
                    OrderDate = DateTime.UtcNow,
                    PaymentMethod = dto.PaymentMethod,
                    Status = OrderStatus.Pending,
                    TotalAmount = total,
                    Items = orderItems
                };

                try
                {
                    await _orderRepository.AddAsync(order);

                    return new OrderConfirmationDto
                    {
                        OrderId = order.OrderId,
                        OrderDate = order.OrderDate,
                        TotalAmount = order.TotalAmount,
                        Status = order.Status,
                        Message =
                          "Your order has been received"
                    };
                }
                catch(Exception ex)
                {
                    throw new Exception($"Failed to create order for customer {order.Customer.FirstName} {order.Customer.LastName}");
                }          
            }
            catch (Exception ex) 
            {
                throw new Exception($"Error while trying to validate customer\n{ex.StackTrace}");
            }
        }



        public async Task<OrderDto?> GetOrderAsync(int orderId)
        {
            var order =
                await _orderRepository.GetByIdAsync(orderId);


            if (order == null)
                return null;


            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(OrderStatus status)
        {
            var orders = await _orderRepository.GetByStatusAsync(status);

            var mappedOrders = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return mappedOrders;
        }



        public async Task UpdateStatusAsync(int orderId, OrderStatus status)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(orderId);
                if (order == null)
                    throw new Exception("Order not found");


                order.Status = status;

                try
                {
                    await _orderRepository.UpdateAsync(order);
                }
                catch(Exception ex)
                {
                    throw new Exception($"Failed to update order to status {status.ToString()}\n{ex.StackTrace}");
                }

                
            }
            catch (Exception ex) 
            {
                throw new Exception($"Failed to retrieve order\n{ex.StackTrace}");
            }
           
        }

        public async Task CancelOrderAsync(int orderId)
        {
            try
            {
                var order = await  _orderRepository.GetByIdAsync(orderId);
                
                if(order == null)
                {
                    throw new Exception($"Order not found");
                }

                await _orderRepository.DeleteAsync(order);
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to delete order");
            }
        }
    }
}
