using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DiceHaven_Utils.Enumeration;

namespace DiceHaven_DTO
{
    public class CampoFichaDTO
    {
        public int ID_CAMPO_FICHA { get; set; }
        public string DS_NOME_CAMPO { get; set; }
        public TipoCampoFicha TIPO_CAMPO { get; set; }
        public string DS_REFERENCIA { get; set; }
        public string DS_DESCRICAO { get; set; }
        public bool FL_TEM_MODIFICADOR { get; set; }
        public bool? FL_BLOQUEADO { get; set; }
        public bool? FL_VISIVEL { get; set; }
        public string DS_FORMULA_MODIFICADOR { get; set; }
        public string DS_VALOR_PADRAO { get; set; }
        public int NR_ORDEM { get; set; }
        public int ID_CAMPANHA { get; set; }
    }
}
