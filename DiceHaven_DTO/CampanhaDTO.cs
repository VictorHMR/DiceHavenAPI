using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DiceHaven_Utils.Enumeration;

namespace DiceHaven_DTO
{
    public class CampanhaDTO
    {
        public int? ID_CAMPANHA { get; set; }
        public string DS_NOME_CAMPANHA { get; set; }
        public string DS_LORE { get; set; }
        public string DS_PERIODO { get; set; }
        public bool FL_EXISTE_MAGIA { get; set; }
        public TipoDefinicaoAtributos DS_DEFINICAO_ATRIBUTOS { get; set; }
        public string DS_XP_SUBIR_LVL { get; set; }
        public DateTime? DT_CRIACAO { get; set; }
        public bool? FL_ATIVO { get; set; }
        public int ID_USUARIO_CRIADOR { get; set; }
        public int ID_MESTRE_CAMPANHA { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<PersonagemDTO>? PERSONAGENS { get; set; }
    }
}
