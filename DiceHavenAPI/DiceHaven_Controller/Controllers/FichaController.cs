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
    [Route("api/[controller]")]
    [ApiController]
    public class FichaController : ControllerBase
    {
        private DiceHavenBDContext dbDiceHaven;
        public FichaController(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        [ProducesResponseType(typeof(List<FichaDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Listar fichas", Description = "Lista todas as fichas, do usuário, da campanha ou geral.")]
        [HttpGet("ListarFichas")]
        public ActionResult ListarFichas(int? idUsuario, int? idCampanha)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Ficha fichaModel = new Ficha(dbDiceHaven);

                return StatusCode(200, fichaModel.ListarFichas(idCampanha, idUsuario ?? idUsuarioLogado));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(FichaDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Obter ficha", Description = "Busca uma ficha baseado no ID_PERSONAGEM e ID_CAMPANHA fornecido.")]
        [HttpGet("BuscarFicha")]
        public ActionResult BuscarFicha(int idPersonagem, int idCampanha)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Ficha fichaModel = new Ficha(dbDiceHaven);

                return StatusCode(200, fichaModel.ObterFicha(idPersonagem, idCampanha));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Criar ficha", Description = "Cria uma nova ficha com as informações recebidas.")]
        [HttpPost("CadastrarFicha")]
        public ActionResult CadastrarFicha(FichaDTO novaFicha)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Ficha fichaModel = new Ficha(dbDiceHaven);
                int idFicha = fichaModel.CadastrarFicha(novaFicha);
                return StatusCode(200, new {Message=$"Nova ficha criada com sucesso !", Id=idFicha});
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

    }
}
