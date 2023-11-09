using System;
using System.Collections.Generic;

namespace EsercitazioneApi.Entities;

public partial class ContiCorrente
{
    public long Id { get; set; }

    public long IdUtente { get; set; }

    public int Saldo { get; set; }

    public DateTime DataUltimaOperazione { get; set; }

    public virtual Utenti IdUtenteNavigation { get; set; } = null!;

    public ContiCorrente(long id, long idUtente)
    {
        Id = id;
        IdUtente = idUtente;
    }
}
