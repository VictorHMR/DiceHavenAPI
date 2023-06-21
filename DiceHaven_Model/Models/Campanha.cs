using DiceHaven_BD.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_Model.Models
{
    public class Campanha
    {
        public DiceHavenBDContext dbDiceHaven;

        public Campanha(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

    }
}
