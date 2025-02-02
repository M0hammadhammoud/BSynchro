using AutoMapper;
using BSynchro.RJP.Accounts.Application.Contracts;
using BSynchro.RJP.Accounts.Application.Models.DTOs;
using BSynchro.RJP.Accounts.Domain.Contracts;
using BSynchro.RJP.Accounts.Domain.Entities;

namespace BSynchro.RJP.Accounts.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, 
                               IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _unitOfWork.Repository<Customer>().GetAllAsync();
            return _mapper.Map<List<CustomerDTO>>(customers);
        }

        //public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        //{
        //    var customers = await _unitOfWork.Repository<Customer>().GetAllAsync();
        //    return _mapper.Map<List<CustomerDTO>>(customers);
        //}
    }
}
