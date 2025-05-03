using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiceHavenAPI.Utils
{
    public class HttpDiceExcept : Exception
    {
        public HttpStatusCode CodeStatus { get; set; }

        public HttpDiceExcept(string mensagem)
            : base(mensagem)
        {
        }

        public HttpDiceExcept(string mensagem, HttpStatusCode Code)
            : base(mensagem)
        {
            this.CodeStatus = Code;
        }
    }

}
