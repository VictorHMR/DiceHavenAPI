using DiceHaven_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_Model.Interfaces
{
    public interface IUsuario
    {
        AuthTokenDTO GerarToken(UsuarioDTO usuario);
        UsuarioDTO Login(string login, string password);
        int cadastrarUsuario(UsuarioDTO request);
        void alterarDadosUsuario(UsuarioDTO request);
        UsuarioDTO obterUsuario(int idUsuario);
        void alterarConfigUsuario(ConfigUsuarioDTO configsUsuario, int idUsuario);
    }
}
