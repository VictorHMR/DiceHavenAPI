using System;
using System.Collections.Generic;

namespace DiceHavenAPI.Models;

public partial class tb_grupo_usuario
{
    public int ID_GRUPO_USUARIO { get; set; }

    public int ID_GRUPO { get; set; }

    public int ID_USUARIO { get; set; }
}
