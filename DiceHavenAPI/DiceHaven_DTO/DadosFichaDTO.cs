using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_DTO
{
    public class DadosFichaDTO
    {
        public int? ID_DADOS_FICHA { get; set; }
        public int ID_CAMPO_FICHA { get; set; }
        public int ID_PERSONAGEM { get; set; }
        public string DS_VALOR { get; set; }

    }
}
