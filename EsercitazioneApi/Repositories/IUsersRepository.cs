using EsercitazioneApi.Models;

namespace EsercitazioneApi.Repositories
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetUsers();
        public bool AddUser(User user);
        public bool DeleteUserById(User user);
        public bool UpdateUser(User user);
    }
}
