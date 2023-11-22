using EsercitazioneApi.Dto;
using EsercitazioneApi.Entities;

namespace EsercitazioneApi.Repositories
{
    public interface IDbRepository
    {
        Task<IEnumerable<Utenti>> GetUsersAsync();
        Task<Utenti?> GetUserByIdAsync(long userId);
        Task<Utenti> CreateUserAsync(UtenteDtoToAdd user);
        Task<bool> DeleteUserAsync(long userId);
        Task<bool> UpdateUserAsync(UtenteDtoToUpdate user, long userId);

        Task<IEnumerable<Banche>> GetBanksAsync();
        Task<Banche?> GetBankAsync(long bankId);

        Task<IEnumerable<Funzionalitum>> GetFunzionalitumAsync();
        Task<Funzionalitum> GetFunzionalitumById(long id);

        Task<BancheFunzionalitum> GetBancheFunzionalitumAsync(long IdBanca, long IdFunzionalitum);
        Task<bool> RemoveBancheFunzionalitum(BancheFunzionalitum banchefunc);


        Task<bool> AddBnacheFunzionalitumAsync(long IdBanca, long IdFunzionalitum);
        Task<IEnumerable<Funzionalitum>> GetBancaFunzionalitum(long IdBanca);

        Task<bool> SaveChanges();
    }
}
