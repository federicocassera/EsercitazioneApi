using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsercitazioneApi.Entities;

public partial class Utenti
{
    public long Id { get; set; }

    public long IdBanca { get; set; }

    public string NomeUtente { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Bloccato { get; set; }

    public virtual ICollection<ContiCorrente> ContiCorrentes { get; set; } = new List<ContiCorrente>();

    public virtual Banche Banca { get; set; } = null!;

    //public Utenti(long id, long idBanca, string nomeUtente, string password)
    //{
    //    Id = id;
    //    IdBanca = idBanca;
    //    NomeUtente = nomeUtente;
    //    Password = password;
    //}
}
