using System;
using System.Collections.Generic;

namespace DiceHavenAPI.Models;

public partial class tb_config_usuario
{
    public int ID_CONFIG_USUARIO { get; set; }

    public bool FL_DARK_MODE { get; set; }

    public virtual tb_usuario ID_CONFIG_USUARIONavigation { get; set; }
}
