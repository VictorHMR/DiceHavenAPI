using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_Utils
{
    public class Enumeration
    {

        public enum Grupo
        {
            Admin = 1,
            Moderador_1 = 2,
            Comum = 3
        }

        public enum Permissoes
        {
            PMS_Ver_Permissao = 1,
            PMS_Adm_Permissao = 2,
            PMS_Ver_Grupos = 3,
            PMS_Adm_Grupos = 4,
            PMS_Adm_Fichas = 5,
            PMS_Ver_Fichas_Privadas = 6
        }

        public enum TipoDefinicaoAtributos
        {
            Distribuicao = 1,
            BaseadoEmRaca = 2,
            BaseadoEmClasse = 3,
            BaseadoEmAmbos = 4

        }
    }
}
