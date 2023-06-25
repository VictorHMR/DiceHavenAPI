using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_DTO
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class AtributosDTO
    {
        public int? NR_STR { get; set; }
        public int? NR_DEX{ get; set; }
        public int? NR_CON{ get; set; }
        public int? NR_INT{ get; set; }
        public int? NR_WIS{ get; set; }
        public int? NR_CHA{ get; set; }
    }
}
