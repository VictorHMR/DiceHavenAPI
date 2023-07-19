using DiceHaven_BD.Contexts;
using DiceHaven_DTO;
using DiceHaven_Model.Interfaces;
using DiceHaven_Model.Models;
using DiceHaven_Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace DiceHaven_Controller.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class PersonagemController : ControllerBase
    {
        private DiceHavenBDContext dbDiceHaven;
        private readonly IConfiguration _configuration;
        private IPersonagem _personagem;

        public PersonagemController(DiceHavenBDContext dbDiceHaven, IConfiguration configuration, IPersonagem personagem)
        {
            this.dbDiceHaven = dbDiceHaven;
            this._configuration = configuration;
            this._personagem = personagem;
        }

        [ProducesResponseType(typeof(List<PersonagemDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Lista todos os personagens de um usuário", 
            Description = "Lista todos os personagens de um usuário. por padrão o usuário logado")]
        [HttpGet("ListarPersonagens")]
        public ActionResult ListarPersonagens(int? idUsuario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                List<PersonagemDTO> listaPersonagem;
                if (idUsuario == 0 || idUsuario is null)
                    listaPersonagem = _personagem.ListarPersonagem(idUsuarioLogado);
                else
                    listaPersonagem = _personagem.ListarPersonagem(idUsuario ?? 0);

                return StatusCode(200, listaPersonagem);

            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Cadastra um novo personagem", Description = "Cadastra um novo personagem")]
        [HttpPost("CadastrarPersonagem")]
        public ActionResult CadastrarPersonagem(PersonagemDTO novoPersonagem)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                novoPersonagem.ID_USUARIO = idUsuarioLogado;
                _personagem.CadastrarPersonagem(novoPersonagem);

                return StatusCode(200, new { Message = "Personagem Cadastrado com sucesso!" });
            }
            catch(HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Edita um personagem", Description = "Edita os dados de um personagem")]
        [HttpPut("EditarPersonagem")]
        public ActionResult EditarPersonagem(PersonagemDTO novoPersonagem)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                _personagem.EditarPersonagem(novoPersonagem);

                return StatusCode(200, new { Message = "Personagem editado com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

    }
}
