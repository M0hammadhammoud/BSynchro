using AutoMapper;
using BSynchro.RJP.Accounts.Application.Models.DTOs;
using BSynchro.RJP.Accounts.Domain.Entities;

namespace BSynchro.RJP.Accounts.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OpenAccountDTO, Account>();
        }
    }
}
