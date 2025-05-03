using System;
using System.Collections.Generic;

namespace DiceHavenAPI.Models;

public partial class tb_grupo_permissao
{
    public int ID_GRUPO_PERMISSAO { get; set; }

    public int ID_GRUPO { get; set; }

    public int ID_PERMISSAO { get; set; }
}
