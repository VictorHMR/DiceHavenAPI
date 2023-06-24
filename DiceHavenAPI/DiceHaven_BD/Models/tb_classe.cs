using System;
using System.Collections.Generic;

namespace DiceHaven_BD.Models;

public partial class tb_classe
{
    public int ID_CLASSE { get; set; }

    public string DS_CLASSE { get; set; }

    public string DS_DESCRICAO { get; set; }

    public byte[] DS_FOTO { get; set; }

    public int? NR_STR_PADRAO { get; set; }

    public int? NR_DEX_PADRAO { get; set; }

    public int? NR_CON_PADRAO { get; set; }

    public int? NR_INT_PADRAO { get; set; }

    public int? NR_WIS_PADRAO { get; set; }

    public int? NR_CHA_PADRAO { get; set; }

    public int ID_CAMPANHA { get; set; }

    public virtual tb_campanha ID_CAMPANHANavigation { get; set; }

    public virtual ICollection<tb_ficha> tb_fichas { get; set; } = new List<tb_ficha>();
}
