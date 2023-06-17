using System;
using System.Collections.Generic;

namespace DiceHaven_BD.Models;

public partial class TB_GRUPO_PERMISSAO
{
    public int ID_GRUPO_PERMISSAO { get; set; }

    public int ID_GRUPO { get; set; }

    public int ID_PERMISSAO { get; set; }

    public virtual TB_GRUPO ID_GRUPONavigation { get; set; }

    public virtual TB_PERMISSAO ID_PERMISSAONavigation { get; set; }
}
