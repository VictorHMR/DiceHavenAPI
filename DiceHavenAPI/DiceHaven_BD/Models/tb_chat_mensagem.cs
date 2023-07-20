using System;
using System.Collections.Generic;

namespace DiceHaven_BD.Models;

public partial class tb_chat_mensagem
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

    public virtual tb_chat ID_CHATNavigation { get; set; }

    public virtual tb_usuario ID_USUARIONavigation { get; set; }
}
