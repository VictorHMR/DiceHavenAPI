using DiceHaven_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_Model.Interfaces
{
    public interface ICampoFicha
    {
        List<CampoFichaDTO> ListarCamposFicha(int idCampanha);
        CampoFichaDTO ObterCampoFicha(int idCampoFicha);
        int CadastrarCampoFicha(CampoFichaDTO novoCampo);
        void EditarCampoFicha(CampoFichaDTO novoCampo);
        bool DeletarCampoFicha(int idCampoFicha);
    }
}
