using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHavenAPI.DTOs
{
    public class UsuarioDTO
    {
        public int ID_USUARIO { get; set; }
        public string DS_NOME { get; set; }
        public DateTime DT_NASCIMENTO { get; set; }
        public string DS_LOGIN { get; set; }
        public string DS_SENHA { get; set; }
        public string DS_EMAIL { get; set; }
        public bool FL_ATIVO { get; set; } = true;
        public DateTime? DT_ULTIMO_ACESSO { get; set; }
        public string DS_FOTO { get; set; } 

    }
}
