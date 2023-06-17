using DiceHaven_BD.Contexts;
using DiceHaven_BD.Models;
using DiceHaven_DTO.ControleDeAcesso;
using DiceHaven_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiceHaven_Model.Models.ControlleDeAcesso
{
    public class Permissao
    {
        public DiceHavenBDContext dbDiceHaven;

        public Permissao(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }
        public List<PermissaoDTO> ListarPermissoes()
        {
            try
            {
                List<PermissaoDTO> todasPermissões = (from p in dbDiceHaven.TB_PERMISSAOs
                                                      select new PermissaoDTO
                                                      {
                                                          ID_PERMISSAO = p.ID_PERMISSAO,
                                                          DS_PERMISSAO = p.DS_PERMISSAO,
                                                          DS_DESCRICAO = p.DS_DESCRICAO
                                                      }).ToList();
                return todasPermissões;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro na listagem de permissões. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public List<PermissaoDTO> ListarPermissoesGrupo(int idGrupo)
        {
            try
            {
                List<PermissaoDTO> permissoesGrupo = (from p in dbDiceHaven.TB_PERMISSAOs
                                                      join gp in dbDiceHaven.TB_GRUPO_PERMISSAOs on p.ID_PERMISSAO equals gp.ID_PERMISSAO
                                                      join g in dbDiceHaven.TB_GRUPOs on gp.ID_GRUPO equals g.ID_GRUPO
                                                      where (g.ID_GRUPO == idGrupo || g.FL_ADMIN)
                                                      select new PermissaoDTO
                                                      {
                                                          ID_PERMISSAO = p.ID_PERMISSAO,
                                                          DS_PERMISSAO = p.DS_PERMISSAO,
                                                          DS_DESCRICAO = p.DS_DESCRICAO
                                                      }).ToList();
                return permissoesGrupo;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept("Ocorreu um erro na listagem de permissões do grupo", HttpStatusCode.InternalServerError);
            }
        }

        public List<PermissaoDTO> ListarPermissoesUsuario(int idUsuario)
        {
            try
            {
                Grupo grupoModel = new Grupo(dbDiceHaven);
                List<PermissaoDTO> permissoesUsuario;
                List<GrupoDTO> gruposUsuario = grupoModel.ListarGrupoUsuario(idUsuario);
                if (gruposUsuario.Where(x => x.FL_ADMIN).Any())
                {
                    permissoesUsuario = (from p in dbDiceHaven.TB_PERMISSAOs
                                         select new PermissaoDTO
                                         {
                                             ID_PERMISSAO = p.ID_PERMISSAO,
                                             DS_PERMISSAO = p.DS_PERMISSAO,
                                             DS_DESCRICAO = p.DS_DESCRICAO
                                         }).ToList();
                }
                else
                {
                    permissoesUsuario = (from gu in gruposUsuario
                                         join gp in dbDiceHaven.TB_GRUPO_PERMISSAOs on gu.ID_GRUPO equals gp.ID_GRUPO
                                         join p in dbDiceHaven.TB_PERMISSAOs on gp.ID_PERMISSAO equals p.ID_PERMISSAO
                                         select new PermissaoDTO
                                         {
                                             ID_PERMISSAO = p.ID_PERMISSAO,
                                             DS_PERMISSAO = p.DS_PERMISSAO,
                                             DS_DESCRICAO = p.DS_DESCRICAO
                                         }).ToList();
                }

                return permissoesUsuario;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept("Ocorreu um erro na listagem de permissões", HttpStatusCode.InternalServerError);
            }
        }

        public bool VerificaPermissaoUsuario(int idUsuario, Enumeration.Permissoes permissao)
        {
            try
            {
                List<PermissaoDTO> listaDePermissoes = ListarPermissoesUsuario(idUsuario);
                if (listaDePermissoes.Where(x => x.ID_PERMISSAO == (int)permissao).Any())
                    return true;
                else
                    throw new HttpDiceExcept("o usuário não possui permissão para realizar essa ação", HttpStatusCode.Unauthorized);
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
        }

        public void VincularPermissaoGrupo(int idGrupo, Enumeration.Permissoes permissao)
        {
            try
            {
                TB_GRUPO Grupo = dbDiceHaven.TB_GRUPOs.Find(idGrupo);
                TB_PERMISSAO Permissao = dbDiceHaven.TB_PERMISSAOs.Find((int)permissao);

                if (Grupo is null)
                    throw new HttpDiceExcept("O grupo informado não existe.", HttpStatusCode.InternalServerError);
                else if (Grupo.FL_ADMIN)
                    throw new HttpDiceExcept("O grupo já possui acesso total", HttpStatusCode.InternalServerError);
                else
                {
                    TB_GRUPO_PERMISSAO GrupoPermissao = dbDiceHaven.TB_GRUPO_PERMISSAOs.Where(x => x.ID_GRUPO == idGrupo && x.ID_PERMISSAO == (int)permissao).FirstOrDefault() ?? new TB_GRUPO_PERMISSAO();
                    if (GrupoPermissao.ID_GRUPO_PERMISSAO != 0)
                        throw new HttpDiceExcept("O grupo já possui essa permissão!", HttpStatusCode.InternalServerError);

                    GrupoPermissao.ID_GRUPO = Grupo.ID_GRUPO;
                    GrupoPermissao.ID_PERMISSAO = Permissao.ID_PERMISSAO;
                    dbDiceHaven.TB_GRUPO_PERMISSAOs.Add(GrupoPermissao);
                    dbDiceHaven.SaveChanges();
                }

            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao verificar permissão do usuario. Message:{ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public void DesvincularPermissaoGrupo(int idGrupo, Enumeration.Permissoes permissao)
        {
            try
            {
                TB_GRUPO_PERMISSAO GrupoPermissao = dbDiceHaven.TB_GRUPO_PERMISSAOs.Where(x => x.ID_GRUPO == idGrupo && x.ID_PERMISSAO == (int)permissao).FirstOrDefault();
                if (GrupoPermissao is null)
                    throw new HttpDiceExcept("O grupo não possui essa permissão vinculada.", HttpStatusCode.InternalServerError);
                dbDiceHaven.TB_GRUPO_PERMISSAOs.Remove(GrupoPermissao);
                dbDiceHaven.SaveChanges();
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao verificar permissão do usuario. Message:{ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
    }
}
