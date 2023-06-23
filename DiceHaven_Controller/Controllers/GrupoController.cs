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
    public class GrupoController : ControllerBase
    {
        private DiceHavenBDContext dbDiceHaven;
        public GrupoController(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        [ProducesResponseType(typeof(List<GrupoDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Listar grupos", Description = "Lista todos os grupos existentes no banco.")]
        [HttpGet("ListarGrupos")]
        public ActionResult ListarGrupos()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Grupo grupoModel = new Grupo(dbDiceHaven);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                permissaoModel.VerificaPermissaoUsuario(idUsuarioLogado, (int)Enumeration.Permissoes.PMS_Ver_Grupos);

                return StatusCode(200, grupoModel.ListarGrupos());

            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(List<UsuarioDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Listar usuarios em grupos", Description = "Lista todos os usuários pertencentes a um grupo.")]
        [HttpGet("ListarUsuariosGrupo")]
        public ActionResult ListarUsuariosGrupo(Enumeration.Grupo grupo)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Grupo grupoModel = new Grupo(dbDiceHaven);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                permissaoModel.VerificaPermissaoUsuario(idUsuarioLogado, (int)Enumeration.Permissoes.PMS_Ver_Grupos);
                return StatusCode(200, grupoModel.ListarUsuariosGrupo((int)grupo));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Vincula um usuário a um grupo", Description = "Vincula um usuário a um grupo.")]
        [HttpPost("VincularUsuarioGrupo")]
        public ActionResult VincularUsuarioGrupo(int ID_USUARIO, Enumeration.Grupo grupo)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Grupo grupoModel = new Grupo(dbDiceHaven);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                permissaoModel.VerificaPermissaoUsuario(idUsuarioLogado, (int)Enumeration.Permissoes.PMS_Adm_Grupos);
                grupoModel.VincularUsuarioGrupo(ID_USUARIO, (int)grupo);
                return StatusCode(200, new { Message = "Usuário Vinculado com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Desvincula um usuário de um grupo", Description = "Desvincula um usuário de um grupo.")]
        [HttpPost("DesvincularUsuarioGrupo")]
        public ActionResult DesvincularUsuarioGrupo(int ID_USUARIO, Enumeration.Grupo grupo)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Grupo grupoModel = new Grupo(dbDiceHaven);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                permissaoModel.VerificaPermissaoUsuario(idUsuarioLogado, (int)Enumeration.Permissoes.PMS_Adm_Grupos);
                grupoModel.DesvincularUsuarioGrupo(ID_USUARIO, (int)grupo);
                return StatusCode(200, new { Message = "Usuário Desvinculado com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }
    }
}
