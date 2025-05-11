using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_API.DTOs.Response
{
    public class AuthTokenDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public int ID_USUARIO { get; set; }
    }
}
