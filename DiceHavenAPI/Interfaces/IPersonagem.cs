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
        List<PersonagemDTO> ListarPersonagem(int idUsuario);
        void CadastrarPersonagem(PersonagemDTO novoPersonagem);
        void EditarPersonagem(PersonagemDTO personagemInfo);
    }
}
