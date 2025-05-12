using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DiceHavenAPI.Utils.Enumeration;

namespace DiceHavenAPI.DTOs
{
    public class CampoFichaDTO
    {
        public int? ID_CAMPO_FICHA { get; set; }
        public string DS_NOME_CAMPO { get; set; }
        public TipoCampoFicha TIPO_CAMPO { get; set; }
        public bool? FL_BLOQUEADO { get; set; }
        public bool? FL_VISIVEL { get; set; }
        public bool FL_MODIFICADOR { get; set; }
        public string DS_VALOR_PADRAO { get; set; }
        public int NR_ORDEM { get; set; }
        public int ID_SECAO_FICHA { get; set; }
        public bool FL_DELETE { get; set; }
    }
}
