using DiceHaven_BD.Contexts;
using DiceHaven_DTO.ControleDeAcesso;
using DiceHaven_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace DiceHaven_Model.Models.ControlleDeAcesso
{
    public class Grupo
    {
        public DiceHavenBDContext dbDiceHaven;

        public Grupo(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        public List<GrupoDTO> ListarGrupos()
        {
            try
            {
                List<GrupoDTO> listaDeGrupos = (from g in dbDiceHaven.TB_GRUPOs
                                                //where g.FL_ADMIN == false
                                                select new GrupoDTO
                                                {
                                                    ID_GRUPO = g.ID_GRUPO,
                                                    DS_GRUPO = g.DS_GRUPO,
                                                    DS_DESCRICAO = g.DS_DESCRICAO,
                                                    FL_ADMIN = g.FL_ADMIN,
                                                }).ToList();
                return listaDeGrupos;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept("Ocorreu um erro na listagem de grúpos", HttpStatusCode.InternalServerError);
            }
        }
    }
}
