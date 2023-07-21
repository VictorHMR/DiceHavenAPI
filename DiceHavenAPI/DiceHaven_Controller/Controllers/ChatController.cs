using DiceHaven_BD.Contexts;
using DiceHaven_DTO;
using DiceHaven_Model.Interfaces;
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
    public class ChatController : ControllerBase
    {
        private DiceHavenBDContext dbDiceHaven;
        private IChat _chat;

        public ChatController(DiceHavenBDContext dbDiceHaven, IChat chat)
        {
            this.dbDiceHaven = dbDiceHaven;
            this._chat = chat;
        }

        [ProducesResponseType(typeof(List<UsuarioDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Listar chats", Description = "Lista todos os usuários que possuem chat com o usuário logado")]
        [HttpGet("ListarChatsUsuario")]
        public ActionResult ListarChatsUsuario()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                

                return StatusCode(200, _chat.ListarChatsUsuario(idUsuarioLogado));

            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Inicia um chat com outro usuário", Description = "Inicia um chat com outro usuário")]
        [HttpPost("IniciarChat")]
        public ActionResult IniciarChat(int idUsuario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                _chat.IniciarChat(idUsuarioLogado, idUsuario);

                return StatusCode(200, new { Message = "Chat iniciado com sucesso!" });

            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Finaliza um chat com outro usuário", Description = "Finaliza um chat com outro usuário")]
        [HttpPost("RemoverChat")]
        public ActionResult RemoverChat(int idUsuario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                _chat.RemoverChat(idUsuarioLogado, idUsuario);

                return StatusCode(200, new { Message = "Chat removido com sucesso!" });

            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(List<MensagemDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Listagem de mensagens", Description = "Lista todas as mensagens de um chat.")]
        [HttpGet("ListarMensagens")]
        public ActionResult ListarMensagens(int idChat)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                

                return StatusCode(200, _chat.ListarMensagensChat(idChat, idUsuarioLogado));

            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Enviar mensagem", Description = "Envia uma mensagem para o chat com outro usuário.")]
        [HttpPost("EnviarMensagem")]
        public ActionResult EnviarMensagem(MensagemDTO novaMensagem)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                _chat.EnviarMensagem(novaMensagem, idUsuarioLogado);

                return StatusCode(200, new { Message = "Mensagem enviada." });

            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Edita uma mensagem", Description = "Editar uma mensagem de um chat com outro usuário.")]
        [HttpPut("EditarMensagem")]
        public ActionResult EditarMensagem(MensagemDTO novaMensagem)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                _chat.EditarMensagem(novaMensagem, idUsuarioLogado);

                return StatusCode(200, new { Message = "Mensagem editada." });

            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Desativa uma mensagem", Description = "Desativa uma mensagem de um chat com outro usuário.")]
        [HttpPut("DesativarMensagem")]
        public ActionResult DesativarMensagem(int idChatMensagem)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                _chat.DesativarMensagem(idChatMensagem, idUsuarioLogado);

                return StatusCode(200, new { Message = "Mensagem desativada." });

            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }
    }
}
