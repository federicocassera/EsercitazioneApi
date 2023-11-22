using AutoMapper;
using EsercitazioneApi.Dto;
using EsercitazioneApi;

namespace BankomatApi.Profiles
{
    public class Banche_Funzionalita : Profile
    {
        public Banche_Funzionalita()
        {
            CreateMap<Banche_Funzionalita, BancheFunzionalitumDto>()
                .ReverseMap();
        }
    }
}