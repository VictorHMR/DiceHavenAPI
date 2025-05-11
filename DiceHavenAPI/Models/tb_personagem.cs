using System;
using System.Collections.Generic;

namespace DiceHavenAPI.Models;

public partial class tb_personagem
{
    public int ID_PERSONAGEM { get; set; }
    public string DS_NOME { get; set; }
    public string DS_FOTO { get; set; }
    public int ID_USUARIO { get; set; }

    public virtual tb_usuario ID_USUARIONavigation { get; set; }

    public virtual ICollection<tb_dados_ficha> tb_dados_fichas { get; set; } = new List<tb_dados_ficha>();
}
