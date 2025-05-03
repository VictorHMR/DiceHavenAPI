using System;
using System.Collections.Generic;

namespace DiceHavenAPI.Models;

public partial class tb_usuario
{
    public int ID_USUARIO { get; set; }

    public string DS_NOME { get; set; }

    public DateTime DT_NASCIMENTO { get; set; }

    public string DS_LOGIN { get; set; }

    public string DS_SENHA { get; set; }

    public string DS_EMAIL { get; set; }

    public bool FL_ATIVO { get; set; } = true;

    public DateTime? DT_ULTIMO_ACESSO { get; set; }

    public string DS_FOTO { get; set; }

    public virtual ICollection<tb_campanha> tb_campanhaID_MESTRE_CAMPANHANavigations { get; set; } = new List<tb_campanha>();

    public virtual ICollection<tb_campanha> tb_campanhaID_USUARIO_CRIADORNavigations { get; set; } = new List<tb_campanha>();

    public virtual ICollection<tb_chat> tb_chatID_USUARIO_1Navigations { get; set; } = new List<tb_chat>();

    public virtual ICollection<tb_chat> tb_chatID_USUARIO_2Navigations { get; set; } = new List<tb_chat>();

    public virtual ICollection<tb_chat_mensagem> tb_chat_mensagems { get; set; } = new List<tb_chat_mensagem>();

    public virtual tb_config_usuario tb_config_usuario { get; set; }

    public virtual ICollection<tb_personagem> tb_personagems { get; set; } = new List<tb_personagem>();

    public virtual ICollection<tb_usuario_campanha> tb_usuario_campanhas { get; set; } = new List<tb_usuario_campanha>();

    public virtual ICollection<tb_usuario_contato> tb_usuario_contatoID_CONTATONavigations { get; set; } = new List<tb_usuario_contato>();

    public virtual ICollection<tb_usuario_contato> tb_usuario_contatoID_USUARIONavigations { get; set; } = new List<tb_usuario_contato>();
}
