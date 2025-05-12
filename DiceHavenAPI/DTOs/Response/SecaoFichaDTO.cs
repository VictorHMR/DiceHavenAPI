using DiceHavenAPI.DTOs;

namespace DiceHaven_API.DTOs.Response
{
    public class SecaoFichaDTO
    {
        public int? ID_SECAO_FICHA { get; set; }
        public string DS_NOME_SECAO { get; set; }
        public int NR_ORDEM { get; set; }
        public int ID_CAMPANHA { get; set; }

        public List<CampoFichaDTO> CAMPOS { get; set; } = new List<CampoFichaDTO>();

        public bool FL_DELETE { get; set; } = false;
    }
}
