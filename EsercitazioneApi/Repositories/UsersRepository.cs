using EsercitazioneApi.Models;

namespace EsercitazioneApi.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public List<User> users { get; set; }
        public UsersRepository() 
        {
            users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    IdBanca = 5,
                    NomeUtente = "dario",
                    Password = "dario",
                    Bloccato = false
                },
                new User()
                {
                    Id = 2,
                    IdBanca = 3,
                    NomeUtente = "carlo",
                    Password = "carlo",
                    Bloccato = false
                }
            };
        }

        public IEnumerable<User> GetUsers()
        {
            return users;
        }

        public bool AddUser(User user)
        {
            users.Add(user);
            return true;
        }

        public bool DeleteUserById(User user)
        {
            users.Remove(user);
            return true;
        }

        public bool UpdateUser(User user)
        {
            var userToUpdate = users.FirstOrDefault(u => u.Id == user.Id);

            userToUpdate.IdBanca = user.IdBanca;
            userToUpdate.NomeUtente = user.NomeUtente;
            userToUpdate.Password = user.Password;
            userToUpdate.Bloccato = user.Bloccato;
            return true;
        }
    }
}
