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

    public string DS_XP_SUBIR_LVL { get; set; }

    public DateTime DT_CRIACAO { get; set; }

    public bool FL_ATIVO { get; set; }

    public bool FL_PUBLICA { get; set; }

    public int ID_USUARIO_CRIADOR { get; set; }

    public int ID_MESTRE_CAMPANHA { get; set; }

    public virtual tb_usuario ID_MESTRE_CAMPANHANavigation { get; set; }

    public virtual tb_usuario ID_USUARIO_CRIADORNavigation { get; set; }

    public virtual ICollection<tb_classe> tb_classes { get; set; } = new List<tb_classe>();

    public virtual ICollection<tb_ficha> tb_fichas { get; set; } = new List<tb_ficha>();

    public virtual ICollection<tb_raca> tb_racas { get; set; } = new List<tb_raca>();

    public virtual ICollection<tb_usuario_campanha> tb_usuario_campanhas { get; set; } = new List<tb_usuario_campanha>();
}
