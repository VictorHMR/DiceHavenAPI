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
using DiceHavenAPI.Interfaces;

namespace DiceHavenAPI.Services
{
    public class Contato : IContato
    {
        public DiceHavenBDContext dbDiceHaven;

        public Contato(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        public void AdicionarContato(int idUsuario, int idUsuarioLogado)
        {
            try
            {
                dbDiceHaven.Database.BeginTransaction();
                if(idUsuario == idUsuarioLogado)
                    throw new HttpDiceExcept("Você não pode se adicionar a lista de contatos!", HttpStatusCode.Forbidden);

                if (!dbDiceHaven.tb_usuario_contatos.Where(x => x.ID_USUARIO == idUsuarioLogado && x.ID_CONTATO == idUsuario).Any())
                {
                    tb_usuario_contato novoContatoBD = new tb_usuario_contato();
                    novoContatoBD.ID_USUARIO = idUsuarioLogado;
                    novoContatoBD.ID_CONTATO = idUsuario;
                    novoContatoBD.FL_MUTADO = false;
                    dbDiceHaven.tb_usuario_contatos.Add(novoContatoBD);

                    dbDiceHaven.SaveChanges();
                    dbDiceHaven.Database.CommitTransaction();
                }
                else
                    throw new HttpDiceExcept("Usuário já existe na sua lista de contatos", HttpStatusCode.Forbidden);

            }
            catch (HttpDiceExcept ex)
            {
                dbDiceHaven.Database.RollbackTransaction();
                throw ex;
            }
            catch (Exception ex)
            {
                dbDiceHaven.Database.RollbackTransaction();
                throw new HttpDiceExcept($"Ocorreu um erro ao adicionar contato. Message: {ex.Message}", HttpStatusCode.InternalServerError);

            }
        }

        public void RemoverContato(int idUsuario, int idUsuarioLogado)
        {
            try
            {
                tb_usuario_contato contato = dbDiceHaven.tb_usuario_contatos.Where(x => x.ID_USUARIO == idUsuarioLogado && x.ID_CONTATO == idUsuario).FirstOrDefault();
                if (contato is not null)
                {
                    dbDiceHaven.tb_usuario_contatos.Remove(contato);
                    dbDiceHaven.SaveChanges();
                }
                else
                    throw new HttpDiceExcept("Usuário não existe na sua lista de contatos", HttpStatusCode.Forbidden);
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao deletar contato. Message: {ex.Message}", HttpStatusCode.InternalServerError);

            }
        }

        public List<ContatoDTO> ListarContatos(int idUsuarioLogado)
        {
            try
            {
                List<ContatoDTO> lstContatos = (from uc in dbDiceHaven.tb_usuario_contatos
                                                join u in dbDiceHaven.tb_usuarios on uc.ID_USUARIO equals u.ID_USUARIO
                                                where uc.ID_USUARIO == idUsuarioLogado
                                                select new ContatoDTO
                                                {
                                                    ID_USUARIO_CONTATO = uc.ID_USUARIO_CONTATO,
                                                    ID_USUARIO = uc.ID_USUARIO,
                                                    CONTATO = new UsuarioDTO
                                                    {
                                                        ID_USUARIO = u.ID_USUARIO,
                                                        DS_NOME = u.DS_NOME,
                                                        DT_NASCIMENTO = u.DT_NASCIMENTO,
                                                        DS_LOGIN = u.DS_LOGIN,
                                                        DS_SENHA = u.DS_SENHA,
                                                        DS_EMAIL = u.DS_EMAIL,
                                                        FL_ATIVO = u.FL_ATIVO,
                                                        DT_ULTIMO_ACESSO = u.DT_ULTIMO_ACESSO
                                                    },
                                                    FL_MUTADO = uc.FL_MUTADO
                                                }).ToList();
                return lstContatos;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao listar contatos. Message: {ex.Message}", HttpStatusCode.InternalServerError);

            }
        }

        public void MuteDesmuteContato(int idUsuarioContato, bool flMute)
        {
            try
            {
                tb_usuario_contato contato = dbDiceHaven.tb_usuario_contatos.Find(idUsuarioContato);
                if (contato is not null)
                {
                    contato.FL_MUTADO = flMute;
                    dbDiceHaven.SaveChanges();
                }
                else
                    throw new HttpDiceExcept("Usuário não existe na sua lista de contatos", HttpStatusCode.Forbidden);
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao" + (flMute ? "mutar": "desmutar") + $"contato. Message: {ex.Message}", HttpStatusCode.InternalServerError);

            }
        }


    }
}
