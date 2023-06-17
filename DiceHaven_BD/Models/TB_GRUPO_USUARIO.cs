using System;
using System.Collections.Generic;

namespace DiceHaven_BD.Models;

public partial class TB_GRUPO_USUARIO
{
    public int ID_GRUPO_USUARIO { get; set; }

    public int ID_GRUPO { get; set; }

    public int ID_USUARIO { get; set; }

    public virtual TB_GRUPO ID_GRUPONavigation { get; set; }

    public virtual TB_USUARIO ID_USUARIONavigation { get; set; }
}
