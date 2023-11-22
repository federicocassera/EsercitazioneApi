using AutoMapper;
using EsercitazioneApi.Dto;
using EsercitazioneApi;
using EsercitazioneApi.Entities;

namespace BankomatApi.Profiles
{
    public class UtentiProfile : Profile
    {
        public UtentiProfile()
        {
            CreateMap<Utenti, UsersDto>()
                .ReverseMap();
            CreateMap<Utenti, UtenteDtoToAdd>()
                .ReverseMap();
            CreateMap<Utenti, UtenteDtoToUpdate>()
                .ReverseMap();
            CreateMap<UtenteDtoToAdd, UtenteDtoToUpdate>()
                .ReverseMap();

        }
    }
}
