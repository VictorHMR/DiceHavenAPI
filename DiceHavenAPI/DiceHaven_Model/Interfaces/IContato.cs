using DiceHaven_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_Model.Interfaces
{
    public interface IContato
    {
        void AdicionarContato(int idUsuario, int idUsuarioLogado);
        void RemoverContato(int idUsuario, int idUsuarioLogado);
        List<ContatoDTO> ListarContatos(int idUsuarioLogado);
        void MuteDesmuteContato(int idUsuarioContato, bool flMute);
    }
}
