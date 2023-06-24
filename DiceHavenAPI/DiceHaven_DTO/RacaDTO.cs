using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_DTO
{
    public class RacaDTO: AtributosDTO
    {
        public int ID_RACA { get; set; }
        public string DS_RACA { get; set; }
        public string DS_DESCRICAO { get; set; }
        public string DS_FOTO { get; set; }
        public int ID_CAMPANHA { get; set; }
    }
}
