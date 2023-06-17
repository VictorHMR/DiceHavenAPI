using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_Utils
{
    public class Enumeration
    {

        public enum Permissao
        {
            Visitante = 0,
            Usuario = 1,
            Moderador = 2,
            Admin = 4
        }
    }
}
