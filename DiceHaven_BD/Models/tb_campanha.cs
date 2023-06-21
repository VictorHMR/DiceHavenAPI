using System;
using System.Collections.Generic;

namespace DiceHaven_BD.Models;

public partial class tb_campanha
{
    public int ID_CAMPANHA { get; set; }

    public string DS_NOME_CAMPANHA { get; set; }

    public string DS_LORE { get; set; }

    public string DS_PERIODO { get; set; }

    public bool FL_EXISTE_MAGIA { get; set; }

    public int NR_DEFINICAO_ATRIBUTOS { get; set; }

    public DateTime DT_CRIACAO { get; set; }

    public bool FL_ATIVO { get; set; }

    public int ID_USUARIO_CRIADOR { get; set; }

    public int ID_MESTRE_CAMPANHA { get; set; }

    public virtual tb_usuario ID_MESTRE_CAMPANHANavigation { get; set; }

    public virtual tb_usuario ID_USUARIO_CRIADORNavigation { get; set; }
}
