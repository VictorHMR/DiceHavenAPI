using DiceHaven_BD.Contexts;
using DiceHaven_DTO.ControleDeAcesso;
using DiceHaven_Model.Models.ControlleDeAcesso;
using DiceHaven_Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace DiceHaven_Controller.Controllers.ControleDeAcesso
{
    [ApiExplorerSettings(GroupName = "Geral")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private DiceHavenBDContext dbDiceHaven;
        private readonly IConfiguration _configuration;

        public AuthenticateController(DiceHavenBDContext dbDiceHaven, IConfiguration config)
        {
            this.dbDiceHaven = dbDiceHaven;
            _configuration = config;
        }

        [SwaggerOperation(Summary = "Faz Login",
            Description = "Autentica usuário")]
        [HttpGet("Login")]
        public ActionResult Login(string login, string senha)
        {
            try
            {
                Authenticate authModel = new Authenticate(dbDiceHaven, _configuration);
                Usuario usuarioModel = new Usuario(dbDiceHaven);
                UsuarioDTO usuario = authModel.Login(login, senha);

                return StatusCode(200, authModel.GerarToken(usuario));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }

        }

    }
}