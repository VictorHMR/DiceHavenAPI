using System;
using System.Collections.Generic;

namespace DiceHaven_BD.Models;

public partial class TB_PERMISSAO
{
    public int ID_PERMISSAO { get; set; }

    public string DS_PERMISSAO { get; set; }

    public string DS_DESCRICAO { get; set; }

    public virtual ICollection<TB_GRUPO_PERMISSAO> TB_GRUPO_PERMISSAOs { get; set; } = new List<TB_GRUPO_PERMISSAO>();
}
