using DiceHaven_BD.Contexts;
using DiceHaven_DTO.ControleDeAcesso;
using DiceHaven_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using DiceHaven_BD.Models;
using static DiceHaven_Utils.Enumeration;

namespace DiceHaven_Model.Models
{
    public class Grupo
    {
        public DiceHavenBDContext dbDiceHaven;

        public Grupo(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        public List<GrupoDTO> ListarGrupos()
        {
            try
            {
                List<GrupoDTO> listaDeGrupos = (from g in dbDiceHaven.tb_grupos
                                                    //where g.FL_ADMIN == false
                                                select new GrupoDTO
                                                {
                                                    ID_GRUPO = g.ID_GRUPO,
                                                    DS_GRUPO = g.DS_GRUPO,
                                                    DS_DESCRICAO = g.DS_DESCRICAO,
                                                    FL_ADMIN = g.FL_ADMIN,
                                                }).ToList();
                return listaDeGrupos;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro na listagem de grupos. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public List<UsuarioDTO> ListarUsuariosGrupo(int idGrupo)
        {
            try
            {
                List<UsuarioDTO> listaUsuarios = (from g in dbDiceHaven.tb_grupos
                                                  join gu in dbDiceHaven.tb_grupo_usuarios on g.ID_GRUPO equals gu.ID_GRUPO
                                                  join u in dbDiceHaven.tb_usuarios on gu.ID_USUARIO equals u.ID_USUARIO
                                                  where g.ID_GRUPO == idGrupo
                                                  select new UsuarioDTO
                                                  {
                                                      ID_USUARIO = u.ID_USUARIO,
                                                      DS_NOME = u.DS_NOME,
                                                      DS_EMAIL = u.DS_EMAIL,
                                                      FL_ATIVO = u.FL_ATIVO
                                                  }).ToList();
                return listaUsuarios;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro na listagem de usuarios do grupos. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public void VincularUsuarioGrupo(int idUsuario, int idGrupo)
        {
            try
            {
                tb_grupo Grupo = dbDiceHaven.tb_grupos.Find(idGrupo);
                tb_usuario Usuario = dbDiceHaven.tb_usuarios.Find(idUsuario);

                if (Grupo is null)
                    throw new HttpDiceExcept("O grupo informado não existe.", HttpStatusCode.InternalServerError);
                else if (Usuario is null)
                    throw new HttpDiceExcept("O usuário informado não existe", HttpStatusCode.InternalServerError);
                else
                {
                    tb_grupo_usuario GrupoUsuario = dbDiceHaven.tb_grupo_usuarios.Where(x => x.ID_GRUPO == idGrupo && x.ID_USUARIO == idUsuario).FirstOrDefault() ?? new tb_grupo_usuario();
                    if (GrupoUsuario.ID_GRUPO_USUARIO != 0)
                        throw new HttpDiceExcept("O usuário já está vinculado a esse grupo", HttpStatusCode.InternalServerError);
                    GrupoUsuario.ID_GRUPO = Grupo.ID_GRUPO;
                    GrupoUsuario.ID_USUARIO = Usuario.ID_USUARIO;
                    dbDiceHaven.tb_grupo_usuarios.Add(GrupoUsuario);
                    dbDiceHaven.SaveChanges();

                }
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao vincular usuario ao grupo. Message:{ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public void DesvincularUsuarioGrupo(int idUsuario, int idGrupo)
        {
            try
            {

                tb_grupo_usuario GrupoUsuario = dbDiceHaven.tb_grupo_usuarios.Where(x => x.ID_GRUPO == idGrupo && x.ID_USUARIO == idUsuario).FirstOrDefault();
                if (GrupoUsuario is null)
                    throw new HttpDiceExcept("O usuário não está vinculado a esse grupo.", HttpStatusCode.InternalServerError);

                dbDiceHaven.tb_grupo_usuarios.Remove(GrupoUsuario);
                dbDiceHaven.SaveChanges();

            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao vincular usuario ao grupo. Message:{ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public List<GrupoDTO> ListarGrupoUsuario(int idUsuario)
        {
            try
            {
                List<GrupoDTO> ListadeGrupos = (from g in dbDiceHaven.tb_grupos
                                                join gu in dbDiceHaven.tb_grupo_usuarios on g.ID_GRUPO equals gu.ID_GRUPO
                                                join u in dbDiceHaven.tb_usuarios on gu.ID_USUARIO equals u.ID_USUARIO
                                                where u.ID_USUARIO == idUsuario
                                                select new GrupoDTO
                                                {
                                                    ID_GRUPO = g.ID_GRUPO,
                                                    DS_GRUPO = g.DS_GRUPO,
                                                    DS_DESCRICAO = g.DS_DESCRICAO,
                                                    FL_ADMIN = g.FL_ADMIN
                                                }).ToList();
                return ListadeGrupos;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao listar grupos do usuario. Message:{ex.Message}", HttpStatusCode.InternalServerError);
            }
        }


    }
}
