namespace EsercitazioneApi.Dto
{
    public class MovementsDto
    {
        public string NomeBanca { get; set; } = null!;

        public string NomeUtente { get; set; } = null!;

        public string Funzionalita { get; set; } = null!;

        public int Quantita { get; set; }

        public DateTime DataOperazione { get; set; }
    }
}
