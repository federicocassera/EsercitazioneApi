using EsercitazioneApi.Entities;

namespace EsercitazioneApi.Dto
{
    public class UsersDto
    {
        public long IdBanca { get; set; }

        public string NomeUtente { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool Bloccato { get; set; }

        public virtual ICollection<ContiCorrente> ContiCorrentes { get; set; } = new List<ContiCorrente>();
    }
}
