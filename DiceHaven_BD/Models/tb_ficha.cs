using System;
using System.Collections.Generic;

namespace DiceHaven_BD.Models;

public partial class tb_ficha
{
    public int ID_FICHA { get; set; }

    public int NR_XP { get; set; }

    public int NR_PONTOS_VIDA { get; set; }

    public int NR_PONTOS_MANA { get; set; }

    public int NR_PONTOS_HAB { get; set; }

    public string DS_TENDENCIA { get; set; }

    public int? NR_STR { get; set; }

    public int? NR_DEX { get; set; }

    public int? NR_CON { get; set; }

    public int? NR_INT { get; set; }

    public int? NR_WIS { get; set; }

    public int? NR_CHA { get; set; }

    public int ID_RACA { get; set; }

    public int ID_CLASSE { get; set; }

    public int ID_PERSONAGEM { get; set; }

    public int ID_CAMPANHA { get; set; }

    public virtual tb_campanha ID_CAMPANHANavigation { get; set; }

    public virtual tb_classe ID_CLASSENavigation { get; set; }

    public virtual tb_personagem ID_PERSONAGEMNavigation { get; set; }

    public virtual tb_raca ID_RACANavigation { get; set; }
}
