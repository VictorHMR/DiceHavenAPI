using DiceHaven_API.DTOs.Response;
using DiceHavenAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHavenAPI.Interfaces
{
    public interface IPersonagem
    {
        PersonagemDTO ObterPersonagem(int idPersonagem);
        List<PersonagemDTO> ListarPersonagem(int idUsuario);
        int CadastrarPersonagem(PersonagemDTO novoPersonagem);
        void EditarPersonagem(PersonagemDTO personagemInfo);
    }
}
