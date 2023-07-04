using System;
using System.Collections.Generic;

namespace DiceHaven_BD.Models;

public partial class tb_ficha
{
    public int ID_FICHA { get; set; }

    public int ID_CAMPO_FICHA { get; set; }

    public int ID_PERSONAGEM { get; set; }

    public int ID_USUARIO { get; set; }

    public string DS_VALOR { get; set; }

    public virtual tb_campo_ficha ID_CAMPO_FICHANavigation { get; set; }

    public virtual tb_personagem ID_PERSONAGEMNavigation { get; set; }

    public virtual tb_usuario ID_USUARIONavigation { get; set; }
}
