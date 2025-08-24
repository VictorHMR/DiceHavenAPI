using DiceHaven_API.DTOs.Response;
using DiceHavenAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHavenAPI.Interfaces
{
    public interface IUsuario
    {
        AuthTokenDTO GerarToken(UsuarioDTO usuario);
        UsuarioDTO Login(LoginDTO login);
        Task<int> cadastrarUsuario(UsuarioDTO request);
        Task alterarDadosUsuario(UsuarioDTO request);
        UsuarioDTO obterUsuario(int idUsuario);
        void alterarConfigUsuario(ConfigUsuarioDTO configsUsuario, int idUsuario);
    }
}
