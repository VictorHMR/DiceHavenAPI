using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_Utils
{
    public class DiceHavenExcept : Exception
    {
        public HttpStatusCode CodeStatus { get; set; }

        public DiceHavenExcept(string mensagem)
            : base(mensagem)
        {
        }

        public DiceHavenExcept(string mensagem, HttpStatusCode Code)
            : base(mensagem)
        {
            this.CodeStatus = Code;
        }
    }

}
