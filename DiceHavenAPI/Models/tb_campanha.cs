﻿using DiceHaven_API.Models;
using System;
using System.Collections.Generic;

namespace DiceHavenAPI.Models;

public partial class tb_campanha
{
    public int ID_CAMPANHA { get; set; }

    public string DS_NOME_CAMPANHA { get; set; }

    public string DS_LORE { get; set; }

    public DateTime DT_CRIACAO { get; set; }

    public bool FL_ATIVO { get; set; }

    public bool FL_PUBLICA { get; set; }

    public string DS_FOTO { get; set; }

    public int ID_USUARIO_CRIADOR { get; set; }

    public int ID_MESTRE_CAMPANHA { get; set; }

    public virtual tb_usuario ID_MESTRE_CAMPANHANavigation { get; set; }

    public virtual tb_usuario ID_USUARIO_CRIADORNavigation { get; set; }

    public virtual ICollection<tb_secao_ficha> tb_secao_ficha { get; set; } = new List<tb_secao_ficha>();

    public virtual ICollection<tb_usuario_campanha> tb_usuario_campanhas { get; set; } = new List<tb_usuario_campanha>();

    public virtual ICollection<tb_personagem_campanha> tb_personagem_campanhas { get; set; } = new List<tb_personagem_campanha>();

    public virtual ICollection<tb_campanha_mensagem> tb_campanha_mensagens { get; set; } = new List<tb_campanha_mensagem>();

}
