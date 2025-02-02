using AutoMapper;
using BSynchro.RJP.Accounts.Application.Models.DTOs;
using BSynchro.RJP.Accounts.WebAPI.Models.Requests.Accounts;
using BSynchro.RJP.Accounts.WebAPI.Models.Responses;
using BSynchro.RJP.Accounts.WebAPI.Models.Responses.Customers;
using Microsoft.AspNetCore.DataProtection;

namespace BSynchro.RJP.Accounts.WebAPI.Mapping
{
    public class MappingProfile : Profile
    {
        private readonly IDataProtector _dataProtector;
        public MappingProfile(IDataProtector dataProtector)
        {

            _dataProtector = dataProtector;

            CreateMap<OpenAccountRequest, OpenAccountDTO>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => _dataProtector.Unprotect(src.CustomerId)));

            CreateMap<string, BaseResponse>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src));

            CreateMap<List<CustomerDTO>, GetAllCustomersResponse>()
                .ForMember(dest => dest.Customers, opt => opt.MapFrom(src => src));
        }
    }
}
