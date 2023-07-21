using DiceHaven_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_Model.Interfaces
{
    public interface IChat
    {
        List<UsuarioDTO> ListarChatsUsuario(int idUsuarioLogado);
        void IniciarChat(int idUsuarioLogado, int idUsuario);
        void RemoverChat(int idUsuarioLogado, int idChat);
        List<MensagemDTO> ListarMensagensChat(int idChat, int idUsuarioLogado);
        void EnviarMensagem(MensagemDTO novaMensagem, int idUsuarioLogado);
        void EditarMensagem(MensagemDTO mensagemEditada, int idUsuarioLogado);
        void DesativarMensagem(int idChatMensagem, int idUsuarioLogado);
    }
}
