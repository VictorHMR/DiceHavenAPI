using System;
using System.Collections.Generic;

namespace DiceHaven_BD.Models;

public partial class tb_dados_ficha
{
    public int ID_DADO_FICHA { get; set; }

    public int ID_CAMPO_FICHA { get; set; }

    public int ID_PERSONAGEM { get; set; }

    public string DS_VALOR { get; set; }

    public virtual tb_campo_ficha ID_CAMPO_FICHANavigation { get; set; }

    public virtual tb_personagem ID_PERSONAGEMNavigation { get; set; }
}
