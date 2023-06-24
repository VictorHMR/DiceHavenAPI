using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_DTO
{
    public class ClasseDTO: AtributosDTO
    {
        public int ID_CLASSE { get; set; }
        public string DS_CLASSE{ get; set; }
        public string DS_DESCRICAO { get; set; }
        public string DS_FOTO { get; set; }
        public int ID_CAMPANHA { get; set; }
    }
}
