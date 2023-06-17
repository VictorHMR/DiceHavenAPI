using System;
using System.Collections.Generic;

namespace DiceHaven_BD.Models;

public partial class TB_CONFIG_USUARIO
{
    public int ID_CONFIG_USUARIO { get; set; }

    public bool FL_DARK_MODE { get; set; }

    public virtual TB_USUARIO ID_CONFIG_USUARIONavigation { get; set; }
}
