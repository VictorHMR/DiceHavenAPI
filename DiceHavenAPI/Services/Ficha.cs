using DiceHavenAPI.Contexts;
using DiceHavenAPI.DTOs;
using DiceHavenAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net; 
using DiceHavenAPI.Models;
using Microsoft.AspNetCore.Mvc;
using DiceHavenAPI.Interfaces;
using DiceHaven_API.DTOs.Response;
using DiceHaven_API.Models;

namespace DiceHavenAPI.Services
{
    public class Ficha: IFicha
    {
        public DiceHavenBDContext dbDiceHaven;
        public ICampanha campanhaService;
        public IPersonagem personagemService;
        public Ficha(DiceHavenBDContext dbDiceHaven, ICampanha campanhaService, IPersonagem personagemService)
        {
            this.dbDiceHaven = dbDiceHaven;
            this.campanhaService = campanhaService;
            this.personagemService = personagemService;
        }
        public List<SecaoFichaDTO> ObterModeloDeFicha(int idCampanha)
        {
            try
            {
                List<SecaoFichaDTO> lstSecoes = dbDiceHaven.tb_secao_fichas.Where(x => x.ID_CAMPANHA == idCampanha).Select( x=> new SecaoFichaDTO
                {
                    ID_SECAO_FICHA = x.ID_SECAO_FICHA,
                    DS_NOME_SECAO = x.DS_NOME_SECAO,
                    NR_ORDEM = x.NR_ORDEM,
                    ID_CAMPANHA = x.ID_CAMPANHA
                }).ToList();

                foreach (var secao in lstSecoes)
                {
                    List<CampoFichaDTO> lstcampos = dbDiceHaven.tb_campo_fichas.Where(x => x.ID_SECAO_FICHA == secao.ID_SECAO_FICHA).Select(x => new CampoFichaDTO
                    {
                        ID_CAMPO_FICHA = x.ID_CAMPO_FICHA,
                        DS_NOME_CAMPO = x.DS_NOME_CAMPO,
                        TIPO_CAMPO = (Enumeration.TipoCampoFicha)x.NR_TIPO_CAMPO,
                        FL_BLOQUEADO = x.FL_BLOQUEADO,
                        FL_VISIVEL = x.FL_VISIVEL,
                        FL_MODIFICADOR = x.FL_MODIFICADOR,
                        DS_VALOR_PADRAO = x.DS_VALOR_PADRAO,
                        NR_ORDEM = x.NR_ORDEM,
                        ID_SECAO_FICHA = x.ID_SECAO_FICHA
                    }).ToList();

                    secao.CAMPOS = lstcampos;
                }

                if (lstSecoes is null)
                    throw new HttpDiceExcept("Não existe modelo de ficha cadastrado para essa campanha. Contate o mestre.", HttpStatusCode.InternalServerError);
                return lstSecoes;
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

        public void EditarModeloDeFicha(List<SecaoFichaDTO> lstSecao)
        {
            try
            {
                dbDiceHaven.Database.BeginTransaction();

                foreach(SecaoFichaDTO secao in lstSecao)
                {
                    if(secao.ID_SECAO_FICHA is null)
                    {
                        tb_secao_ficha novaSecao = new tb_secao_ficha();
                        novaSecao.DS_NOME_SECAO = secao.DS_NOME_SECAO;
                        novaSecao.NR_ORDEM = secao.NR_ORDEM;
                        novaSecao.ID_CAMPANHA = secao.ID_CAMPANHA;

                        dbDiceHaven.Add(novaSecao);
                        dbDiceHaven.SaveChanges();
                        secao.CAMPOS.ForEach(C => C.ID_SECAO_FICHA = novaSecao.ID_SECAO_FICHA);
                        EditarCamposFicha(secao.CAMPOS);
                    }
                    else
                    {
                        tb_secao_ficha secaoBD = dbDiceHaven.tb_secao_fichas.Find(secao.ID_SECAO_FICHA);
                        if (secao.FL_DELETE)
                        {
                            secao.CAMPOS.ForEach(C => C.FL_DELETE = true);
                            EditarCamposFicha(secao.CAMPOS);
                            dbDiceHaven.tb_secao_fichas.Remove(secaoBD);
                            dbDiceHaven.SaveChanges();
                        }
                        else
                        {
                            secaoBD.DS_NOME_SECAO = secao.DS_NOME_SECAO;
                            secaoBD.NR_ORDEM = secao.NR_ORDEM;
                            dbDiceHaven.tb_secao_fichas.Update(secaoBD);
                            secao.CAMPOS.ForEach(C => C.ID_SECAO_FICHA = secaoBD.ID_SECAO_FICHA);

                            EditarCamposFicha(secao.CAMPOS);
                            dbDiceHaven.SaveChanges();
                        }
                    }
                }

                dbDiceHaven.Database.CommitTransaction();

            }
            catch (Exception ex)
            {
                dbDiceHaven.Database.RollbackTransaction();
                throw new HttpDiceExcept($"Ocorreu um erro ao editar campos da ficha. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        private void EditarCamposFicha(List<CampoFichaDTO> lstCampos)
        {
            foreach (var novoCampo in lstCampos)
            {
                if (novoCampo.ID_CAMPO_FICHA is null)
                {
                    tb_campo_ficha campoBD = new tb_campo_ficha();
                    campoBD.DS_NOME_CAMPO = novoCampo.DS_NOME_CAMPO;
                    campoBD.NR_TIPO_CAMPO = (int)novoCampo.TIPO_CAMPO;
                    campoBD.FL_BLOQUEADO = novoCampo.FL_BLOQUEADO;
                    campoBD.FL_VISIVEL = novoCampo.FL_VISIVEL;
                    campoBD.FL_MODIFICADOR = novoCampo.FL_MODIFICADOR;
                    campoBD.DS_VALOR_PADRAO = novoCampo.DS_VALOR_PADRAO;
                    campoBD.NR_ORDEM = novoCampo.NR_ORDEM;
                    campoBD.ID_SECAO_FICHA = novoCampo.ID_SECAO_FICHA;

                    dbDiceHaven.tb_campo_fichas.Add(campoBD);
                }
                else
                {
                    tb_campo_ficha campoBD = dbDiceHaven.tb_campo_fichas.Find(novoCampo.ID_CAMPO_FICHA);
                    if (novoCampo.FL_DELETE)
                    {
                        List<tb_dados_ficha> lstDadosPersonagem = dbDiceHaven.tb_dados_fichas.Where(x => x.ID_CAMPO_FICHA == campoBD.ID_CAMPO_FICHA).ToList();
                        dbDiceHaven.tb_dados_fichas.RemoveRange(lstDadosPersonagem);
                        dbDiceHaven.tb_campo_fichas.Remove(campoBD);
                    }
                    else
                    {
                        campoBD.DS_NOME_CAMPO = novoCampo.DS_NOME_CAMPO;
                        campoBD.NR_TIPO_CAMPO = (int)novoCampo.TIPO_CAMPO;
                        campoBD.FL_MODIFICADOR = novoCampo.FL_MODIFICADOR;
                        campoBD.FL_BLOQUEADO = novoCampo.FL_BLOQUEADO;
                        campoBD.FL_VISIVEL = novoCampo.FL_VISIVEL;
                        campoBD.DS_VALOR_PADRAO = novoCampo.DS_VALOR_PADRAO;
                        campoBD.NR_ORDEM = novoCampo.NR_ORDEM;
                        campoBD.ID_SECAO_FICHA = novoCampo.ID_SECAO_FICHA;
                    }
                }

                dbDiceHaven.SaveChanges();
            }
        }

        public FichaDTO ListarDadosFicha(int idCampanha, int? idPersonagem)
        {
            try
            {

                PersonagemDTO personagem = idPersonagem is not null ? personagemService.ObterPersonagem(idPersonagem ?? 0) : new PersonagemDTO();
                List<SecaoDTO> lstSecao = dbDiceHaven.tb_secao_fichas.Where(s => s.ID_CAMPANHA == idCampanha).Select(x => new SecaoDTO
                {
                    ID_SECAO_FICHA = x.ID_SECAO_FICHA,
                    DS_NOME_SECAO = x.DS_NOME_SECAO,
                    NR_ORDEM = x.NR_ORDEM
                }).ToList();

                foreach(var secao in lstSecao)
                {
                    List<DadoFichaDTO> dadosFicha = (from cf in dbDiceHaven.tb_campo_fichas
                                                     join df in dbDiceHaven.tb_dados_fichas
                                                     on cf.ID_CAMPO_FICHA equals df.ID_CAMPO_FICHA into dadoCampo
                                                     from df in dadoCampo.Where(x => x.ID_PERSONAGEM == idPersonagem).DefaultIfEmpty()
                                                     where cf.ID_SECAO_FICHA == secao.ID_SECAO_FICHA
                                                     select new DadoFichaDTO
                                                     {
                                                         ID_DADO_FICHA = df != null ? df.ID_DADO_FICHA : (int?)null,
                                                         CAMPO_FICHA = new CampoFichaDTO
                                                         {
                                                             ID_CAMPO_FICHA = cf.ID_CAMPO_FICHA,
                                                             DS_NOME_CAMPO = cf.DS_NOME_CAMPO,
                                                             TIPO_CAMPO = (Enumeration.TipoCampoFicha)cf.NR_TIPO_CAMPO,
                                                             FL_BLOQUEADO = cf.FL_BLOQUEADO,
                                                             FL_VISIVEL = cf.FL_VISIVEL,
                                                             FL_MODIFICADOR = cf.FL_MODIFICADOR,
                                                             DS_VALOR_PADRAO = cf.DS_VALOR_PADRAO,
                                                             NR_ORDEM = cf.NR_ORDEM,
                                                             ID_SECAO_FICHA = cf.ID_SECAO_FICHA
                                                         },
                                                         DS_VALOR = df.DS_VALOR,
                                                         DS_VALOR_MODIFICADOR = df.DS_VALOR_MODIFICADOR ?? 0
                                                     }).ToList();

                    secao.LST_DADOS_FICHA = dadosFicha;

                }


                return new FichaDTO
                {
                    ID_CAMPANHA = idCampanha,
                    PERSONAGEM = personagem,
                    LST_SECAO_FICHA = lstSecao
                };
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao listar valores dos campos da ficha. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }

        }

        public void GravarFicha(FichaDTO dadosFicha)
        {
            try
            {
                dbDiceHaven.Database.BeginTransaction();

                if(dadosFicha.PERSONAGEM?.ID_PERSONAGEM is null)
                    dadosFicha.PERSONAGEM.ID_PERSONAGEM = personagemService.CadastrarPersonagem(dadosFicha.PERSONAGEM);
                else
                    personagemService.EditarPersonagem(dadosFicha.PERSONAGEM);
                

                if (!dbDiceHaven.tb_personagem_campanhas.Any(x => x.ID_PERSONAGEM == dadosFicha.PERSONAGEM.ID_PERSONAGEM && x.ID_CAMPANHA == dadosFicha.ID_CAMPANHA))
                {
                    tb_personagem_campanha personagemCampanha = new tb_personagem_campanha();
                    personagemCampanha.ID_PERSONAGEM = dadosFicha.PERSONAGEM.ID_PERSONAGEM ?? 0;
                    personagemCampanha.ID_CAMPANHA = dadosFicha.ID_CAMPANHA;
                    personagemCampanha.DT_REGISTRO = DateTime.Now;
                    dbDiceHaven.Add(personagemCampanha);
                }
                foreach (var secao in dadosFicha.LST_SECAO_FICHA)
                {
                    foreach (var dado in secao.LST_DADOS_FICHA)
                    {
                        tb_dados_ficha dadosFichaBD = dbDiceHaven.tb_dados_fichas.Find(dado.ID_DADO_FICHA) ?? new tb_dados_ficha();
                        dadosFichaBD.ID_PERSONAGEM = dadosFicha.PERSONAGEM.ID_PERSONAGEM ?? 0;
                        dadosFichaBD.ID_CAMPO_FICHA = dado.CAMPO_FICHA.ID_CAMPO_FICHA ?? 0;
                        dadosFichaBD.DS_VALOR = dado.DS_VALOR ?? dado.CAMPO_FICHA.DS_VALOR_PADRAO;
                        dadosFichaBD.DS_VALOR_MODIFICADOR = dado.DS_VALOR_MODIFICADOR;

                        if (dado.ID_DADO_FICHA is null)
                            dbDiceHaven.Add(dadosFichaBD);
                        else
                            dbDiceHaven.tb_dados_fichas.Update(dadosFichaBD);
                    }
                }
                dbDiceHaven.SaveChanges();
                dbDiceHaven.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                dbDiceHaven.Database.RollbackTransaction();
                throw new HttpDiceExcept($"Ocorreu um erro ao gravar ficha. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
    }
}
