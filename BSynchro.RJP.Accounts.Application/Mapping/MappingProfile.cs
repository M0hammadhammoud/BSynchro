using AutoMapper;
using BSynchro.RJP.Accounts.Application.Models.DTOs;
using BSynchro.RJP.Accounts.Domain.Entities;
using BSynchro.RJP.Accounts.Domain.Models.DTOs.Transactions;
using Microsoft.AspNetCore.DataProtection;

namespace BSynchro.RJP.Accounts.Application.Mapping
{
    public class MappingProfile : Profile
    {
        private readonly IDataProtector _dataProtector;

        public MappingProfile(IDataProtector dataProtector)
        {
            _dataProtector = dataProtector;

            CreateMap<OpenAccountDTO, Account>();

            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => _dataProtector.Protect(src.Id.ToString())));

            CreateMap<Account, AccountDTO>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => _dataProtector.Protect(src.CustomerId.ToString())));
        }
    }
}
