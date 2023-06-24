using System;
using System.Collections.Generic;

namespace DiceHaven_BD.Models;

public partial class tb_config_usuario
{
    public int ID_CONFIG_USUARIO { get; set; }

    public bool FL_DARK_MODE { get; set; }

    public virtual tb_usuario ID_CONFIG_USUARIONavigation { get; set; }
}
