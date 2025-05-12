using DiceHavenAPI.DTOs;

namespace DiceHaven_API.DTOs.Response
{
    public class FichaDTO
    {
        public int ID_CAMPANHA { get; set; }
        public PersonagemDTO PERSONAGEM { get; set; }
        public List<SecaoDTO> LST_SECAO_FICHA { get; set; }
    }

    public class DadoFichaDTO
    {
        public int? ID_DADO_FICHA { get; set; }
        public CampoFichaDTO CAMPO_FICHA { get; set; }
        public string DS_VALOR { get; set; }
        public int? DS_VALOR_MODIFICADOR { get; set; }
    }
    public class SecaoDTO
    {
        public int ID_SECAO_FICHA { get; set; }
        public string DS_NOME_SECAO { get; set; }
        public int NR_ORDEM { get; set; }
        public List<DadoFichaDTO> LST_DADOS_FICHA { get; set; }
    }
}
