using EsercitazioneApi.Entities;

namespace EsercitazioneApi.Dto
{
    public class BankDto
    { 
        public string Nome { get; set; } = null!;

        public virtual ICollection<BancheFunzionalitum> BancheFunzionalita { get; set; } = new List<BancheFunzionalitum>();
    }
}
