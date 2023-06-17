using DiceHaven_BD.Contexts;
using DiceHaven_BD.Models;
using DiceHaven_DTO.ControleDeAcesso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceHaven_Utils;
using System.Net;

namespace DiceHaven_Model.Models.ControlleDeAcesso
{
    public class Usuario
    {
        public DiceHavenBDContext dbDiceHaven;

        public Usuario(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        public int cadastrarUsuario(UsuarioDTO request)
        {
            try
            {

                if (!loginValido(request.DS_LOGIN))
                    throw new HttpDiceExcept("Usuário já existe", HttpStatusCode.Conflict);

                else if (!emailValido(request.DS_EMAIL))
                    throw new HttpDiceExcept("email já existe", HttpStatusCode.Conflict);
                else
                {
                    dbDiceHaven.Database.BeginTransaction();
                    TB_USUARIO novoUsuario = new TB_USUARIO();
                    novoUsuario.DS_NOME = request.DS_NOME;
                    novoUsuario.DT_NASCIMENTO = request.DT_NASCIMENTO;
                    novoUsuario.DS_LOGIN = request.DS_LOGIN;
                    novoUsuario.DS_SENHA = request.DS_SENHA;
                    novoUsuario.DS_EMAIL = request.DS_EMAIL?.ToLower();
                    novoUsuario.FL_ATIVO = request.FL_ATIVO;
                    novoUsuario.DT_ULTIMO_ACESSO = DateTime.Now;
                    dbDiceHaven.Add(novoUsuario);
                    dbDiceHaven.SaveChanges();

                    TB_CONFIG_USUARIO config = new TB_CONFIG_USUARIO();
                    config.ID_CONFIG_USUARIO = novoUsuario.ID_USUARIO;
                    config.FL_DARK_MODE = true;
                    dbDiceHaven.Add(config);
                    dbDiceHaven.SaveChanges();
                    dbDiceHaven.Database.CommitTransaction();

                    return novoUsuario.ID_USUARIO;
                }
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception exx)
            {
                dbDiceHaven.Database.RollbackTransaction();
                throw new HttpDiceExcept($"Ocorreu um erro ao cadastrar o usuário! Message: {exx}", HttpStatusCode.InternalServerError);
            }

        }

        public UsuarioDTO obterUsuario(int idUsuario)
        {
            UsuarioDTO usuario = (from u in dbDiceHaven.TB_USUARIOs
                                  where u.ID_USUARIO == idUsuario
                                  select new UsuarioDTO
                                  {
                                      ID_USUARIO = idUsuario,
                                      DS_NOME = u.DS_NOME,
                                      DT_NASCIMENTO = u.DT_NASCIMENTO,
                                      DS_LOGIN = u.DS_LOGIN,
                                      DS_EMAIL = u.DS_EMAIL,
                                      FL_ATIVO = u.FL_ATIVO,
                                      DT_ULTIMO_ACESSO = u.DT_ULTIMO_ACESSO
                                  }).FirstOrDefault();
            return usuario;
        }

        public bool loginValido(string login)
        {
            try
            {
                return !dbDiceHaven.TB_USUARIOs.Where(x => x.DS_NOME == login).Any();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool emailValido(string email)
        {
            try
            {
                return !dbDiceHaven.TB_USUARIOs.Where(x => x.DS_EMAIL == email).Any();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void alterarConfigUsuario(ConfigUsuarioDTO configsUsuario, int idUsuario)
        {
            try
            {
                TB_CONFIG_USUARIO configAntiga = dbDiceHaven.TB_CONFIG_USUARIOs.Find(idUsuario);
                configAntiga.FL_DARK_MODE = configsUsuario.FL_DARK_MODE;
                dbDiceHaven.SaveChanges();
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception exx)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao atualizar configurações! Message: {exx}", HttpStatusCode.InternalServerError);
            }
        }
    }
}
