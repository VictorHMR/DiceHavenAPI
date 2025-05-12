using DiceHaven_API.DTOs.Request;
using DiceHaven_API.DTOs.Response;
using DiceHavenAPI.Contexts;
using DiceHavenAPI.DTOs;
using DiceHavenAPI.Interfaces;
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
    public class CampanhaController : ControllerBase
    {
        private ICampanha _campanha;

        public CampanhaController(ICampanha campanha)
        {
            this._campanha = campanha;
        }

        [ProducesResponseType(typeof(CampanhaDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Busca uma campanha", Description = "Busca uma campanha baseado no ID_CAMPANHA")]
        [HttpGet("obterCampanha")]
        public ActionResult obterCampanha(int idCampanha)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                return StatusCode(200, _campanha.ObterCampanha(idCampanha));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(List<CampanhaDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Lista campanhas", Description = "Lista todas as campanhas ligadas a um usuário")]
        [HttpGet("listarCampanhas")]
        public ActionResult listarCampanhas(int? idUsuario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                return StatusCode(200, _campanha.ListarCampanhas(idUsuario ?? idUsuarioLogado));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Cadastra uma nova campanha", Description = "Cadastra uma nova campanha com as informações passadas pelo usuário")]
        [HttpPost("cadastrarCampanha")]
        public ActionResult cadastrarCampanha([FromBody] CampanhaDTO novaCampanha)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                int idCampanha = _campanha.CadastrarCampanha(novaCampanha, idUsuarioLogado);
                return StatusCode(200, new { Message = "Campanha cadastrada com sucesso!", Id = idCampanha });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Atualiza uma campanha", Description = "Atualiza os dados de uma campanha com informações passadas pelo usuário")]
        [HttpPut("atualizarCampanha")]
        public ActionResult atualizarCampanha(CampanhaDTO campanhaAtualizada)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                _campanha.AtualizarCampanha(campanhaAtualizada);
                return StatusCode(200, new { Message = "Campanha atualizada com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Entrar em campanha",
            Description = "Faz o vinculo de um usuario a uma campanha, caso o usuario não seja informado, será o usuario logado.")]
        [HttpPost("vincularUsuarioCampanha")]
        public ActionResult vincularUsuarioCampanha([FromBody] VincularUsuarioCampanhaDTO vincularUsuarios)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                foreach (int usuario in vincularUsuarios.LST_USUARIOS)
                    _campanha.VincularUsuarioCampanha(vincularUsuarios.ID_CAMPANHA, usuario, usuario == idUsuarioLogado);
                return StatusCode(200, new { Message = "Usuário vinculado com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Sair de uma campanha",
            Description = "Desvincula um usuario de uma campanha, caso o usuario não seja informado, será o usuario logado.")]
        [HttpPost("desvincularUsuarioCampanha")]
        public ActionResult desvincularUsuarioCampanha([FromBody] DesvincularUsuarioCampanhaDTO desvincularUsuario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                _campanha.DesvincularUsuarioCampanha(desvincularUsuario.IdCampanha, desvincularUsuario.IdUsuario);
                return StatusCode(200, new { Message = "Usuário desvinculado com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Tornar usuário admin ou participante",
            Description = "Torna um usuário um administrador ou participante da campanha de acordo com a flag.")]
        [HttpPut("AlterarAdmins")]
        public ActionResult AlterarAdmins([FromBody] GerenciarAdminDTO gerenciarAdmin)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                _campanha.AlterarAdmins(gerenciarAdmin, idUsuarioLogado);
                return StatusCode(200, new { Message = "Membro atualizado com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }


        [ProducesResponseType(typeof(List<UsuarioBasicoDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Lista usuário pertencentes a campanha",
            Description = "Lista usuário pertencentes a campanha.")]
        [HttpGet("listarUsuarios")]
        public ActionResult ListarUsuarios(int? idCampanha)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                return StatusCode(200, _campanha.ListarUsuarios(idUsuarioLogado, idCampanha));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(List<PersonagemDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Lista personagens pertencentes a campanha",
            Description = "Lista personagens pertencentes a campanha.")]
        [HttpGet("ListarPersonagens")]
        public ActionResult ListarPersonagens(int idCampanha)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);

                return StatusCode(200, _campanha.ListarPersonagens(idCampanha));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }
    }
}
