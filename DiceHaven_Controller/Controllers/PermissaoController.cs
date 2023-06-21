using DiceHaven_BD.Contexts;
using DiceHaven_Model.Models;
using DiceHaven_Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
