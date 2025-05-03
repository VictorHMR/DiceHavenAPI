using System;
using System.Collections.Generic;

namespace DiceHavenAPI.Models;

public partial class tb_usuario_contato
{
    public int ID_USUARIO_CONTATO { get; set; }

    public int ID_USUARIO { get; set; }

    public int ID_CONTATO { get; set; }

    public bool FL_MUTADO { get; set; }

    public virtual tb_usuario ID_CONTATONavigation { get; set; }

    public virtual tb_usuario ID_USUARIONavigation { get; set; }
}
