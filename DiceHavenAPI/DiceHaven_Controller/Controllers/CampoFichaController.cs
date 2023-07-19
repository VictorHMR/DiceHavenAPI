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
    public class CampoFichaController : ControllerBase
    {
        private DiceHavenBDContext dbDiceHaven;
        public CampoFichaController(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        [ProducesResponseType(typeof(List<CampoFichaDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Lista todos os campos da ficha", Description = "Lista todos os campos da ficha baseado no ID_CAMPANHA")]
        [HttpGet("ListarCamposFicha")]
        public ActionResult ListarCamposFicha(int idCampanha)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                CampoFicha campoFichaModel = new CampoFicha(dbDiceHaven);

                return StatusCode(200, campoFichaModel.ListarCamposFicha(idCampanha));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(CampoFichaDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Busca um campo da ficha", Description = "Busca um campo da ficha baseado no ID")]
        [HttpGet("BuscarCampoFicha")]
        public ActionResult BuscarCampoFicha(int idCampoFicha)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                CampoFicha campoFichaModel = new CampoFicha(dbDiceHaven);

                return StatusCode(200, campoFichaModel.ObterCampoFicha(idCampoFicha));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Cadastra um campo no modelo de ficha", Description = "Cadastra um campo no modelo de ficha")]
        [HttpPost("CadastrarCampoFicha")]
        public ActionResult CadastrarCampoFicha(CampoFichaDTO novoCampo)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                CampoFicha campoFichaModel = new CampoFicha(dbDiceHaven);
                int idCampoFicha = campoFichaModel.CadastrarCampoFicha(novoCampo);
                return StatusCode(200, new {Message="Campo Cadastrado com sucesso no modelo de ficha.", Id=idCampoFicha});
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Edita um campo no modelo de ficha", Description = "Edita um campo no modelo de ficha")]
        [HttpPut("EditarCampoFicha")]
        public ActionResult EditarCampoFicha(CampoFichaDTO novoCampo)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                CampoFicha campoFichaModel = new CampoFicha(dbDiceHaven);
                campoFichaModel.EditarCampoFicha(novoCampo);
                return StatusCode(200, new { Message = "Campo Editado com sucesso no modelo de ficha."});
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Deleta um campo no modelo de ficha", Description = "Deleta um campo no modelo de ficha baseado no ID")]
        [HttpDelete("DeletarCampoFicha")]
        public ActionResult DeletarCampoFicha(int idCampoFicha)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                CampoFicha campoFichaModel = new CampoFicha(dbDiceHaven);
                string tipoAcao = campoFichaModel.DeletarCampoFicha(idCampoFicha) ? "deletado" : "desativado";
                return StatusCode(200, new { Message = $"Campo {tipoAcao} com sucesso do modelo de ficha." });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

    }
}
