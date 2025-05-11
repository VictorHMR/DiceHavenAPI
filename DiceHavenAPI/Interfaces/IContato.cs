using DiceHavenAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHavenAPI.Interfaces
{
    public interface IContato
    {
        void AdicionarContato(string username, int idUsuarioLogado);
        void RemoverContato(int idUsuario, int idUsuarioLogado);
        List<ContatoDTO> ListarContatos(int idUsuarioLogado);
        void MuteDesmuteContato(int idUsuarioContato, bool flMute);
    }
}
