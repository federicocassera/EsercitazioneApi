using AutoMapper;
using EsercitazioneApi.Dto;
using EsercitazioneApi;
using EsercitazioneApi.Entities;

namespace BankomatApi.Profiles
{
    public class FunzionalitaProfile : Profile
    {
        public FunzionalitaProfile()
        {
            CreateMap<Funzionalitum, FunctionDto>()
                .ReverseMap();


        }
    }
}
