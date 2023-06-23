using DiceHaven_BD.Contexts;
using DiceHaven_BD.Models;
using DiceHaven_DTO;
using DiceHaven_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.CompilerServices;

namespace DiceHaven_Model.Models
{
    public class Raca
    {
        public DiceHavenBDContext dbDiceHaven;

        public Raca(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        public List<RacaDTO> ListarRacas(int idCampanha)
        {
            try
            {
                List<RacaDTO> listaRacas = (from r in dbDiceHaven.tb_racas
                                            where r.ID_CAMPANHA == idCampanha
                                            select new RacaDTO
                                            {
                                                ID_RACA = r.ID_RACA,
                                                DS_RACA = r.DS_RACA,
                                                DS_DESCRICAO = r.DS_DESCRICAO,
                                                DS_FOTO = Conversor.ConvertToBase64(r.DS_FOTO),
                                                ID_CAMPANHA = r.ID_CAMPANHA,
                                                NR_STR = r.NR_STR_PADRAO,
                                                NR_DEX = r.NR_DEX_PADRAO,
                                                NR_CON = r.NR_CON_PADRAO,
                                                NR_INT = r.NR_INT_PADRAO,
                                                NR_WIS = r.NR_WIS_PADRAO,
                                                NR_CHA = r.NR_CHA_PADRAO
                                            }).ToList();
                if (listaRacas is null)
                    return new List<RacaDTO>();
                else
                    return listaRacas;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept("Ocorreu um erro ao listar raças!", HttpStatusCode.InternalServerError);
            }
        }

        public RacaDTO ObterRaca(int idRaca)
        {
            try
            {
                RacaDTO raca = (from r in dbDiceHaven.tb_racas
                                where r.ID_RACA == idRaca
                                select new RacaDTO
                                {
                                    ID_RACA = r.ID_RACA,
                                    DS_RACA = r.DS_RACA,
                                    DS_DESCRICAO = r.DS_DESCRICAO,
                                    DS_FOTO = Conversor.ConvertToBase64(r.DS_FOTO),
                                    ID_CAMPANHA = r.ID_CAMPANHA,
                                    NR_STR = r.NR_STR_PADRAO,
                                    NR_DEX = r.NR_DEX_PADRAO,
                                    NR_CON = r.NR_CON_PADRAO,
                                    NR_INT = r.NR_INT_PADRAO,
                                    NR_WIS = r.NR_WIS_PADRAO,
                                    NR_CHA = r.NR_CHA_PADRAO
                                }).FirstOrDefault();
                if (raca is null)
                    return new RacaDTO();
                else
                    return raca;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept("Ocorreu um erro ao listar raças!", HttpStatusCode.InternalServerError);
            }
        }

        public int CadastrarRaca(RacaDTO novaRaca, int idUsuarioLogado)
        {
            try
            {
                tb_raca novaRacaBD = new tb_raca();
                tb_campanha campanha = dbDiceHaven.tb_campanhas.Find(novaRaca.ID_CAMPANHA);
                if (campanha.ID_MESTRE_CAMPANHA != idUsuarioLogado || campanha.ID_USUARIO_CRIADOR != idUsuarioLogado)
                    throw new HttpDiceExcept("Voce não tem permissão para criar raças!", HttpStatusCode.InternalServerError);
                if (campanha is null)
                    throw new HttpDiceExcept("A campanha informada não existe!", HttpStatusCode.InternalServerError);

                novaRacaBD.DS_RACA = novaRaca.DS_RACA;
                novaRacaBD.DS_DESCRICAO = novaRaca.DS_DESCRICAO;
                novaRacaBD.DS_FOTO = Conversor.ConvertToByteArray(novaRaca.DS_FOTO);
                novaRacaBD.NR_STR_PADRAO = novaRaca.NR_STR;
                novaRacaBD.NR_DEX_PADRAO = novaRaca.NR_DEX;
                novaRacaBD.NR_CON_PADRAO = novaRaca.NR_CON;
                novaRacaBD.NR_INT_PADRAO = novaRaca.NR_INT;
                novaRacaBD.NR_WIS_PADRAO = novaRaca.NR_WIS;
                novaRacaBD.NR_CHA_PADRAO = novaRaca.NR_CHA;
                novaRacaBD.ID_CAMPANHA = novaRaca.ID_CAMPANHA;
                dbDiceHaven.tb_racas.Add(novaRacaBD);
                dbDiceHaven.SaveChanges();
                return novaRacaBD.ID_RACA;
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao criar campanha! Message: {ex.Message}", HttpStatusCode.InternalServerError);

            }
        }

        public void EditarRaca(RacaDTO novosDados, int idUsuarioLogado)
        {
            try
            {
                tb_raca racaBD = dbDiceHaven.tb_racas.Find(novosDados.ID_RACA);
                tb_campanha campanha = dbDiceHaven.tb_campanhas.Find(racaBD?.ID_CAMPANHA);
                if (campanha.ID_MESTRE_CAMPANHA != idUsuarioLogado || campanha.ID_USUARIO_CRIADOR != idUsuarioLogado)
                    throw new HttpDiceExcept("Voce não tem permissão para editar raças!", HttpStatusCode.InternalServerError);
                if(racaBD is null)
                    throw new HttpDiceExcept("A raça informada não existe!", HttpStatusCode.InternalServerError);
                if (campanha is null)
                    throw new HttpDiceExcept("A campanha informada não existe!", HttpStatusCode.InternalServerError);

                racaBD.DS_RACA = novosDados.DS_RACA;
                racaBD.DS_DESCRICAO = novosDados.DS_DESCRICAO;
                racaBD.DS_FOTO = Conversor.ConvertToByteArray(novosDados.DS_FOTO);
                dbDiceHaven.SaveChanges();
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao criar campanha! Message: {ex.Message}", HttpStatusCode.InternalServerError);

            }
        }

        public void DeletarRaca(int idRaca, int idUsuarioLogado)
        {
            try
            {
                dbDiceHaven.Database.BeginTransaction();
                tb_raca raca = dbDiceHaven.tb_racas.Find(idRaca);
                List<tb_ficha> fichasVinculadas = dbDiceHaven.tb_fichas.Where(x => x.ID_RACA == idRaca).ToList();
                tb_campanha campanha = dbDiceHaven.tb_campanhas.Find(raca?.ID_CAMPANHA);

                if (campanha.ID_MESTRE_CAMPANHA != idUsuarioLogado || campanha.ID_USUARIO_CRIADOR != idUsuarioLogado)
                    throw new HttpDiceExcept("Voce não tem permissão para deletar raças!", HttpStatusCode.InternalServerError);
                if (raca is null)
                    throw new HttpDiceExcept("A raça informada não existe!", HttpStatusCode.InternalServerError);
                if (campanha is null)
                    throw new HttpDiceExcept("A campanha informada não existe!", HttpStatusCode.InternalServerError);

                foreach (tb_ficha ficha in fichasVinculadas)
                {
                    ficha.ID_RACA = null;
                }
                dbDiceHaven.tb_racas.Remove(raca);
                dbDiceHaven.SaveChanges();
                dbDiceHaven.Database.CommitTransaction();
            }
            catch(Exception ex)
            {
                dbDiceHaven.Database.RollbackTransaction();
                throw new HttpDiceExcept($"Ocorreu um erro ao deletar raça! Message: {ex.Message}");
            }
        }
    }
}
