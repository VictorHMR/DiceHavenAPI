using System;
using System.Collections.Generic;

namespace DiceHaven_BD.Models;

public partial class TB_USUARIO
{
    public int ID_USUARIO { get; set; }

    public string DS_NOME { get; set; }

    public DateTime DT_NASCIMENTO { get; set; }

    public string DS_LOGIN { get; set; }

    public string DS_SENHA { get; set; }

    public string DS_EMAIL { get; set; }

    public bool FL_ATIVO { get; set; }

    public DateTime? DT_ULTIMO_ACESSO { get; set; }

    public int? NR_PERMISSAO { get; set; }

    public virtual TB_CONFIG_USUARIO TB_CONFIG_USUARIO { get; set; }

    public virtual ICollection<TB_GRUPO_USUARIO> TB_GRUPO_USUARIOs { get; set; } = new List<TB_GRUPO_USUARIO>();

    public virtual ICollection<TB_PERSONAGEM> TB_PERSONAGEMs { get; set; } = new List<TB_PERSONAGEM>();
}
