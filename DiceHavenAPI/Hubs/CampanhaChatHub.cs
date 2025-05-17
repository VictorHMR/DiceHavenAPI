using DiceHaven_API.DTOs;
using DiceHavenAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace DiceHaven_API.Hubs
{
    [Authorize]
    public class CampanhaChatHub: Hub
    {
        private IChat chatService;
        ICampanha campanhaService;
        public CampanhaChatHub(IChat chatService, ICampanha campanhaService)
        {
            this.chatService = chatService;
            this.campanhaService = campanhaService;
        }
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var identity = httpContext?.User?.Identity as ClaimsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                var campanhasDoUsuario = campanhaService.ListarCampanhas(idUsuarioLogado);

                foreach (var campanha in campanhasDoUsuario)
                {
                    var groupName = $"CAMPANHA_{campanha.ID_CAMPANHA}";
                    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                }
            }

            await base.OnConnectedAsync();
        }


        public async Task SendMessageToGroup(MensagemCampanhaDTO NovaMensagem)
        {
            NovaMensagem.ID_CAMPANHA_MENSAGEM = chatService.EnviarMensagemCampanha(NovaMensagem);

            await Clients.Group("CAMPANHA_" + NovaMensagem.ID_CAMPANHA)
                .SendAsync("ReceiveMessage", NovaMensagem);
        }




    }
}
