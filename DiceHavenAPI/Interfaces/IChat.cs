using DiceHaven_API.DTOs;
using DiceHavenAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHavenAPI.Interfaces
{
    public interface IChat
    {
        int EnviarMensagemCampanha(MensagemCampanhaDTO novaMensagem);
        List<MensagemCampanhaDTO> ListarMensagensCampanha(int idCampanha);
    }
}
