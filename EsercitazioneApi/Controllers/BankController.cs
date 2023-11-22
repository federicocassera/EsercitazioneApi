using AutoMapper;
using EsercitazioneApi.Dto;
using EsercitazioneApi.Entities;
using EsercitazioneApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EsercitazioneApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankController : ControllerBase
    {
        private readonly IDbRepository _dbrepo;
        private readonly IMapper _mapper;

        public BankController(IDbRepository dbrepo, IMapper mapper)
        {
            _dbrepo = dbrepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankDto>>> GetBanks()
        {
            IEnumerable<Banche> banks = await _dbrepo.GetBanksAsync();
            return Ok(_mapper.Map<IEnumerable<Banche>, IEnumerable<BankDto>>(banks));
        }

        [HttpGet("{Id}", Name = nameof(GetBankById))]
        public async Task<ActionResult<IEnumerable<UsersDto>>> GetBankById(int Id)
        {
            var bankToreturn = await _dbrepo.GetBankAsync(Id);
            if (bankToreturn == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Banche, BankDto>(bankToreturn));
        }
    }
}
