using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHavenAPI.DTOs
{
    public class ContatoDTO
    {
        public int ID_USUARIO_CONTATO { get; set; }
        public int ID_USUARIO { get; set; }
        public UsuarioDTO CONTATO { get; set; }
        public bool FL_MUTADO { get; set; }

    }
}
