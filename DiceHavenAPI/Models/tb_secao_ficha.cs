using DiceHavenAPI.Models;

namespace DiceHaven_API.Models
{
    public class tb_secao_ficha
    {
        public int ID_SECAO_FICHA { get; set; }
        public string DS_NOME_SECAO { get; set; }
        public int NR_ORDEM { get; set; }
        public int ID_CAMPANHA { get; set; }
        public virtual tb_campanha ID_CAMPANHANavigation { get; set; }
        public virtual ICollection<tb_campo_ficha> tb_campo_fichas { get; set; } = new List<tb_campo_ficha>();

    }
}
