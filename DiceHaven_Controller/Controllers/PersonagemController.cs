using DiceHaven_BD.Contexts;
using DiceHaven_DTO.Ficha;
using DiceHaven_Model.Models.ControlleDeAcesso;
using DiceHaven_Model.Models.Ficha;
using DiceHaven_Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DiceHaven_Controller.Controllers.Ficha
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class PersonagemController : ControllerBase
    {
        private DiceHavenBDContext dbDiceHaven;
        public PersonagemController(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        [HttpGet("ListarPersonagens")]
        public ActionResult ListarPersonagens(int? idUsuario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                permissaoModel.VerificaPermissaoUsuario(idUsuarioLogado, (int)Enumeration.Permissoes.PMS_Adm_Fichas);
                Personagem personagemModel = new Personagem(dbDiceHaven);
                List<PersonagemDTO> listaPersonagem;
                if (idUsuario == 0 || idUsuario is null)
                    listaPersonagem = personagemModel.ListarPersonagem(idUsuarioLogado);
                else
                    listaPersonagem = personagemModel.ListarPersonagem(idUsuario ?? 0);

                return StatusCode(200, listaPersonagem);

            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [HttpPost("CadastrarPersonagem")]
        public ActionResult CadastrarPersonagem(PersonagemDTO novoPersonagem)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                permissaoModel.VerificaPermissaoUsuario(idUsuarioLogado, (int)Enumeration.Permissoes.PMS_Adm_Fichas);
                Personagem personagemModel = new Personagem(dbDiceHaven);

                novoPersonagem.ID_USUARIO = idUsuarioLogado;
                personagemModel.CadastrarPersonagem(novoPersonagem);

                return StatusCode(200, new { Message = "Personagem Cadastrado com sucesso!" });
            }
            catch(HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [HttpPut("EditarPersonagem")]
        public ActionResult EditarPersonagem(PersonagemDTO novoPersonagem)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                permissaoModel.VerificaPermissaoUsuario(idUsuarioLogado, (int)Enumeration.Permissoes.PMS_Adm_Fichas);
                Personagem personagemModel = new Personagem(dbDiceHaven);
                personagemModel.EditarPersonagem(novoPersonagem);

                return StatusCode(200, new { Message = "Personagem editado com sucesso!" });
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

    }
}
