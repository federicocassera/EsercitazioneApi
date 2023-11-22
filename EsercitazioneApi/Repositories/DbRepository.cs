using EsercitazioneApi.DbContexts;
using EsercitazioneApi.Dto;
using EsercitazioneApi.Entities;
using EsercitazioneApi.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace EsercitazioneApi.Repositories
{
    public class DbRepository : IDbRepository
    {
        private BankomatContext _ctx;
        private readonly IMapper _mapper;

        public DbRepository(BankomatContext ctx, IMapper mapper)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
            _mapper = mapper;
        }

        public async Task<bool> SaveChanges()
        {
            return (await _ctx.SaveChangesAsync() >= 0);
        }

        public async Task<Utenti> CreateUserAsync(UtenteDtoToAdd user)
        {
            //mapping
            Utenti utenteToAdd = _mapper.Map<Utenti>(user);

            ContiCorrente conto = new ContiCorrente();
             

            conto.Saldo = 0;
            conto.DataUltimaOperazione = DateTime.Today;

            utenteToAdd.ContiCorrentes.Add(conto);
            utenteToAdd.Bloccato = false;
            utenteToAdd.IdBanca = user.IdBanca;

            await _ctx.Utenti.AddAsync(utenteToAdd);
            await SaveChanges();
            return utenteToAdd;
        }

        //public async Task<bool> CreateContoAsync(ContiCorrente conto)
        //{
        //    _ctx.ContiCorrente.Add(conto);
        //    return await SaveChanges();
        //}

        public async Task<bool> DeleteUserAsync(long userId)
        {
            var userToDelete = await _ctx.Utenti
               .FirstOrDefaultAsync(c => c.Id == userId);

            bool UtenteRimuovibile = true;
            var utente = GetUsersAsync().Result.ToList().FirstOrDefault(u => u.Id == userId);
            var contiUtente = utente.ContiCorrentes.ToList();
            if (contiUtente != null)
            {
                foreach (var conto in contiUtente)
                {
                    if (conto.Saldo > 0)
                    {
                        UtenteRimuovibile = false;
                    }
                }
                if (UtenteRimuovibile)
                {
                    foreach (var conto in contiUtente)
                    {
                        _ctx.ContiCorrente.Remove(conto);
                    }

                    _ctx.Utenti.Remove(utente);
                        
                    return await SaveChanges();

                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    _ctx.Utenti.Remove(utente);
                    return await SaveChanges();
                }
        }

        public async Task<IEnumerable<Banche>> GetBanksAsync()
        {
            return await _ctx.Banche.ToListAsync();
        }

        public async Task<Banche?> GetBankAsync(long bankId)
        {
            return await _ctx.Banche
                .FirstOrDefaultAsync(c => c.Id == bankId);
        }

        public async Task<Utenti?> GetUserByIdAsync(long userId)
        {
            return await _ctx.Utenti
                .FirstOrDefaultAsync(c => c.Id == userId);
        }

        public async Task<IEnumerable<Utenti>> GetUsersAsync()
        {
            var users = await _ctx.Utenti.ToListAsync();
            return users;
        }

        public async Task<bool> UpdateUserAsync(UtenteDtoToUpdate user, long userId)
        {
            var utente = await _ctx.Utenti.FirstOrDefaultAsync(u => u.Id == userId);
            utente.NomeUtente = user.NomeUtente;
            utente.Password = user.Password;
            utente.Bloccato = user.Bloccato;
            return await SaveChanges();
        }

        public Task<IEnumerable<Funzionalitum>> GetFunzionalitumAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Funzionalitum> GetFunzionalitumById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<BancheFunzionalitum> GetBancheFunzionalitumAsync(long IdBanca, long IdFunzionalitum)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveBancheFunzionalitum(BancheFunzionalitum banchefunc)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddBnacheFunzionalitumAsync(long IdBanca, long IdFunzionalitum)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Funzionalitum>> GetBancaFunzionalitum(long IdBanca)
        {
            throw new NotImplementedException();
        }
    }
}
