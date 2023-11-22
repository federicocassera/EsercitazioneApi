using AutoMapper;
using EsercitazioneApi.Dto;
using EsercitazioneApi;
using EsercitazioneApi.Entities;

namespace BankomatApi.Profiles
{
    public class ContiCorrenteProfile : Profile
    {
        public ContiCorrenteProfile()
        {
            CreateMap<ContiCorrente, BankAccountDto>()
                .ReverseMap();


        }
    }
}
