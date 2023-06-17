using DiceHaven_BD.Contexts;
using DiceHaven_DTO.ControleDeAcesso;
using DiceHaven_Model.Models.ControlleDeAcesso;
using DiceHaven_Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DiceHaven_Controller.Controllers.ControleDeAcesso
{
    [ApiExplorerSettings(GroupName = "Geral")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private DiceHavenBDContext dbDiceHaven;

        public UsuarioController(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        [HttpPost("cadastrarUsuario")]
        public ActionResult cadastrarUsuario(UsuarioDTO novoUsuario)
        {
            try
            {
                Usuario usuarioModel = new Usuario(dbDiceHaven);
                usuarioModel.cadastrarUsuario(novoUsuario);
                return StatusCode(200, new { Message = "Usuário cadastrado com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }

        }

        [HttpGet("obterUsuario")]
        public ActionResult obterUsuario(int idUsuario)
        {
            try
            {
                Usuario usuarioModel = new Usuario(dbDiceHaven);
                return StatusCode(200, usuarioModel.obterUsuario(idUsuario));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }

        }

        [HttpPut("alterarConfigUsuario")]
        public ActionResult alterarConfigUsuario(ConfigUsuarioDTO configsUsuario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuario = int.Parse(claim[0].Value);
                Usuario userModel = new Usuario(dbDiceHaven);
                userModel.alterarConfigUsuario(configsUsuario, idUsuario);
                return StatusCode(200, new { Message = "Configurações alteradas com sucesso" });

            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }

        }


    }
}