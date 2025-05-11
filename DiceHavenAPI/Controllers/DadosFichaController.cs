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
    public class DadosFichaController : ControllerBase
    {
        private IDadosFicha _dadosFicha;
        public DadosFichaController(IDadosFicha dadosFicha)
        {
            this._dadosFicha = dadosFicha;
        }

        [ProducesResponseType(typeof(List<DadosFichaDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Lista todos os dados da ficha", Description = "Lista todos os dados da ficha baseado no ID_CAMPANHA")]
        [HttpGet("ListarDadosFicha")]
        public ActionResult ListarDadosFicha(int idCampanha, int idPersonagem)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                return StatusCode(200, _dadosFicha.ListarDadosFicha(idCampanha, idPersonagem));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(DadosFichaDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Exibe o valor de um campo da ficha", Description = "Exibe o valor de um campo da ficha")]
        [HttpGet("ObterDadosFicha")]
        public ActionResult ObterDadosFicha(int idCampoFicha, int idPersonagem)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                return StatusCode(200, _dadosFicha.ObterDadosFicha(idCampoFicha, idPersonagem));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Criar ficha", Description = "Cria uma ficha em branco para um personagem")]
        [HttpPost("CriarFicha")]
        public ActionResult CriarFicha(int idCampanha, int idPersonagem)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                _dadosFicha.GerarFichaPersonagem(idPersonagem, idCampanha);
                return StatusCode(200, new {Message="Ficha criada com sucesso!"});
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Edita o valor de um campo da ficha", Description = "Edita o valor de um campo da ficha")]
        [HttpPut("AtualizarDadoFicha")]
        public ActionResult AtualizarDadoFicha([FromForm] DadosFichaDTO novosDados)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                _dadosFicha.AtualizarDadosFicha(novosDados);
                return StatusCode(200, new { Message = "Dado alterado com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }
    }
}
