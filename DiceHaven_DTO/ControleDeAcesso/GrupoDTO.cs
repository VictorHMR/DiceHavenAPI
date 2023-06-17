using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_DTO.ControleDeAcesso
{
    public class GrupoDTO
    {
        public int ID_GRUPO { get; set; }
        public string DS_GRUPO { get; set; }
        public string DS_DESCRICAO { get; set; }
        public bool FL_ADMIN { get; set; }
    }
}
