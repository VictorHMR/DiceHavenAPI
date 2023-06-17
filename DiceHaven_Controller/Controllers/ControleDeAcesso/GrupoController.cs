using DiceHaven_BD.Contexts;
using DiceHaven_Model.Models.ControlleDeAcesso;
using DiceHaven_Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DiceHaven_Controller.Controllers.ControleDeAcesso
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class GrupoController : ControllerBase
    {
        private DiceHavenBDContext dbDiceHaven;
        public GrupoController(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        [HttpGet("ListarGrupos")]
        public ActionResult ListarGrupos()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuario = int.Parse(claim[0].Value);
                Grupo grupoModel = new Grupo(dbDiceHaven);

                return StatusCode(200, grupoModel.ListarGrupos());

            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [HttpGet("ListarUsuariosGrupo")]
        public ActionResult ListarUsuariosGrupo(int idGrupo)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                Grupo grupoModel = new Grupo(dbDiceHaven);

                return StatusCode(200, grupoModel.ListarUsuariosGrupo(idGrupo));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [HttpPost("VincularUsuarioGrupo")]
        public ActionResult VincularUsuarioGrupo(int ID_USUARIO, int ID_GRUPO)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                Grupo grupoModel = new Grupo(dbDiceHaven);
                grupoModel.VincularUsuarioGrupo(ID_USUARIO, ID_GRUPO);
                return StatusCode(200, new { Message = "Usuário Vinculado com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [HttpPost("DesvincularUsuarioGrupo")]
        public ActionResult DesvincularUsuarioGrupo(int ID_USUARIO, int ID_GRUPO)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                Grupo grupoModel = new Grupo(dbDiceHaven);
                grupoModel.DesvincularUsuarioGrupo(ID_USUARIO, ID_GRUPO);
                return StatusCode(200, new { Message = "Usuário Desvinculado com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }
    }
}
