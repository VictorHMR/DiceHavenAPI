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
    public class PermissaoController : ControllerBase
    {
        private DiceHavenBDContext dbDiceHaven;
        public PermissaoController(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        [ProducesResponseType(typeof(List<PermissaoDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Lista todas as permissões", Description = "Lista todas as permissões existentes no banco.")]
        [HttpGet("ListarPermissoes")]
        public ActionResult ListarPermissoes()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                permissaoModel.VerificaPermissaoUsuario(idUsuarioLogado, (int)Enumeration.Permissoes.PMS_Ver_Permissao);

                return StatusCode(200, permissaoModel.ListarPermissoes());
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(List<PermissaoDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Lista todas as permissões de um grupo", Description = "Lista todas as permissões vinculadas e um grupo.")]
        [HttpGet("ListarPermissoesGrupo")]
        public ActionResult ListarPermissoesGrupo(Enumeration.Grupo grupo)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                permissaoModel.VerificaPermissaoUsuario(idUsuarioLogado, (int)Enumeration.Permissoes.PMS_Ver_Permissao);

                return StatusCode(200, permissaoModel.ListarPermissoesGrupo((int)grupo));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(List<PermissaoDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Lista todas as permissões de um usuario", Description = "Lista todas as permissões pertencentes aos grupos que o usuário está vinculado.")]
        [HttpGet("ListarPermissoesUsuario")]
        public ActionResult ListarPermissoesUsuario(int idUsuario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                permissaoModel.VerificaPermissaoUsuario(idUsuarioLogado, (int)Enumeration.Permissoes.PMS_Ver_Permissao);

                return StatusCode(200, permissaoModel.ListarPermissoesUsuario(idUsuario));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Vincula uma permissão a um grupo", Description = "Vincula uma permissão a um grupo.")]
        [HttpPost("VincularPermissaoGrupo")]
        public ActionResult VincularPermissaoGrupo(Enumeration.Grupo grupo, Enumeration.Permissoes permissao)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                permissaoModel.VerificaPermissaoUsuario(idUsuarioLogado, (int)Enumeration.Permissoes.PMS_Ver_Permissao);
                permissaoModel.VincularPermissaoGrupo((int)grupo, (int)permissao);

                return StatusCode(200, new { Message = "Permissão vinculada com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Desvincula uma permissão de um grupo", Description = "Desvincula uma permissão de um grupo.")]
        [HttpPost("DesvincularPermissaoGrupo")]
        public ActionResult DesvincularPermissaoGrupo(Enumeration.Grupo grupo, Enumeration.Permissoes permissao)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                permissaoModel.VerificaPermissaoUsuario(idUsuarioLogado, (int)Enumeration.Permissoes.PMS_Ver_Permissao);
                permissaoModel.DesvincularPermissaoGrupo((int)grupo, (int)permissao);

                return StatusCode(200, new { Message = "Permissão desvinculada com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }
    }
}
