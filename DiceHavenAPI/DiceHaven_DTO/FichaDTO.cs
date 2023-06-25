using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_DTO
{
    public class FichaDTO
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PersonagemDTO PERSONAGEM { get; set; }
        public int ID_FICHA { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int ID_PERSONAGEM { get; set; }
        public string DS_TENDENCIA { get; set; }
        public string DS_SOBRE { get; set; }
        public int NR_XP { get; set; }
        public int NR_LVL { get; set; }
        public int NR_MAX_PV { get; set; }
        public int NR_MAX_PM { get; set; }
        public int NR_PV { get; set; }
        public int NR_PM { get; set; }
        public int NR_PONTOS_HAB { get; set; }
        public int? ID_RACA { get; set; }
        public int? ID_CLASSE { get; set; }
        public int ID_CAMPANHA { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AtributosDTO ATRIBUTOS { get; set; }
    }
}
