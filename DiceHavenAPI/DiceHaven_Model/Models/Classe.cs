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
    public class Classe
    {
        public DiceHavenBDContext dbDiceHaven;

        public Classe(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        public List<ClasseDTO> ListarClasses(int idCampanha)
        {
            try
            {
                List<ClasseDTO> listaClasses = (from c in dbDiceHaven.tb_classes
                                            where c.ID_CAMPANHA == idCampanha
                                            select new ClasseDTO
                                            {
                                                ID_CLASSE = c.ID_CLASSE,
                                                DS_CLASSE = c.DS_CLASSE,
                                                DS_DESCRICAO = c.DS_DESCRICAO,
                                                DS_FOTO = Conversor.ConvertToBase64(c.DS_FOTO),
                                                ID_CAMPANHA = c.ID_CAMPANHA,
                                                NR_STR = c.NR_STR_PADRAO,
                                                NR_DEX = c.NR_DEX_PADRAO,
                                                NR_CON = c.NR_CON_PADRAO,
                                                NR_INT = c.NR_INT_PADRAO,
                                                NR_WIS = c.NR_WIS_PADRAO,
                                                NR_CHA = c.NR_CHA_PADRAO
                                            }).ToList();
                if (listaClasses is null)
                    return new List<ClasseDTO>();
                else
                    return listaClasses;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept("Ocorreu um erro ao listar raças!", HttpStatusCode.InternalServerError);
            }
        }

        public ClasseDTO ObterClasse(int idClasse)
        {
            try
            {
                ClasseDTO classe = (from c in dbDiceHaven.tb_classes
                                where c.ID_CLASSE == idClasse
                                select new ClasseDTO
                                {
                                    ID_CLASSE = c.ID_CLASSE,
                                    DS_CLASSE = c.DS_CLASSE,
                                    DS_DESCRICAO = c.DS_DESCRICAO,
                                    DS_FOTO = Conversor.ConvertToBase64(c.DS_FOTO),
                                    ID_CAMPANHA = c.ID_CAMPANHA,
                                    NR_STR = c.NR_STR_PADRAO,
                                    NR_DEX = c.NR_DEX_PADRAO,
                                    NR_CON = c.NR_CON_PADRAO,
                                    NR_INT = c.NR_INT_PADRAO,
                                    NR_WIS = c.NR_WIS_PADRAO,
                                    NR_CHA = c.NR_CHA_PADRAO
                                }).FirstOrDefault();
                if (classe is null)
                    return new ClasseDTO();
                else
                    return classe;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept("Ocorreu um erro ao listar raças!", HttpStatusCode.InternalServerError);
            }
        }

        public int CadastrarClasse(ClasseDTO novaClasse, int idUsuarioLogado)
        {
            try
            {
                tb_classe novaClasseBD = new tb_classe();
                tb_campanha campanha = dbDiceHaven.tb_campanhas.Find(novaClasse.ID_CAMPANHA);
                if (campanha.ID_MESTRE_CAMPANHA != idUsuarioLogado || campanha.ID_USUARIO_CRIADOR != idUsuarioLogado)
                    throw new HttpDiceExcept("Voce não tem permissão para criar classes!", HttpStatusCode.InternalServerError);
                if (campanha is null)
                    throw new HttpDiceExcept("A campanha informada não existe!", HttpStatusCode.InternalServerError);

                novaClasseBD.DS_CLASSE = novaClasse.DS_CLASSE;
                novaClasseBD.DS_DESCRICAO = novaClasse.DS_DESCRICAO;
                novaClasseBD.DS_FOTO = Conversor.ConvertToByteArray(novaClasse.DS_FOTO);
                novaClasseBD.NR_STR_PADRAO = novaClasse.NR_STR;
                novaClasseBD.NR_DEX_PADRAO = novaClasse.NR_DEX;
                novaClasseBD.NR_CON_PADRAO = novaClasse.NR_CON;
                novaClasseBD.NR_INT_PADRAO = novaClasse.NR_INT;
                novaClasseBD.NR_WIS_PADRAO = novaClasse.NR_WIS;
                novaClasseBD.NR_CHA_PADRAO = novaClasse.NR_CHA;
                novaClasseBD.ID_CAMPANHA = novaClasse.ID_CAMPANHA;
                dbDiceHaven.tb_classes.Add(novaClasseBD);
                dbDiceHaven.SaveChanges();
                return novaClasseBD.ID_CLASSE;
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

        public void EditarClasse(ClasseDTO novosDados, int idUsuarioLogado)
        {
            try
            {
                tb_classe classeBD = dbDiceHaven.tb_classes.Find(novosDados.ID_CLASSE);
                tb_campanha campanha = dbDiceHaven.tb_campanhas.Find(classeBD?.ID_CAMPANHA);
                if (campanha.ID_MESTRE_CAMPANHA != idUsuarioLogado || campanha.ID_USUARIO_CRIADOR != idUsuarioLogado)
                    throw new HttpDiceExcept("Voce não tem permissão para editar classes!", HttpStatusCode.InternalServerError);
                if(classeBD is null)
                    throw new HttpDiceExcept("A classe informada não existe!", HttpStatusCode.InternalServerError);
                if (campanha is null)
                    throw new HttpDiceExcept("A campanha informada não existe!", HttpStatusCode.InternalServerError);

                classeBD.DS_CLASSE = novosDados.DS_CLASSE;
                classeBD.DS_DESCRICAO = novosDados.DS_DESCRICAO;
                classeBD.DS_FOTO = Conversor.ConvertToByteArray(novosDados.DS_FOTO);
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

        public void DeletarClasse(int idClasse, int idUsuarioLogado)
        {
            try
            {
                dbDiceHaven.Database.BeginTransaction();
                tb_classe classe = dbDiceHaven.tb_classes.Find(idClasse);
                List<tb_ficha> fichasVinculadas = dbDiceHaven.tb_fichas.Where(x => x.ID_CLASSE == idClasse).ToList();
                tb_campanha campanha = dbDiceHaven.tb_campanhas.Find(classe?.ID_CAMPANHA);

                if (campanha.ID_MESTRE_CAMPANHA != idUsuarioLogado || campanha.ID_USUARIO_CRIADOR != idUsuarioLogado)
                    throw new HttpDiceExcept("Voce não tem permissão para deletar classes!", HttpStatusCode.InternalServerError);
                if (classe is null)
                    throw new HttpDiceExcept("A classe informada não existe!", HttpStatusCode.InternalServerError);
                if (campanha is null)
                    throw new HttpDiceExcept("A campanha informada não existe!", HttpStatusCode.InternalServerError);

                foreach (tb_ficha ficha in fichasVinculadas)
                {
                    ficha.ID_CLASSE = null;
                }
                dbDiceHaven.tb_classes.Remove(classe);
                dbDiceHaven.SaveChanges();
                dbDiceHaven.Database.CommitTransaction();
            }
            catch(Exception ex)
            {
                dbDiceHaven.Database.RollbackTransaction();
                throw new HttpDiceExcept($"Ocorreu um erro ao deletar a classe! Message: {ex.Message}");
            }
        }
    }
}
