using EsercitazioneApi.Dto;
using EsercitazioneApi.Entities;
using EsercitazioneApi.Models;
using EsercitazioneApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EsercitazioneApi.Controllers
{
    [ApiController]
    [Route("api/bankomat/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _repo;
        private readonly IDbRepository _dbrepo;

        public UsersController(IUsersRepository repo, IDbRepository dbrepo)
        {
            _repo = repo;
            _dbrepo = dbrepo;

        }

        [HttpGet]
        //public ActionResult<IEnumerable<User>> GetUsers()
        //{
        //    IEnumerable<User> users = _repo.GetUsers();
        //    return Ok(users);
        //}
        public async Task<ActionResult<IEnumerable<Utenti>>> GetUsers()
        {
            IEnumerable<Utenti> users = await _dbrepo.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{Id}", Name = nameof(GetUserById))]
        public async Task<ActionResult<IEnumerable<Utenti>>> GetUserById(int Id)
        {
            var userToreturn = _dbrepo.GetUsersAsync().Result.ToList().FirstOrDefault(u => u.Id == Id);
            return Ok(userToreturn);
        }

        //public ActionResult<User> GetUserById(int Id)
        //{
        //    var userToReturn = _repo.GetUsers().FirstOrDefault(u => u.Id == Id);

        //    if(userToReturn == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(userToReturn);
        //}

        //[HttpGet("{NomeUtente}")]
        //public ActionResult<User> GetUserByNomeUtente(string nomeUtente)
        //{
        //    var userToReturn = _repo.GetUsers().FirstOrDefault(u => u.NomeUtente == nomeUtente);

        //    if (userToReturn == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(userToReturn);
        //}

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] Utenti user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var UserId = _dbrepo.GetUsersAsync().Result.ToList().Max(u => u.Id);

            var newUser = new Utenti(UserId, user.IdBanca, user.NomeUtente, user.Password)
            {
                ContiCorrentes = new List<ContiCorrente>()
                {
                    new ContiCorrente(user.ContiCorrentes.Max(c => c.Id), UserId)
                    {
                        Saldo = 0
                    }
                },
                Banca = user.Banca,
            };

            await _dbrepo.CreateUserAsync(newUser);
            return NoContent();
        }
        //public ActionResult CreateUser([FromBody] User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    var UserId = _repo.GetUsers()
        //        .Max(p => p.Id);

        //    var newUser = new User()
        //    {
        //        Id = ++UserId,
        //        IdBanca = user.IdBanca,
        //        NomeUtente = user.NomeUtente,
        //        Password = user.Password,
        //        Bloccato = user.Bloccato,
        //    };
        //    _repo.AddUser(newUser);
        //    return CreatedAtRoute(nameof(GetUserById),
        //        routeValues: new
        //        {
        //            d = user.Id,
        //        },
        //        newUser);
        //}

        [HttpPut]
        public ActionResult UpdateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userToReturn = _repo.GetUsers()
                .FirstOrDefault(u => u.Id == user.Id);

            if (userToReturn == null)
            {
                return NotFound();
            }

            userToReturn.IdBanca = user.IdBanca;
            userToReturn.NomeUtente = user.NomeUtente;
            userToReturn.Password = user.Password;
            userToReturn.Bloccato = user.Bloccato;

            _repo.UpdateUser(userToReturn);

            return NoContent();
        }

        [HttpPut("{UserId:int}/newPassword/{password}")]
        public ActionResult UpdatePasswordOfUser(string password, int UserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userToReturn = _repo.GetUsers()
                .FirstOrDefault(u => u.Id == UserId);

            if (userToReturn == null)
            {
                return NotFound();
            }

            userToReturn.Password = password;

            _repo.UpdateUser(userToReturn);

            return NoContent();
        }

        [HttpDelete("{UserId:int}")]
        public async Task<ActionResult> DeleteUser(int UserId)
        {
            var userToDelete = _dbrepo.GetUsersAsync()
                .Result.ToList().FirstOrDefault(u => u.Id == UserId);

            if (userToDelete == null)
            {
                return NotFound();
            }

            await _dbrepo.DeleteUserAsync(userToDelete.Id);
            return NoContent();

        }
        //public ActionResult DeleteUser(int UserId)
        //{
        //    var userToDelete = _repo.GetUsers()
        //        .FirstOrDefault(u => u.Id == UserId);

        //    if (userToDelete == null)
        //    {
        //        return NotFound();
        //    }

        //    _repo.DeleteUserById(userToDelete);
        //    return NoContent();
        //}
    }
}
