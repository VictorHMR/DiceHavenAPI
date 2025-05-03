using System;
using System.Collections.Generic;

namespace DiceHavenAPI.Models;

public partial class tb_chat
{
    public int ID_CHAT { get; set; }

    public int ID_USUARIO_1 { get; set; }

    public int ID_USUARIO_2 { get; set; }

    public bool FL_ATIVO_USR_1 { get; set; }

    public bool FL_ATIVO_USR_2 { get; set; }

    public virtual tb_usuario ID_USUARIO_1Navigation { get; set; }

    public virtual tb_usuario ID_USUARIO_2Navigation { get; set; }

    public virtual ICollection<tb_chat_mensagem> tb_chat_mensagems { get; set; } = new List<tb_chat_mensagem>();
}
