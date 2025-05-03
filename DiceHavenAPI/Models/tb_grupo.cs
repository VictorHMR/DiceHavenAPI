using System;
using System.Collections.Generic;

namespace DiceHavenAPI.Models;

public partial class tb_grupo
{
    public int ID_GRUPO { get; set; }

    public string DS_GRUPO { get; set; }

    public string DS_DESCRICAO { get; set; }

    public bool FL_ADMIN { get; set; }
}
