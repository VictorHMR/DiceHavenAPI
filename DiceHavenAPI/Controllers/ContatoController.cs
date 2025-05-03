using DiceHavenAPI.Contexts;
using DiceHavenAPI.DTOs;
using DiceHavenAPI.Interfaces;
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
    public class ContatoController : ControllerBase
    {
        private DiceHavenBDContext dbDiceHaven;
        private IContato _contato;

        public ContatoController(DiceHavenBDContext dbDiceHaven, IContato contato)
        {
            this.dbDiceHaven = dbDiceHaven;
            this._contato = contato;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Adicionar Usuário a lista de contatos",
           Description = "Adiciona um usuário a lista de contatos do usuário logado")]
        [HttpPost("AdicionarContato")]
        public ActionResult AdicionarContato(int idUsuario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                _contato.AdicionarContato(idUsuario, idUsuarioLogado);

                return StatusCode(200, new {Message="Contato Adicionado com sucesso!"});

            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Remove um Usuário da lista de contatos",
           Description = "Remove um usuário da lista de contatos do usuário logado")]
        [HttpDelete("RemoverContato")]
        public ActionResult RemoverContato(int idUsuario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                _contato.RemoverContato(idUsuario, idUsuarioLogado);

                return StatusCode(200, new { Message = "Contato Removido com sucesso!" });

            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(List<ContatoDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Retorna a lista de contatos",
           Description = "Lista todos os contatos do usuário logado")]
        [HttpGet("ListarContatos")]
        public ActionResult ListarContatos()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                
                return StatusCode(200, _contato.ListarContatos(idUsuarioLogado));

            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Mutar ou desmutar um contato",
   Description = "Muta ou desmuta um usuário para não receber notificações ou não")]
        [HttpPut("MuteDesmuteContato")]
        public ActionResult MuteDesmuteContato(int idUsuarioContato, bool flMute)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                _contato.MuteDesmuteContato(idUsuarioContato,flMute);

                return StatusCode(200, new { Message = "Contato" + (flMute ? "mutado": "desmutado") +  "com sucesso!" });

            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

    }
}
