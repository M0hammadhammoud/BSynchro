using AutoMapper;
using BSynchro.RJP.Transactions.Application.Models.DTOs;
using BSynchro.RJP.Transactions.WebAPI.Models.Requests.Transactions;
using BSynchro.RJP.Transactions.WebAPI.Models.Responses;
using Microsoft.AspNetCore.DataProtection;

namespace BSynchro.RJP.Transactions.WebAPI.Mapping
{
    public class MappingProfile : Profile
    {
        private readonly IDataProtector _dataProtector;
        public MappingProfile(IDataProtector dataProtector)
        {

            _dataProtector = dataProtector;

            CreateMap<CreateTransactionRequest, TransactionDTO>();

            CreateMap<(bool IsSuccess, string Message), BaseResponse>()
               .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message));
        }
    }
}
