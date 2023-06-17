using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_DTO.ControleDeAcesso
{
    public class AuthTokenDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
