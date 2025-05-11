using DiceHaven_API.DTOs.Response;
using DiceHavenAPI.Contexts;
using DiceHavenAPI.DTOs;
using DiceHavenAPI.Interfaces;
using DiceHavenAPI.Services;
using DiceHavenAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace DiceHavenAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class PersonagemController : ControllerBase
    {
        private IPersonagem _personagem;

        public PersonagemController(IPersonagem personagem)
        {
            this._personagem = personagem;
        }

        [ProducesResponseType(typeof(PersonagemDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Lista todos os dados de um personagem", Description = "Lista dados detalhados de um personagem")]
        [HttpGet("ObterPersonagem")]
        public ActionResult ObterPersonagem(int idPersonagem)
        {
            try
            {
                return StatusCode(200, _personagem.ObterPersonagem(idPersonagem));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(List<PersonagemDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Lista todos os personagens de um usuário", Description = "Lista todos os personagens de um usuário. por padrão o usuário logado")]
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
