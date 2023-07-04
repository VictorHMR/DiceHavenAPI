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

        public enum TipoCampoFicha
        {
            texto = 0,
            textarea = 1,
            numero = 2,
            flag = 3
        }
    }
}
