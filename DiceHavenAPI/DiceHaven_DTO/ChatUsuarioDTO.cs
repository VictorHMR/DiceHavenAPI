using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_DTO
{
    public class ChatUsuarioDTO
    {
        public int IdChat { get;set; }
        public UsuarioDTO Usuario { get; set; }
        public MensagemDTO UltimaMensagem { get; set; }
    }
}
