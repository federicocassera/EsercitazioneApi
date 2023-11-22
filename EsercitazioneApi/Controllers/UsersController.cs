using EsercitazioneApi.Dto;
using EsercitazioneApi.Entities;
using EsercitazioneApi.Models;
using EsercitazioneApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace EsercitazioneApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _repo;
        private readonly IDbRepository _dbrepo;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepository repo, IDbRepository dbrepo, IMapper mapper)
        {
            _repo = repo;
            _dbrepo = dbrepo;
            _mapper = mapper;
        }

        [HttpGet]
        //public ActionResult<IEnumerable<User>> GetUsers()
        //{
        //    IEnumerable<User> users = _repo.GetUsers();
        //    return Ok(users);
        //}
        public async Task<ActionResult<IEnumerable<UsersDto>>> GetUsers()
        {
            IEnumerable<Utenti> users = await _dbrepo.GetUsersAsync();
            return Ok(_mapper.Map<IEnumerable<Utenti>, IEnumerable<UsersDto>>(users));
        }

        [HttpGet("{Id}", Name = nameof(GetUserById))]
        public async Task<ActionResult<IEnumerable<UsersDto>>> GetUserById(int Id)
        {
            var userToreturn = await _dbrepo.GetUserByIdAsync(Id);
            if (userToreturn == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Utenti, UsersDto>(userToreturn));
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
        public async Task<ActionResult> AddUser([FromBody] UtenteDtoToAdd user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("modello utente non valido");
            }

            //ricerca banca
            var banca = await _dbrepo.GetBankAsync(user.IdBanca);
            if (banca == null)
            {
                return BadRequest("nessuna banca trovata");
            }

            var userToReturn = await _dbrepo.CreateUserAsync(user);
            return CreatedAtRoute(nameof(GetUserById),
               routeValues: new
               {
                   Id = userToReturn.Id
               },
               user);
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
        public async Task<ActionResult> UpdateUser([FromBody] UtenteDtoToUpdate user, int UserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("modello utente non valido");
            }

            var userToReturn = await _dbrepo.GetUserByIdAsync(UserId);

            if (userToReturn == null)
            {
                return NotFound();
            }

            var result = await _dbrepo.UpdateUserAsync(user, UserId);

            //userToReturn.IdBanca = user.IdBanca;
            //userToReturn.NomeUtente = user.NomeUtente;
            //userToReturn.Password = user.Password;
            //userToReturn.Bloccato = user.Bloccato;

            return CreatedAtRoute(nameof(GetUserById),
               routeValues: new
               {
                   Id = userToReturn.Id
               },
               user);


        }

        //[HttpPut("{UserId:int}/newPassword/{password}")]
        //public ActionResult UpdatePasswordOfUser(string password, int UserId)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    var userToReturn = _repo.GetUsers()
        //        .FirstOrDefault(u => u.Id == UserId);

        //    if (userToReturn == null)
        //    {
        //        return NotFound();
        //    }

        //    userToReturn.Password = password;

        //    _repo.UpdateUser(userToReturn);

        //    return NoContent();
        //}

        [HttpDelete("{UserId:int}")]
        public async Task<ActionResult> DeleteUser(int UserId)
        {
            var userToDelete = _dbrepo.GetUserByIdAsync(UserId);
            if (userToDelete == null)
            {
                return NotFound();
            }

            var result = await _dbrepo.DeleteUserAsync(userToDelete.Id);
            if(result == true)
            {
                return NoContent();
            }

            return BadRequest("Utente non cancellabile");

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
