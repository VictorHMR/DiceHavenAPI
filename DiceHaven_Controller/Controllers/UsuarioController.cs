using DiceHaven_BD.Contexts;
using DiceHaven_DTO;
using DiceHaven_Model.Models;
using DiceHaven_Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace DiceHaven_Controller.Controllers
{
    [ApiExplorerSettings(GroupName = "Geral")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private DiceHavenBDContext dbDiceHaven;
        private readonly IConfiguration _configuration;

        public UsuarioController(DiceHavenBDContext dbDiceHaven, IConfiguration config)
        {
            this.dbDiceHaven = dbDiceHaven;
            this._configuration = config;
        }

        [SwaggerOperation(Summary = "Faz Login", Description = "Autentica usuário")]
        [HttpGet("Login")]
        public ActionResult Login(string login, string senha)
        {
            try
            {
                Usuario usuarioModel = new Usuario(dbDiceHaven, _configuration);
                UsuarioDTO usuario = usuarioModel.Login(login, senha);

                return StatusCode(200, usuarioModel.GerarToken(usuario));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }

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

        [HttpPut("alterarDadosUsuario")]
        public ActionResult alterarDadosUsuario(UsuarioDTO Usuario)
        {
            try
            {
                Usuario usuarioModel = new Usuario(dbDiceHaven);
                usuarioModel.alterarDadosUsuario(Usuario);
                return StatusCode(200, new { Message = "Usuário atualizado com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
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