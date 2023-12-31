using DiceHaven_BD.Contexts;
using DiceHaven_DTO;
using DiceHaven_Model.Interfaces;
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
        private IUsuario _usuario;

        public UsuarioController(DiceHavenBDContext dbDiceHaven, IConfiguration config, IUsuario usuario)
        {
            this.dbDiceHaven = dbDiceHaven;
            this._configuration = config;
            this._usuario = usuario;
        }

        [ProducesResponseType(typeof(AuthTokenDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Faz login no sistema", Description = "Verifica as credenciais, se estiver tudo certo, retorna um Bearer token e sua dura��o.")]
        [HttpGet("Login")]
        public ActionResult Login(string login, string senha)
        {
            try
            {
                UsuarioDTO usuario = _usuario.Login(login, senha);

                return StatusCode(200, _usuario.GerarToken(usuario));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }

        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Cadastra um usu�rio no sistema", Description = "Cadastra um usu�rio no sistema.")]
        [HttpPost("cadastrarUsuario")]
        public ActionResult cadastrarUsuario(UsuarioDTO novoUsuario)
        {
            try
            {
                _usuario.cadastrarUsuario(novoUsuario);
                return StatusCode(200, new { Message = "Usu�rio cadastrado com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }

        }

        [ProducesResponseType(typeof(UsuarioDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Busca um usuario", Description = "Busca um usuario pelo ID_USUARIO.")]
        [Authorize]
        [HttpGet("obterUsuario")]
        public ActionResult obterUsuario(int idUsuario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                if(idUsuario != 0)
                    return StatusCode(200, _usuario.obterUsuario(idUsuario));
                else
                    return StatusCode(200, _usuario.obterUsuario(idUsuarioLogado));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }

        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Edita um usuario", Description = "Edita os dados de um usuario.")]
        [HttpPut("alterarDadosUsuario")]
        public ActionResult alterarDadosUsuario(UsuarioDTO Usuario)
        {
            try
            {
                _usuario.alterarDadosUsuario(Usuario);
                return StatusCode(200, new { Message = "Usu�rio atualizado com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }

        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Edita as configura��es um usuario", Description = "Edita as configura��es um usuario.")]
        [HttpPut("alterarConfigUsuario")]
        public ActionResult alterarConfigUsuario(ConfigUsuarioDTO configsUsuario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuario = int.Parse(claim[0].Value);
                _usuario.alterarConfigUsuario(configsUsuario, idUsuario);
                return StatusCode(200, new { Message = "Configura��es alteradas com sucesso" });

            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }

        }


    }
}