using System;
using System.Collections.Generic;

namespace DiceHaven_BD.Models;

public partial class tb_grupo_usuario
{
    public int ID_GRUPO_USUARIO { get; set; }

    public int ID_GRUPO { get; set; }

    public int ID_USUARIO { get; set; }
}
