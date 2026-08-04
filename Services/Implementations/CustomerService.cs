using AutoMapper;
using HomeMarket.DTOs.Customer;
using HomeMarket.DTOs.Order;
using HomeMarket.Models.DbModels;
using HomeMarket.Repository.Implementations;
using HomeMarket.Services.Interfaces;

namespace HomeMarket.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomersRepository _repository;
        private readonly IMapper _mapper;


        public CustomerService(ICustomersRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        public async Task<IEnumerable<CustomerDto>> GetCustomerAsync()
        {
            var customers = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _repository.GetByIdAsync(customerId);
            return _mapper.Map<CustomerDto>(customer);
        }


        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto)
        {
            var customer = _mapper.Map<Customers>(dto);

            await _repository.AddAsync(customer);

            return _mapper.Map<CustomerDto>(customer);
        }



        public async Task<Customers?> FindCustomerAsync(string email, string phone)
        {
            return await _repository.FindAsync(email, phone);
        }

        public async Task<IEnumerable<OrderDto>> GetCustomerOrdersAsync(int customerId)
        {
            var customer = await _repository.GetByIdAsync(customerId);
            if (customer == null)
            {
                throw new Exception($"Customer with ID {customerId} not found.");
            }
            return _mapper.Map<IEnumerable<OrderDto>>(customer.Orders);
        }
    }
}
