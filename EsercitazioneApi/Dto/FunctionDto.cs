using EsercitazioneApi.Entities;

namespace EsercitazioneApi.Dto
{
    public class FunctionDto
    { 
        public string Nome { get; set; } = null!;

        public virtual ICollection<BancheFunzionalitum> BancheFunzionalita { get; set; } = new List<BancheFunzionalitum>();
    }
}
