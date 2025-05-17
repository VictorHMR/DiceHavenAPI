using DiceHavenAPI.Models;

namespace DiceHaven_API.Models;
public partial class tb_campanha_mensagem
{
    public int ID_CAMPANHA_MENSAGEM { get; set; }
    public string DS_MENSAGEM { get; set; }
    public bool FL_MESTRE { get; set; }
    public DateTime DT_MENSAGEM { get; set; }
    public int ID_USUARIO { get; set; }
    public int ID_CAMPANHA { get; set; }
    public int? ID_PERSONAGEM { get; set; }

    public virtual tb_personagem ID_PERSONAGEMNavigation { get; set; }
    public virtual tb_usuario ID_USUARIONavigation { get; set; }
    public virtual tb_campanha ID_CAMPANHANavigation { get; set; }
}
