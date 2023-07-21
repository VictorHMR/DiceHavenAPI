using DiceHaven_BD.Contexts;
using DiceHaven_BD.Models;
using DiceHaven_DTO;
using DiceHaven_Model.Interfaces;
using DiceHaven_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_Model.Models
{
    public class Chat : IChat
    {
        public DiceHavenBDContext dbDiceHaven;

        public Chat(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }


        public List<UsuarioDTO> ListarChatsUsuario(int idUsuarioLogado)
        {
            try
            {
                List<tb_chat> lstChats = dbDiceHaven.tb_chats.Where(x => (x.ID_USUARIO_1 == idUsuarioLogado && x.FL_ATIVO_USR_1) || (x.ID_USUARIO_2 == idUsuarioLogado && x.FL_ATIVO_USR_2)).ToList();
                List<UsuarioDTO> lstUsuariosChat = new List<UsuarioDTO>();
                IUsuario _usuario = new Usuario(dbDiceHaven);
                foreach(tb_chat chat in lstChats)
                {
                    if (chat.ID_USUARIO_1 == idUsuarioLogado)
                        lstUsuariosChat.Add(_usuario.obterUsuario(chat.ID_USUARIO_2));
                    else
                        lstUsuariosChat.Add(_usuario.obterUsuario(chat.ID_USUARIO_1));
                }

                return lstUsuariosChat;

            }
            catch (HttpDiceExcept ex)
            {
                throw ex;

            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao listar chats. Message: {ex.Message}", HttpStatusCode.InternalServerError);

            }

        }
        public void IniciarChat(int idUsuarioLogado, int idUsuario)
        {
            try
            {
                tb_chat chat = dbDiceHaven.tb_chats.Where(x => (x.ID_USUARIO_1 == idUsuarioLogado && x.ID_USUARIO_2 == idUsuario) ||
                                                             (x.ID_USUARIO_2 == idUsuario && x.ID_USUARIO_1 == idUsuarioLogado)).FirstOrDefault();
                if (chat is null)
                {
                    tb_chat novoChatBD = new tb_chat();
                    novoChatBD.ID_USUARIO_1 = idUsuarioLogado;
                    novoChatBD.ID_USUARIO_2 = idUsuario;
                    novoChatBD.FL_ATIVO_USR_1 = true;
                    novoChatBD.FL_ATIVO_USR_2 = true;
                    dbDiceHaven.tb_chats.Add(novoChatBD);
                }
                else
                {
                    if (idUsuarioLogado == chat.ID_USUARIO_1 && chat.FL_ATIVO_USR_1 == false)
                        chat.FL_ATIVO_USR_1 = true;
                    else if (idUsuarioLogado == chat.ID_USUARIO_2 && chat.FL_ATIVO_USR_2 == false)
                        chat.FL_ATIVO_USR_2 = true;
                    else
                        throw new HttpDiceExcept("Voce já possui um chat com esse usuário!", HttpStatusCode.InternalServerError);
                }
                dbDiceHaven.SaveChanges();
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao iniciar chat. Message: {ex.Message}", HttpStatusCode.InternalServerError);

            }
        }

        public void RemoverChat(int idUsuarioLogado, int idUsuario)
        {
            try
            {
                tb_chat chat = dbDiceHaven.tb_chats.Where(x => (x.ID_USUARIO_1 == idUsuarioLogado && x.ID_USUARIO_2 == idUsuario) ||
                                                             (x.ID_USUARIO_2 == idUsuario && x.ID_USUARIO_1 == idUsuarioLogado)).FirstOrDefault();
                if (chat is null)
                    throw new HttpDiceExcept("O chat informado não existe", HttpStatusCode.InternalServerError);
                else
                {
                    if (idUsuarioLogado == chat.ID_USUARIO_1)
                        chat.FL_ATIVO_USR_1 = false;
                    else if (idUsuarioLogado == chat.ID_USUARIO_2)
                        chat.FL_ATIVO_USR_2 = false;
                }
                dbDiceHaven.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao iniciar chat. Message: {ex.Message}", HttpStatusCode.InternalServerError);

            }
        }

        public List<MensagemDTO> ListarMensagensChat(int idChat, int idUsuarioLogado)
        {
            try
            {
                tb_chat chat = dbDiceHaven.tb_chats.Where(x => x.ID_CHAT == idChat && (x.ID_USUARIO_1 == idUsuarioLogado || x.ID_USUARIO_2 == idUsuarioLogado)).FirstOrDefault();
                if (chat is not null)
                {
                    List<MensagemDTO> lstMensagens = (from mc in dbDiceHaven.tb_chat_mensagems
                                                      where mc.FL_ATIVA == true
                                                      select new MensagemDTO
                                                      {
                                                          ID_CHAT_MENSAGEM = mc.ID_CHAT_MENSAGEM,
                                                          DS_MENSAGEM = mc.DS_MENSAGEM,
                                                          DT_DATA_ENVIO = mc.DT_DATA_ENVIO,
                                                          FL_EDITADA = mc.FL_EDITADA,
                                                          DS_LINK_IMAGEM = mc.DS_LINK_IMAGEM,
                                                          ID_USUARIO = mc.ID_USUARIO,
                                                          FL_ATIVA = mc.FL_ATIVA,
                                                          FL_VISUALIZADA = mc.FL_VISUALIZADA,
                                                          ID_CHAT = mc.ID_CHAT
                                                      }).ToList();
                    return lstMensagens;
                }
                else
                    throw new HttpDiceExcept("Não foi possivel encontrar o chat.", HttpStatusCode.InternalServerError);

            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao listar mensagens do chat. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public void EnviarMensagem(MensagemDTO novaMensagem, int idUsuarioLogado)
        {
            try
            {
                tb_chat chat = dbDiceHaven.tb_chats.Where(x => x.ID_CHAT == novaMensagem.ID_CHAT && 
                                                         (x.ID_USUARIO_1 == idUsuarioLogado || x.ID_USUARIO_2 == idUsuarioLogado)).FirstOrDefault();
                if(chat is not null)
                {
                    tb_chat_mensagem novaMensagemBD = new tb_chat_mensagem();
                    novaMensagemBD.DS_MENSAGEM = novaMensagem.DS_MENSAGEM;
                    novaMensagemBD.DT_DATA_ENVIO = DateTime.Now;
                    novaMensagemBD.FL_EDITADA = false;
                    novaMensagemBD.DS_LINK_IMAGEM = novaMensagem.DS_LINK_IMAGEM;
                    novaMensagemBD.ID_USUARIO = idUsuarioLogado;
                    novaMensagemBD.FL_ATIVA = true;
                    novaMensagemBD.FL_VISUALIZADA = false;
                    novaMensagemBD.ID_CHAT = novaMensagem.ID_CHAT;

                    dbDiceHaven.tb_chat_mensagems.Add(novaMensagemBD);
                    dbDiceHaven.SaveChanges();

                    //Implementar lógica de real time, provavelmente será feito usando Firebase.
                }
                else
                    throw new HttpDiceExcept($"Você não tem permissão para enviar mensagens nesse chat.", HttpStatusCode.InternalServerError);


            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao enviar mensagem. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public void EditarMensagem(MensagemDTO mensagemEditada, int idUsuarioLogado)
        {
            try
            {
                tb_chat_mensagem mensagem = dbDiceHaven.tb_chat_mensagems.Find(mensagemEditada.ID_CHAT_MENSAGEM);
                if (mensagem.ID_USUARIO == idUsuarioLogado)
                {
                    mensagem.DS_MENSAGEM = mensagemEditada.DS_MENSAGEM;
                    mensagem.DS_LINK_IMAGEM = mensagemEditada.DS_LINK_IMAGEM;
                    mensagem.FL_EDITADA = true;
                    dbDiceHaven.SaveChanges();
                    //Implementar lógica de real time, provavelmente será feito usando Firebase.

                }
                else
                    throw new HttpDiceExcept($"Você não tem permissão para editar essa mensagem.", HttpStatusCode.InternalServerError);


            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao editar mensagem. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public void DesativarMensagem(int idChatMensagem, int idUsuarioLogado)
        {
            try
            {
                tb_chat_mensagem mensagem = dbDiceHaven.tb_chat_mensagems.Find(idChatMensagem);
                if(mensagem.ID_USUARIO == idUsuarioLogado)
                {
                    mensagem.FL_ATIVA = false;
                    dbDiceHaven.SaveChanges();
                    //Implementar lógica de real time, provavelmente será feito usando Firebase.

                }
                else
                    throw new HttpDiceExcept($"Você não tem permissão para desativar essa mensagem.", HttpStatusCode.InternalServerError);

            }
            catch(HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao editar mensagem. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
    }
}
