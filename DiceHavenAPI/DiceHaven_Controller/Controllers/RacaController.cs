using DiceHaven_BD.Contexts;
using DiceHaven_DTO;
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
    public class RacaController : ControllerBase
    {
        private DiceHavenBDContext dbDiceHaven;
        public RacaController(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        [ProducesResponseType(typeof(List<RacaDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Listar Raças", Description = "Lista todas as raças baseado em um ID_CAMPANHA.")]
        [HttpGet("ListarRacas")]
        public ActionResult ListarRacas(int idCampanha)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                Raca racaModel = new Raca(dbDiceHaven);

                return StatusCode(200, racaModel.ListarRacas(idCampanha));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(RacaDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Buscar raça", Description = "Busca uma raça baseado no ID_RACA.")]
        [HttpGet("ObterRaca")]
        public ActionResult ObterRaca(int idRaca)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                Raca racaModel = new Raca(dbDiceHaven);

                return StatusCode(200, racaModel.ObterRaca(idRaca));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Adicionar raça", Description = "Adiciona uma raça a campanha.")]
        [HttpPost("CadatrarRaca")]
        public ActionResult CadatrarRaca(RacaDTO novaRaca)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                Raca racaModel = new Raca(dbDiceHaven);
                racaModel.CadastrarRaca(novaRaca, idUsuarioLogado);

                return StatusCode(200, new {Message=$"Raça: {novaRaca.DS_RACA} Cadastrada com sucesso!"});
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Atualizar raça", Description = "Atualiza uma raça.")]
        [HttpPut("EditarRaca")]
        public ActionResult EditarRaca(RacaDTO raca)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                Raca racaModel = new Raca(dbDiceHaven);
                racaModel.EditarRaca(raca, idUsuarioLogado);

                return StatusCode(200, new { Message = $"Raça editada com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Deletar raça", Description = "Deleta uma raça.")]
        [HttpDelete("DeletarRaca")]
        public ActionResult DeletarRaca(int idRaca)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                Raca racaModel = new Raca(dbDiceHaven);
                racaModel.DeletarRaca(idRaca, idUsuarioLogado);

                return StatusCode(200, new { Message = $"Raça deletada com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

    }
}
