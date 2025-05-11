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

namespace DiceHavenAPI.Services
{
    public class DadosFicha: IDadosFicha
    {
        public DiceHavenBDContext dbDiceHaven;

        public DadosFicha(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        public List<DadosFichaDTO> ListarDadosFicha(int idCampanha, int idPersonagem)
        {
            try
            {
                List<DadosFichaDTO> listaDeDados = (from df in dbDiceHaven.tb_dados_fichas
                                                    join cf in dbDiceHaven.tb_campo_fichas on df.ID_CAMPO_FICHA equals cf.ID_CAMPO_FICHA
                                                     join c in dbDiceHaven.tb_campanhas on cf.ID_CAMPANHA equals c.ID_CAMPANHA
                                                     where cf.ID_CAMPANHA == idCampanha && df.ID_PERSONAGEM == idPersonagem 
                                                     select new DadosFichaDTO
                                                     {
                                                         ID_DADOS_FICHA = df.ID_DADO_FICHA,
                                                         ID_CAMPO_FICHA = df.ID_CAMPO_FICHA,
                                                         ID_PERSONAGEM = df.ID_PERSONAGEM,
                                                         DS_VALOR = df.DS_VALOR,
                                                     }).ToList();
                return listaDeDados;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao listar valores dos campos da ficha. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }

        }

        public DadosFichaDTO ObterDadosFicha(int idCampoFicha, int idPersonagem)
        {
            try
            {
                DadosFichaDTO dadosFicha = (from df in dbDiceHaven.tb_dados_fichas
                                            join cf in dbDiceHaven.tb_campo_fichas on df.ID_CAMPO_FICHA equals cf.ID_CAMPO_FICHA
                                            where cf.ID_CAMPO_FICHA == idCampoFicha && df.ID_PERSONAGEM == idPersonagem 
                                            select new DadosFichaDTO
                                            {
                                                ID_DADOS_FICHA = df.ID_DADO_FICHA,
                                                ID_CAMPO_FICHA = df.ID_CAMPO_FICHA,
                                                ID_PERSONAGEM = df.ID_PERSONAGEM,
                                                DS_VALOR = df.DS_VALOR,
                                            }).FirstOrDefault();
                return dadosFicha;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao obter valor do campo da ficha. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public void GerarFichaPersonagem(int idPersonagem, int idCampanha)
        {
            try
            {
                dbDiceHaven.Database.BeginTransaction();
                Campanha campoFichaModels = new Campanha(dbDiceHaven);
                List<CampoFichaDTO> listaDeCampos =  campoFichaModels.ListarCamposFicha(idCampanha);

                foreach(CampoFichaDTO campo in listaDeCampos)
                {

                    tb_dados_ficha novoDado = new tb_dados_ficha();
                    novoDado.ID_CAMPO_FICHA = campo.ID_CAMPO_FICHA ?? 0;
                    novoDado.ID_PERSONAGEM = idPersonagem;
                    novoDado.DS_VALOR = campo.DS_VALOR_PADRAO;
                    dbDiceHaven.tb_dados_fichas.Add(novoDado);

                }
                dbDiceHaven.SaveChanges();
                dbDiceHaven.Database.CommitTransaction();
            }
            catch(HttpDiceExcept ex)
            {
                dbDiceHaven.Database.RollbackTransaction();
                throw ex;
            }
            catch (Exception ex)
            {
                dbDiceHaven.Database.RollbackTransaction();
                throw new HttpDiceExcept($"Ocorreu um erro ao obter valor do campo da ficha. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public void AtualizarDadosFicha(DadosFichaDTO novosDados)
        {
            try
            {
                Campanha campoFichaModels = new Campanha(dbDiceHaven);
                tb_dados_ficha dadosficha = dbDiceHaven.tb_dados_fichas.Where(x => x.ID_CAMPO_FICHA == novosDados.ID_CAMPO_FICHA && x.ID_PERSONAGEM == novosDados.ID_PERSONAGEM).FirstOrDefault();
                if (dadosficha is null)
                    throw new HttpDiceExcept("O campo informado não possui valor!", HttpStatusCode.InternalServerError);

                dadosficha.DS_VALOR = novosDados.DS_VALOR;
                dbDiceHaven.SaveChanges();

            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao obter valor do campo da ficha. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

    }
}
