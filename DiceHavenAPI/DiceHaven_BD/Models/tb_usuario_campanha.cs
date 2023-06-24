using System;
using System.Collections.Generic;

namespace DiceHaven_BD.Models;

public partial class tb_usuario_campanha
{
    public int ID_USUARIO_CAMPANHA { get; set; }

    public int ID_CAMPANHA { get; set; }

    public int ID_USUARIO { get; set; }

    public bool FL_ADMIN { get; set; }

    public bool FL_MUTADO { get; set; }

    public DateTime DT_ENTRADA { get; set; }

    public virtual tb_campanha ID_CAMPANHANavigation { get; set; }

    public virtual tb_usuario ID_USUARIONavigation { get; set; }
}
