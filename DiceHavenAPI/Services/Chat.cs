using DiceHavenAPI.Contexts;
using DiceHavenAPI.Models;
using DiceHavenAPI.DTOs;
using DiceHavenAPI.Interfaces;
using DiceHavenAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DiceHaven_API.Models;
using DiceHaven_API.DTOs;

namespace DiceHavenAPI.Services
{
    public class Chat : IChat
    {
        public DiceHavenBDContext dbDiceHaven;
        public ICampanha campanhaService;

        public Chat(DiceHavenBDContext dbDiceHaven, ICampanha campanhaService)
        {
            this.dbDiceHaven = dbDiceHaven;
            this.campanhaService = campanhaService;
        }

        public int EnviarMensagemCampanha(MensagemCampanhaDTO novaMensagem)
        {
            try
            {
                dbDiceHaven.Database.BeginTransaction();
                var campanha = campanhaService.ObterCampanha(novaMensagem.ID_CAMPANHA);

                var mensagem = new tb_campanha_mensagem
                {
                    DS_MENSAGEM = novaMensagem.DS_MENSAGEM,
                    FL_MESTRE = campanha.ID_MESTRE_CAMPANHA == novaMensagem.ID_USUARIO,
                    DT_MENSAGEM = novaMensagem?.DT_MENSAGEM ?? DateTime.Now,
                    ID_USUARIO = novaMensagem.ID_USUARIO,
                    ID_CAMPANHA = novaMensagem.ID_CAMPANHA,
                    ID_PERSONAGEM = novaMensagem.ID_PERSONAGEM
                };
                dbDiceHaven.Add(mensagem);
                dbDiceHaven.SaveChanges();
                dbDiceHaven.Database.CommitTransaction();

                return mensagem.ID_CAMPANHA_MENSAGEM;
            }
            catch (HttpDiceExcept ex)
            {
                dbDiceHaven.Database.RollbackTransaction();
                throw;
            }
            catch (Exception ex)
            {
                dbDiceHaven.Database.RollbackTransaction();
                throw new HttpDiceExcept($"Ocorreu um erro ao gravar mensagem. Message: {ex.Message}", HttpStatusCode.InternalServerError);

            }
        }

        public List<MensagemCampanhaDTO> ListarMensagensCampanha(int idCampanha)
        {
            try
            {

                return (from cm in dbDiceHaven.tb_campanha_mensagens 
                        where cm.ID_CAMPANHA == idCampanha
                        select new MensagemCampanhaDTO
                        {
                            ID_CAMPANHA_MENSAGEM = cm.ID_CAMPANHA_MENSAGEM,
                            DS_MENSAGEM = cm.DS_MENSAGEM,
                            FL_MESTRE = cm.FL_MESTRE,
                            DT_MENSAGEM = cm.DT_MENSAGEM,
                            ID_USUARIO = cm.ID_USUARIO,
                            ID_CAMPANHA = cm.ID_CAMPANHA,
                            ID_PERSONAGEM = cm.ID_PERSONAGEM
                        }).ToList();
            }
            catch (HttpDiceExcept ex)
            {
                dbDiceHaven.Database.RollbackTransaction();
                throw;
            }
            catch (Exception ex)
            {
                dbDiceHaven.Database.RollbackTransaction();
                throw new HttpDiceExcept($"Ocorreu um erro ao listar mensagem. Message: {ex.Message}", HttpStatusCode.InternalServerError);

            }
        }
    }
}
