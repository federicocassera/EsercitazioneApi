using AutoMapper;
using EsercitazioneApi.Dto;
using EsercitazioneApi;
using EsercitazioneApi.Entities;

namespace BankomatApi.Profiles
{
    public class BancheProfile : Profile
    {
        public BancheProfile()
        {
            CreateMap<Banche, BankDto>()
                .ReverseMap();


        }
    }
}
