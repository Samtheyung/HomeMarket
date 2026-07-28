using AutoMapper;
using HomeMarket.DTOs.Customer;
using HomeMarket.Models.DbModels;
using HomeMarket.Repository.Implementations;
using HomeMarket.Services.Interfaces;

namespace HomeMarket.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomersRepository _repository;
        private readonly IMapper _mapper;


        public CustomerService(
            ICustomersRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto)
        {
            var customer = _mapper.Map<Customers>(dto);

            await _repository.AddAsync(customer);

            return _mapper.Map<CustomerDto>(customer);
        }



        public async Task<Customers?> FindCustomerAsync(
            string email,
            string phone)
        {
            return await _repository
                .FindAsync(email, phone);
        }
    }
}
