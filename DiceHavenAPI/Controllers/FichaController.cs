using DiceHaven_API.DTOs.Response;
using DiceHavenAPI.Contexts;
using DiceHavenAPI.DTOs;
using DiceHavenAPI.Interfaces;
using DiceHavenAPI.Models;
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
    public class FichaController : ControllerBase
    {
        private IFicha _fichaService;
        public FichaController(IFicha fichaService)
        {
            this._fichaService = fichaService;
        }

        [ProducesResponseType(typeof(List<SecaoFichaDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Lista todos os campos da ficha", Description = "Lista todos os campos da ficha baseado no ID_CAMPANHA")]
        [HttpGet("ObterModeloDeFicha")]
        public ActionResult ObterModeloDeFicha(int idCampanha)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                return StatusCode(200, _fichaService.ObterModeloDeFicha(idCampanha));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Edita os campos no modelo de ficha", Description = "Edita os campos no modelo de ficha")]
        [HttpPut("EditarModeloDeFicha")]
        public ActionResult EditarCampoFicha([FromBody] List<SecaoFichaDTO> lstSecoes)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                _fichaService.EditarModeloDeFicha(lstSecoes);
                return StatusCode(200, new { Message = "Campo Editado com sucesso no modelo de ficha." });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }


        [ProducesResponseType(typeof(FichaDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Lista todos os dados da ficha", Description = "Lista todos os dados da ficha baseado no ID_CAMPANHA")]
        [HttpGet("ObterFicha")]
        public ActionResult ObterFicha(int idCampanha, int? idPersonagem)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                return StatusCode(200, _fichaService.ListarDadosFicha(idCampanha, idPersonagem));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }
    }
}
