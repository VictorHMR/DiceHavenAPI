using DiceHavenAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHavenAPI.Interfaces
{
    public interface IDadosFicha
    {
        List<DadosFichaDTO> ListarDadosFicha(int idCampanha, int idPersonagem);
        DadosFichaDTO ObterDadosFicha(int idCampoFicha, int idPersonagem);
        void GerarFichaPersonagem(int idPersonagem, int idCampanha);
        void AtualizarDadosFicha(DadosFichaDTO novosDados);
    }
}
