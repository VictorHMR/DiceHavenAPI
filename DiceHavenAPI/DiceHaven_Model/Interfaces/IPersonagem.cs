using DiceHaven_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_Model.Interfaces
{
    public interface IPersonagem
    {
        List<PersonagemDTO> ListarPersonagem(int idUsuario);
        void CadastrarPersonagem(PersonagemDTO novoPersonagem);
        void EditarPersonagem(PersonagemDTO personagemInfo);
    }
}
