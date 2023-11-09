using EsercitazioneApi.Entities;

namespace EsercitazioneApi.Dto
{
    public class BankAccountDto
    { 
        public long IdUtente { get; set; }

        public int Saldo { get; set; }

        public DateTime DataUltimaOperazione { get; set; }
    }
}
