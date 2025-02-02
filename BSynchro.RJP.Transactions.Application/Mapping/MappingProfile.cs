using AutoMapper;
using Microsoft.AspNetCore.DataProtection;

namespace BSynchro.RJP.Transactions.Application.Mapping
{
    public class MappingProfile : Profile
    {
        private readonly IDataProtector _dataProtector;

        public MappingProfile(IDataProtector dataProtector)
        {
            _dataProtector = dataProtector;
        }
    }
}
