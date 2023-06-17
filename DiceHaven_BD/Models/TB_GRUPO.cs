using System;
using System.Collections.Generic;

namespace DiceHaven_BD.Models;

public partial class TB_GRUPO
{
    public int ID_GRUPO { get; set; }

    public string DS_GRUPO { get; set; }

    public string DS_DESCRICAO { get; set; }

    public bool FL_ADMIN { get; set; }

    public virtual ICollection<TB_GRUPO_PERMISSAO> TB_GRUPO_PERMISSAOs { get; set; } = new List<TB_GRUPO_PERMISSAO>();

    public virtual ICollection<TB_GRUPO_USUARIO> TB_GRUPO_USUARIOs { get; set; } = new List<TB_GRUPO_USUARIO>();
}
