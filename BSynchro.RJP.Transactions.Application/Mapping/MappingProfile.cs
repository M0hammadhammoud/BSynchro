using AutoMapper;
using BSynchro.RJP.Transactions.Application.Models.DTOs;
using BSynchro.RJP.Transactions.Domain.Documents;
using Microsoft.AspNetCore.DataProtection;

namespace BSynchro.RJP.Transactions.Application.Mapping
{
    public class MappingProfile : Profile
    {
        private readonly IDataProtector _dataProtector;

        public MappingProfile(IDataProtector dataProtector)
        {
            _dataProtector = dataProtector;

            CreateMap<TransactionDTO, Transaction>().ReverseMap();
        }
    }
}
