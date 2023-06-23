using DiceHaven_BD.Contexts;
using DiceHaven_BD.Models;
using DiceHaven_DTO;
using DiceHaven_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static DiceHaven_Utils.Enumeration;

namespace DiceHaven_Model.Models
{
    public class Campanha
    {
        public DiceHavenBDContext dbDiceHaven;

        public Campanha(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }
        
        public CampanhaDTO ObterCampanha(int idCampanha)
        {
            try
            {
                CampanhaDTO campanha = (from c in dbDiceHaven.tb_campanhas
                                        where c.ID_CAMPANHA == idCampanha
                                        select new CampanhaDTO
                                        {
                                            ID_CAMPANHA = c.ID_CAMPANHA,
                                            DS_NOME_CAMPANHA = c.DS_NOME_CAMPANHA,
                                            DS_LORE = c.DS_LORE,
                                            DS_PERIODO = c.DS_PERIODO,
                                            FL_EXISTE_MAGIA = c.FL_EXISTE_MAGIA,
                                            DS_DEFINICAO_ATRIBUTOS = (TipoDefinicaoAtributos)c.NR_DEFINICAO_ATRIBUTOS,
                                            DS_XP_SUBIR_LVL = c.DS_XP_SUBIR_LVL,
                                            DT_CRIACAO = c.DT_CRIACAO,
                                            FL_ATIVO = c.FL_ATIVO,
                                            ID_USUARIO_CRIADOR = c.ID_USUARIO_CRIADOR,
                                            ID_MESTRE_CAMPANHA = c.ID_MESTRE_CAMPANHA
                                        }).FirstOrDefault();
                if (campanha is null)
                    throw new HttpDiceExcept("Campanha não encontrada!", HttpStatusCode.InternalServerError);
                return campanha;
            }
            catch(HttpDiceExcept ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao obter campanha! Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public List<CampanhaDTO> ListarCampanhas()
        {
            try
            {
                List<CampanhaDTO> campanhas = (from c in dbDiceHaven.tb_campanhas
                                                select new CampanhaDTO
                                                {
                                                    ID_CAMPANHA = c.ID_CAMPANHA,
                                                    DS_NOME_CAMPANHA = c.DS_NOME_CAMPANHA,
                                                    DS_LORE = c.DS_LORE,
                                                    DS_PERIODO = c.DS_PERIODO,
                                                    FL_EXISTE_MAGIA = c.FL_EXISTE_MAGIA,
                                                    DS_DEFINICAO_ATRIBUTOS =  (TipoDefinicaoAtributos)c.NR_DEFINICAO_ATRIBUTOS,
                                                    DS_XP_SUBIR_LVL = c.DS_XP_SUBIR_LVL,
                                                    DT_CRIACAO = c.DT_CRIACAO,
                                                    FL_ATIVO = c.FL_ATIVO,
                                                    ID_USUARIO_CRIADOR = c.ID_USUARIO_CRIADOR,
                                                    ID_MESTRE_CAMPANHA = c.ID_MESTRE_CAMPANHA
                                                }).ToList();
                if (campanhas is null)
                    return new List<CampanhaDTO>();
                return campanhas;
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao listar campanhas! Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        } 

        public int CadastrarCampanha(CampanhaDTO novaCampanha, int idUsuarioLogado)
        {
            try
            {
                tb_campanha novaCampanhaBD = new tb_campanha();
                novaCampanhaBD.DS_NOME_CAMPANHA = novaCampanha.DS_NOME_CAMPANHA;
                novaCampanhaBD.DS_LORE = novaCampanha.DS_LORE;
                novaCampanhaBD.DS_PERIODO = novaCampanha.DS_PERIODO;
                novaCampanhaBD.FL_EXISTE_MAGIA = novaCampanha.FL_EXISTE_MAGIA;
                novaCampanhaBD.NR_DEFINICAO_ATRIBUTOS = (int)novaCampanha.DS_DEFINICAO_ATRIBUTOS;
                novaCampanhaBD.DS_XP_SUBIR_LVL = novaCampanha.DS_XP_SUBIR_LVL;
                novaCampanhaBD.DT_CRIACAO = DateTime.Now;
                novaCampanhaBD.FL_ATIVO = true;
                novaCampanhaBD.ID_USUARIO_CRIADOR = idUsuarioLogado;
                novaCampanhaBD.ID_MESTRE_CAMPANHA = novaCampanha?.ID_MESTRE_CAMPANHA ?? idUsuarioLogado;

                dbDiceHaven.tb_campanhas.Add(novaCampanhaBD);
                dbDiceHaven.SaveChanges();

                return novaCampanhaBD.ID_CAMPANHA;

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

        public void AtualizarCampanha(CampanhaDTO campanhaAtualizada)
        {
            try
            {

                tb_campanha CampanhaBD = dbDiceHaven.tb_campanhas.Find(campanhaAtualizada.ID_CAMPANHA);
                if (CampanhaBD is null)
                    throw new HttpDiceExcept("A campanha informada não existe !", HttpStatusCode.InternalServerError);
                CampanhaBD.DS_NOME_CAMPANHA = campanhaAtualizada.DS_NOME_CAMPANHA;
                CampanhaBD.DS_LORE = campanhaAtualizada.DS_LORE;
                CampanhaBD.DS_PERIODO = campanhaAtualizada.DS_PERIODO;
                CampanhaBD.DS_XP_SUBIR_LVL = campanhaAtualizada.DS_XP_SUBIR_LVL;
                CampanhaBD.FL_EXISTE_MAGIA = campanhaAtualizada.FL_EXISTE_MAGIA;
                CampanhaBD.FL_ATIVO = campanhaAtualizada.FL_ATIVO ?? true;
                CampanhaBD.ID_MESTRE_CAMPANHA = campanhaAtualizada?.ID_MESTRE_CAMPANHA ?? CampanhaBD.ID_MESTRE_CAMPANHA;
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


    }
}
