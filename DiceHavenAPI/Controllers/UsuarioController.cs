using DiceHaven_API.DTOs.Response;
using DiceHavenAPI.Contexts;
using DiceHavenAPI.DTOs;
using DiceHavenAPI.Interfaces;
using DiceHavenAPI.Services;
using DiceHavenAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace DiceHavenAPI.Controllers
{
    [ApiExplorerSettings(GroupName = "Geral")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private IUsuario _usuario;

        public UsuarioController(IUsuario usuario)
        {
            this._usuario = usuario;
        }

        [ProducesResponseType(typeof(AuthTokenDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Faz login no sistema", Description = "Verifica as credenciais, se estiver tudo certo, retorna um Bearer token e sua dura��o.")]
        [HttpPost("Login")]
        public ActionResult Login(LoginDTO login)
        {
            try
            {
                UsuarioDTO usuario = _usuario.Login(login);

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
        public ActionResult obterUsuario(int? idUsuario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                if(idUsuario is not null)
                    return StatusCode(200, _usuario.obterUsuario((int)idUsuario));
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
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                Usuario.ID_USUARIO = int.Parse(claim[0].Value);

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