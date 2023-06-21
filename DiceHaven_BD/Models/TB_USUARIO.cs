using System;
using System.Collections.Generic;

namespace DiceHaven_BD.Models;

public partial class tb_usuario
{
    public int ID_USUARIO { get; set; }

    public string DS_NOME { get; set; }

    public DateTime DT_NASCIMENTO { get; set; }

    public string DS_LOGIN { get; set; }

    public string DS_SENHA { get; set; }

    public string DS_EMAIL { get; set; }

    public bool FL_ATIVO { get; set; }

    public DateTime? DT_ULTIMO_ACESSO { get; set; }

    public virtual ICollection<tb_campanha> tb_campanhaID_MESTRE_CAMPANHANavigations { get; set; } = new List<tb_campanha>();

    public virtual ICollection<tb_campanha> tb_campanhaID_USUARIO_CRIADORNavigations { get; set; } = new List<tb_campanha>();

    public virtual tb_config_usuario tb_config_usuario { get; set; }

    public virtual ICollection<tb_personagem> tb_personagems { get; set; } = new List<tb_personagem>();
}
