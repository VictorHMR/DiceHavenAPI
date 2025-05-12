using DiceHaven_API.DTOs.Response;
using DiceHavenAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHavenAPI.Interfaces
{
    public interface IFicha
    {
        List<SecaoFichaDTO> ObterModeloDeFicha(int idCampanha);
        void EditarModeloDeFicha(List<SecaoFichaDTO> lstCampos);
        FichaDTO ListarDadosFicha(int idCampanha, int? idPersonagem);
    }
}
