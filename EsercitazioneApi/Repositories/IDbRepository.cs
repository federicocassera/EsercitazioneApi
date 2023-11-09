using EsercitazioneApi.Entities;

namespace EsercitazioneApi.Repositories
{
    public interface IDbRepository
    {
        Task<IEnumerable<Utenti>> GetUsersAsync();

        Task<Utenti?> GetUserByIdAsync(int userId);

        Task<bool> CreateUserAsync(Utenti user);

        Task<bool> DeleteUserAsync(long userId);

        Task<bool> UpdateUserAsync(Utenti user, int userId);

        Task<IEnumerable<Banche>> GetBanksAsync();

        Task<Banche?> GetBankAsync(int bankId);

        Task<bool> SaveChanges();
    }
}
