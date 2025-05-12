using DiceHavenAPI.Models;

namespace DiceHaven_API.Models;

public partial class tb_personagem_campanha
{
    public int ID_PERSONAGEM_CAMPANHA { get; set; }
    public int ID_PERSONAGEM { get; set; }
    public int ID_CAMPANHA { get; set; }
    public DateTime DT_REGISTRO { get; set; }
    public virtual tb_personagem ID_PERSONAGEMNavigation { get; set; }
    public virtual tb_campanha ID_CAMPANHANavigation { get; set; }
}
