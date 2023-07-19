using DiceHaven_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_Model.Interfaces
{
    public interface ICampanha
    {
        CampanhaDTO ObterCampanha(int idCampanha);
        List<CampanhaDTO> ListarCampanhas(int idUsuario = 0);
        int CadastrarCampanha(CampanhaDTO novaCampanha, int idUsuarioLogado);
        void AtualizarCampanha(CampanhaDTO campanhaAtualizada);
        void VincularUsuarioCampanha(int idCampanha, int idUsuario, bool flAdmin = false);
        void DesvincularUsuarioCampanha(int idCampanha, int idUsuario);
        void AlterarAdmins(int idUsuario, int idCampanha, int idUsuarioLogado, bool flAdmin = false);
    }
}
