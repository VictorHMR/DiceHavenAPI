using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_DTO.Ficha
{
    public class PersonagemDTO
    {
        public int? ID_PERSONAGEM { get; set; }
        public string DS_NOME { get; set; }
        public string DS_BACKSTORY { get; set; }
        public string DS_FOTO { get; set; }
        public int NR_IDADE { get; set; }
        public string DS_GENERO { get; set; }
        public string DS_CAMPO_LIVRE { get; set; }
        public int ID_USUARIO { get; set; }
    }
}
