using EsercitazioneApi.DbContexts;
using EsercitazioneApi.Dto;
using EsercitazioneApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EsercitazioneApi.Repositories
{
    public class DbRepository : IDbRepository
    {
        private BankomatContext _ctx;

        public DbRepository(BankomatContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }
        public async Task<bool> CreateUserAsync(Utenti user)
        {
            _ctx.Utenti.Add(user);
            return await SaveChanges();
        }

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
            var banks = await _ctx.Banche.ToListAsync();
            return banks;
        }

        public async Task<Banche?> GetBankAsync(int bankId)
        {
            return await _ctx.Banche
                .FirstOrDefaultAsync(c => c.Id == bankId);
        }

        public async Task<Utenti?> GetUserByIdAsync(int userId)
        {
            return await _ctx.Utenti
                .FirstOrDefaultAsync(c => c.Id == userId);
        }

        public async Task<IEnumerable<Utenti>> GetUsersAsync()
        {
            var users = await _ctx.Utenti.ToListAsync();
            return users;
        }

        public async Task<bool> SaveChanges()
        {
            return (await _ctx.SaveChangesAsync() >= 0);
        }

        public Task<bool> UpdateUserAsync(Utenti user, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
