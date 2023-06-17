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

namespace DiceHaven_Model.Models.ControlleDeAcesso
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
                List<GrupoDTO> listaDeGrupos = (from g in dbDiceHaven.TB_GRUPOs
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
                throw new HttpDiceExcept("Ocorreu um erro na listagem de grupos", HttpStatusCode.InternalServerError);
            }
        }

        public List<UsuarioDTO> ListarUsuariosGrupo(int idGrupo)
        {
            try
            {
                List<UsuarioDTO> listaUsuarios = (from g in dbDiceHaven.TB_GRUPOs
                                                  join gu in dbDiceHaven.TB_GRUPO_USUARIOs on g.ID_GRUPO equals gu.ID_GRUPO
                                                  join u in dbDiceHaven.TB_USUARIOs on gu.ID_USUARIO equals u.ID_USUARIO
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
                throw new HttpDiceExcept("Ocorreu um erro na listagem de usuarios do grupos", HttpStatusCode.InternalServerError);
            }
        }

        public void VincularUsuarioGrupo(int idUsuario, int idGrupo)
        {
            try
            {
                TB_GRUPO Grupo = dbDiceHaven.TB_GRUPOs.Find(idGrupo);
                TB_USUARIO Usuario = dbDiceHaven.TB_USUARIOs.Find(idUsuario);                
                
                if (Grupo == null)
                    throw new HttpDiceExcept("O grupo informado não existe.", HttpStatusCode.InternalServerError);
                else if (Usuario == null)
                    throw new HttpDiceExcept("O usuário informado não existe", HttpStatusCode.InternalServerError);
                else
                {
                    TB_GRUPO_USUARIO GrupoUsuario = dbDiceHaven.TB_GRUPO_USUARIOs.Where(x => x.ID_GRUPO == idGrupo && x.ID_USUARIO == idUsuario).FirstOrDefault() ?? new TB_GRUPO_USUARIO();
                    if (GrupoUsuario.ID_GRUPO_USUARIO != 0)
                        throw new HttpDiceExcept("O usuário já está vinculado a esse grupo", HttpStatusCode.InternalServerError);
                    GrupoUsuario.ID_GRUPO = Grupo.ID_GRUPO;
                    GrupoUsuario.ID_USUARIO = Usuario.ID_USUARIO;
                    dbDiceHaven.TB_GRUPO_USUARIOs.Add(GrupoUsuario);
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

                TB_GRUPO_USUARIO GrupoUsuario = dbDiceHaven.TB_GRUPO_USUARIOs.Where(x => x.ID_GRUPO == idGrupo && x.ID_USUARIO == idUsuario).FirstOrDefault();
                if (GrupoUsuario == null)
                    throw new HttpDiceExcept("O usuário não está vinculado a esse grupo.", HttpStatusCode.InternalServerError);

                dbDiceHaven.TB_GRUPO_USUARIOs.Remove(GrupoUsuario);
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
    }
}
