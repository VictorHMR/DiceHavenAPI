using DiceHaven_API.DTOs.Request;
using DiceHaven_API.DTOs.Response;
using DiceHavenAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHavenAPI.Interfaces
{
    public interface ICampanha
    {
        CampanhaDTO ObterCampanha(int idCampanha);
        List<CampanhaDTO> ListarCampanhas(int idUsuario = 0);
        int CadastrarCampanha(CampanhaDTO novaCampanha, int idUsuarioLogado);
        void AtualizarCampanha(CampanhaDTO campanhaAtualizada);
        void VincularUsuarioCampanha(int idCampanha, int idUsuario, bool flAdmin = false);
        void DesvincularUsuarioCampanha(int idCampanha, int idUsuario);
        void AlterarAdmins(GerenciarAdminDTO gerenciarAdmin,  int idUsuarioLogado);
        List<UsuarioBasicoDTO> ListarUsuarios(int idUsuarioLogado, int? idCampanha);
        List<CampoFichaDTO> ListarCamposFicha(int idCampanha);
        void EditarModeloDeFicha(List<CampoFichaDTO> lstCampos);
    }
}
