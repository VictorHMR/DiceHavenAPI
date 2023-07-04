using DiceHaven_BD.Contexts;
using DiceHaven_DTO;
using DiceHaven_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using DiceHaven_BD.Models;

namespace DiceHaven_Model.Models
{
    public class CampoFicha
    {
        public DiceHavenBDContext dbDiceHaven;

        public CampoFicha(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        public List<CampoFichaDTO> ListarCamposFicha(int idCampanha)
        {
            try
            {
                List<CampoFichaDTO> listaDeCampos = (from lc in dbDiceHaven.tb_campo_fichas
                                                      join c in dbDiceHaven.tb_campanhas on lc.ID_CAMPANHA equals c.ID_CAMPANHA
                                                      where lc.ID_CAMPANHA == idCampanha && lc.FL_ATIVO
                                                      select new CampoFichaDTO
                                                      {
                                                          ID_CAMPO_FICHA = lc.ID_CAMPO_FICHA,
                                                          DS_NOME_CAMPO = lc.DS_NOME_CAMPO,
                                                          TIPO_CAMPO = (Enumeration.TipoCampoFicha)lc.NR_TIPO_CAMPO,
                                                          DS_REFERENCIA = lc.DS_REFERENCIA,
                                                          DS_DESCRICAO = lc.DS_DESCRICAO,
                                                          FL_TEM_MODIFICADOR = lc.FL_TEM_MODIFICADOR,
                                                          FL_BLOQUEADO = lc.FL_BLOQUEADO,
                                                          FL_VISIVEL = lc.FL_VISIVEL,
                                                          DS_FORMULA_MODIFICADOR = lc.DS_FORMULA_MODIFICADOR,
                                                          DS_VALOR_PADRAO = lc.DS_VALOR_PADRAO,
                                                          NR_ORDEM = lc.NR_ORDEM,
                                                          ID_CAMPANHA = lc.ID_CAMPANHA
                                                      }).ToList();
                if (listaDeCampos is null)
                    throw new HttpDiceExcept("Não existe modelo de ficha cadastrado para essa campanha. Contade o mestre.", HttpStatusCode.InternalServerError);
                return listaDeCampos;
            }
            catch(HttpDiceExcept ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao listar campos da ficha. Message: {ex.Message}", HttpStatusCode.InternalServerError); 
            }

        }

        public CampoFichaDTO ObterCampoFicha(int idCampoFicha)
        {
            try
            {
                CampoFichaDTO camposFicha = (from lc in dbDiceHaven.tb_campo_fichas
                                                      where lc.ID_CAMPO_FICHA == idCampoFicha && lc.FL_ATIVO
                                                      select new CampoFichaDTO
                                                      {
                                                          ID_CAMPO_FICHA = lc.ID_CAMPO_FICHA,
                                                          DS_NOME_CAMPO = lc.DS_NOME_CAMPO,
                                                          TIPO_CAMPO = (Enumeration.TipoCampoFicha)lc.NR_TIPO_CAMPO,
                                                          DS_REFERENCIA = lc.DS_REFERENCIA,
                                                          DS_DESCRICAO = lc.DS_DESCRICAO,
                                                          FL_TEM_MODIFICADOR = lc.FL_TEM_MODIFICADOR,
                                                          FL_BLOQUEADO = lc.FL_BLOQUEADO,
                                                          FL_VISIVEL = lc.FL_VISIVEL,
                                                          DS_FORMULA_MODIFICADOR = lc.DS_FORMULA_MODIFICADOR,
                                                          DS_VALOR_PADRAO = lc.DS_VALOR_PADRAO,
                                                          NR_ORDEM = lc.NR_ORDEM,
                                                          ID_CAMPANHA = lc.ID_CAMPANHA
                                                      }).FirstOrDefault();
                if (camposFicha is null)
                    throw new HttpDiceExcept("Campo não existente. Contade o mestre.", HttpStatusCode.InternalServerError);
                return camposFicha;
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao listar campos da ficha. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public int CadastrarCampoFicha(CampoFichaDTO novoCampo)
        {
            try
            {
                tb_campo_ficha novoCampoBD = new tb_campo_ficha();
                novoCampoBD.DS_NOME_CAMPO = novoCampo.DS_NOME_CAMPO;
                novoCampoBD.NR_TIPO_CAMPO = (int)novoCampo.TIPO_CAMPO;
                novoCampoBD.DS_REFERENCIA = novoCampo.DS_REFERENCIA;
                novoCampoBD.DS_DESCRICAO = novoCampo.DS_DESCRICAO;
                novoCampoBD.FL_TEM_MODIFICADOR = novoCampo.FL_TEM_MODIFICADOR;
                novoCampoBD.FL_BLOQUEADO = novoCampo.FL_BLOQUEADO;
                novoCampoBD.FL_VISIVEL = novoCampo.FL_VISIVEL;
                novoCampoBD.DS_FORMULA_MODIFICADOR = novoCampo.DS_FORMULA_MODIFICADOR;
                novoCampoBD.DS_VALOR_PADRAO = novoCampo.DS_VALOR_PADRAO;
                novoCampoBD.NR_ORDEM = novoCampo.NR_ORDEM;
                novoCampoBD.FL_ATIVO = true;
                novoCampoBD.ID_CAMPANHA = novoCampo.ID_CAMPANHA;

                dbDiceHaven.tb_campo_fichas.Add(novoCampoBD);
                dbDiceHaven.SaveChanges();

                return novoCampoBD.ID_CAMPO_FICHA;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao listar campos da ficha. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public void EditarCampoFicha(CampoFichaDTO novoCampo)
        {
            try
            {
                tb_campo_ficha campoBD = dbDiceHaven.tb_campo_fichas.Find(novoCampo.ID_CAMPO_FICHA);
                campoBD.DS_NOME_CAMPO = novoCampo.DS_NOME_CAMPO;
                campoBD.NR_TIPO_CAMPO = (int)novoCampo.TIPO_CAMPO;
                campoBD.DS_REFERENCIA = novoCampo.DS_REFERENCIA;
                campoBD.DS_DESCRICAO = novoCampo.DS_DESCRICAO;
                campoBD.FL_TEM_MODIFICADOR = novoCampo.FL_TEM_MODIFICADOR;
                campoBD.FL_BLOQUEADO = novoCampo.FL_BLOQUEADO;
                campoBD.FL_VISIVEL = novoCampo.FL_VISIVEL;
                campoBD.DS_FORMULA_MODIFICADOR = novoCampo.DS_FORMULA_MODIFICADOR;
                campoBD.DS_VALOR_PADRAO = novoCampo.DS_VALOR_PADRAO;
                campoBD.NR_ORDEM = novoCampo.NR_ORDEM;
                campoBD.ID_CAMPANHA = novoCampo.ID_CAMPANHA;

                dbDiceHaven.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao listar campos da ficha. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public bool DeletarCampoFicha(int idCampoFicha)
        {
            try
            {
                tb_campo_ficha campoBD = dbDiceHaven.tb_campo_fichas.Find(idCampoFicha);
                bool possuiFichas = dbDiceHaven.tb_fichas.Where(x => x.ID_CAMPO_FICHA == idCampoFicha).Any();
                if (!possuiFichas)
                {
                    dbDiceHaven.tb_campo_fichas.Remove(campoBD);
                    dbDiceHaven.SaveChanges();
                    return true;
                }
                else
                {
                    campoBD.FL_ATIVO = false;
                    dbDiceHaven.SaveChanges();
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao listar campos da ficha. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }


    }
}
