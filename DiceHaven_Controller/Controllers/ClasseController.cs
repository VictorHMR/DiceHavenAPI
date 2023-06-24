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
    public class ClasseController : ControllerBase
    {
        private DiceHavenBDContext dbDiceHaven;
        public ClasseController(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        [ProducesResponseType(typeof(List<ClasseDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Listar Classes", Description = "Lista todas as classes baseado em um ID_CAMPANHA.")]
        [HttpGet("ListarClasses")]
        public ActionResult ListarClasses(int idCampanha)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                Classe classeModel = new Classe(dbDiceHaven);

                return StatusCode(200, classeModel.ListarClasses(idCampanha));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(ClasseDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Buscar classe", Description = "Busca uma classe baseado no ID_CLASSE.")]
        [HttpGet("ObterClasse")]
        public ActionResult ObterClasse(int idClasse)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                Classe classeModel = new Classe(dbDiceHaven);

                return StatusCode(200, classeModel.ObterClasse(idClasse));
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Adicionar classe", Description = "Adiciona uma classe a campanha.")]
        [HttpPost("CadatrarClasse")]
        public ActionResult CadatrarClasse(ClasseDTO novaClasse)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                Classe classeModel = new Classe(dbDiceHaven);
                classeModel.CadastrarClasse(novaClasse, idUsuarioLogado);

                return StatusCode(200, new {Message=$"Classe: {novaClasse.DS_CLASSE} Cadastrada com sucesso!"});
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Atualizar classe", Description = "Atualiza uma classe.")]
        [HttpPut("EditarClasse")]
        public ActionResult EditarClasse(ClasseDTO classe)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                Classe classeModel = new Classe(dbDiceHaven);
                classeModel.EditarClasse(classe, idUsuarioLogado);

                return StatusCode(200, new { Message = $"Classe editada com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Deletar classe", Description = "Deleta uma classe.")]
        [HttpDelete("DeletarClasse")]
        public ActionResult DeletarClasse(int idClasse)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                Classe classeModel = new Classe(dbDiceHaven);
                classeModel.DeletarClasse(idClasse, idUsuarioLogado);

                return StatusCode(200, new { Message = $"Classe deletada com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

    }
}
