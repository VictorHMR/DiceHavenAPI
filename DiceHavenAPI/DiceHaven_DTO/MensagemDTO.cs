using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_DTO
{
    public class MensagemDTO
    {
        public int ID_CHAT_MENSAGEM { get; set; }
        public string DS_MENSAGEM { get; set; }
        public DateTime DT_DATA_ENVIO { get; set; }
        public bool FL_EDITADA { get; set; }
        public string DS_LINK_IMAGEM { get; set; }
        public int ID_USUARIO { get; set; }
        public bool FL_ATIVA { get; set; }
        public bool FL_VISUALIZADA { get; set; }
        public int ID_CHAT { get; set; }
    }
}
