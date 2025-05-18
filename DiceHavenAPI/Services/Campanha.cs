using DiceHavenAPI.Contexts;
using DiceHavenAPI.Models;
using DiceHavenAPI.DTOs;
using DiceHavenAPI.Interfaces;
using DiceHavenAPI.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static DiceHavenAPI.Utils.Enumeration;
using DiceHaven_API.DTOs.Response;
using DiceHaven_API.DTOs.Request;
using DiceHaven_API.DTOs;

namespace DiceHavenAPI.Services
{
    public class Campanha: ICampanha
    {
        public DiceHavenBDContext dbDiceHaven;
        private readonly IConfiguration _configuration;

        public Campanha(DiceHavenBDContext dbDiceHaven, IConfiguration configuration)
        {
            this.dbDiceHaven = dbDiceHaven;
            this._configuration = configuration;
        }
        public Campanha(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        public CampanhaDTO ObterCampanha(int idCampanha)
        {
            try
            {
                ImageService imageService = new ImageService(_configuration);

                CampanhaDTO campanha = (from c in dbDiceHaven.tb_campanhas
                                        where c.ID_CAMPANHA == idCampanha
                                        select new CampanhaDTO
                                        {
                                            ID_CAMPANHA = c.ID_CAMPANHA,
                                            DS_NOME_CAMPANHA = c.DS_NOME_CAMPANHA,
                                            DS_LORE = c.DS_LORE,
                                            DT_CRIACAO = c.DT_CRIACAO,
                                            FL_ATIVO = c.FL_ATIVO,
                                            FL_PUBLICA = c.FL_PUBLICA,
                                            DS_FOTO = imageService.GetImageAsBase64(c.DS_FOTO),
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

        public List<CampanhaDTO> ListarCampanhas(int idUsuario = 0)
        {
            try
            {
                ImageService imageService = new ImageService(_configuration);

                List<CampanhaDTO> campanhas = (from c in dbDiceHaven.tb_campanhas
                                               join uc in dbDiceHaven.tb_usuario_campanhas on c.ID_CAMPANHA equals uc.ID_CAMPANHA
                                               where (idUsuario == 0 || uc.ID_USUARIO == idUsuario)
                                                select new CampanhaDTO
                                                {
                                                    ID_CAMPANHA = c.ID_CAMPANHA,
                                                    DS_NOME_CAMPANHA = c.DS_NOME_CAMPANHA,
                                                    DS_LORE = c.DS_LORE,
                                                    DT_CRIACAO = c.DT_CRIACAO,
                                                    FL_ATIVO = c.FL_ATIVO,
                                                    FL_PUBLICA = c.FL_PUBLICA,
                                                    DS_FOTO = imageService.GetImageAsBase64(c.DS_FOTO),
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
                ImageService imageService = new ImageService(_configuration);
                tb_campanha novaCampanhaBD = new tb_campanha();
                novaCampanhaBD.DS_NOME_CAMPANHA = novaCampanha.DS_NOME_CAMPANHA;
                novaCampanhaBD.DS_LORE = novaCampanha.DS_LORE;
                novaCampanhaBD.DT_CRIACAO = DateTime.Now;
                novaCampanhaBD.FL_ATIVO = true;
                novaCampanhaBD.FL_PUBLICA = novaCampanha.FL_PUBLICA;
                novaCampanhaBD.DS_FOTO = imageService.SaveImageFromBase64(novaCampanha.DS_FOTO);
                novaCampanhaBD.ID_USUARIO_CRIADOR = idUsuarioLogado;
                novaCampanhaBD.ID_MESTRE_CAMPANHA = novaCampanha?.ID_MESTRE_CAMPANHA ?? idUsuarioLogado;

                dbDiceHaven.tb_campanhas.Add(novaCampanhaBD);
                dbDiceHaven.SaveChanges();

                if(novaCampanha.LST_USUARIOS is null || novaCampanha.LST_USUARIOS?.Count() == 0)
                    this.VincularUsuarioCampanha(novaCampanhaBD.ID_CAMPANHA, idUsuarioLogado, true);
                else
                    foreach(int usuario in novaCampanha.LST_USUARIOS)
                        this.VincularUsuarioCampanha(novaCampanhaBD.ID_CAMPANHA, usuario, usuario == idUsuarioLogado);

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
                ImageService imageService = new ImageService(_configuration);

                tb_campanha CampanhaBD = dbDiceHaven.tb_campanhas.Find(campanhaAtualizada.ID_CAMPANHA);
                if (CampanhaBD is null)
                    throw new HttpDiceExcept("A campanha informada não existe !", HttpStatusCode.InternalServerError);
                CampanhaBD.DS_NOME_CAMPANHA = campanhaAtualizada.DS_NOME_CAMPANHA;
                CampanhaBD.DS_LORE = campanhaAtualizada.DS_LORE;
                CampanhaBD.FL_ATIVO = campanhaAtualizada.FL_ATIVO ?? true;
                CampanhaBD.FL_PUBLICA = campanhaAtualizada.FL_PUBLICA;
                CampanhaBD.ID_MESTRE_CAMPANHA = campanhaAtualizada?.ID_MESTRE_CAMPANHA ?? CampanhaBD.ID_MESTRE_CAMPANHA;

                if (!string.IsNullOrEmpty(campanhaAtualizada.DS_FOTO) && campanhaAtualizada.DS_FOTO != CampanhaBD.DS_FOTO)
                {
                    imageService.DeleteImage(CampanhaBD.DS_FOTO);
                    CampanhaBD.DS_FOTO = imageService.SaveImageFromBase64(campanhaAtualizada.DS_FOTO);
                }
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

        public void VincularUsuarioCampanha(int idCampanha, int idUsuario, bool flAdmin = false)
        {
            try
            {
                tb_campanha campanha = dbDiceHaven.tb_campanhas.Find(idCampanha);
                tb_usuario usuario = dbDiceHaven.tb_usuarios.Find(idUsuario);
                tb_usuario_campanha novoVinculo = dbDiceHaven.tb_usuario_campanhas.Where(x => x.ID_USUARIO == idUsuario && x.ID_CAMPANHA == idCampanha).FirstOrDefault() ?? null;

                if (campanha is null)
                    throw new HttpDiceExcept("A campanha informada não existe!", HttpStatusCode.NotFound);
                if (usuario is null)
                    throw new HttpDiceExcept("O usuário informado não existe!", HttpStatusCode.NotFound);
                if (novoVinculo is not null)
                    throw new HttpDiceExcept("O usuário informado já está vinculado a essa campanha!", HttpStatusCode.InternalServerError);

                novoVinculo = new tb_usuario_campanha();
                novoVinculo.ID_USUARIO = usuario.ID_USUARIO;
                novoVinculo.ID_CAMPANHA = campanha.ID_CAMPANHA;
                novoVinculo.FL_ADMIN = flAdmin;
                novoVinculo.FL_MUTADO = false;
                novoVinculo.DT_ENTRADA = DateTime.Now;
                dbDiceHaven.tb_usuario_campanhas.Add(novoVinculo);
                dbDiceHaven.SaveChanges();
            }
            catch(HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao vincular usuário a campanha! Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }

        }

        public void DesvincularUsuarioCampanha(int idCampanha, int idUsuario)
        {
            try
            {
                tb_campanha campanha = dbDiceHaven.tb_campanhas.Find(idCampanha);
                tb_usuario usuario = dbDiceHaven.tb_usuarios.Find(idUsuario);
                tb_usuario_campanha vinculo = dbDiceHaven.tb_usuario_campanhas.Where(x=> x.ID_USUARIO == usuario.ID_USUARIO && x.ID_CAMPANHA == campanha.ID_CAMPANHA).FirstOrDefault();

                if (campanha is null)
                    throw new HttpDiceExcept("A campanha informada não existe!", HttpStatusCode.NotFound);
                if (usuario is null)
                    throw new HttpDiceExcept("O usuário informado não existe!", HttpStatusCode.NotFound);
                if (vinculo is null)
                    throw new HttpDiceExcept("O usuário informado não está vinculado a essa campanha!", HttpStatusCode.InternalServerError);

                int qntAdmins = dbDiceHaven.tb_usuario_campanhas.Where(x => x.ID_CAMPANHA == campanha.ID_CAMPANHA && x.FL_ADMIN).Count();
                if (vinculo.FL_ADMIN && qntAdmins == 1)
                    dbDiceHaven.tb_usuario_campanhas.Where(x => x.ID_CAMPANHA == campanha.ID_CAMPANHA).OrderBy(x => x.DT_ENTRADA).FirstOrDefault().FL_ADMIN = true;
                dbDiceHaven.tb_usuario_campanhas.Remove(vinculo);
                dbDiceHaven.SaveChanges();
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao desvincular usuário da campanha! Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }

        }

        public void AlterarAdmins(GerenciarAdminDTO gerenciarAdmin, int idUsuarioLogado)
        {
            try
            {
                tb_campanha campanha = dbDiceHaven.tb_campanhas.Find(gerenciarAdmin.IdCampanha);
                tb_usuario usuario = dbDiceHaven.tb_usuarios.Find(gerenciarAdmin.IdUsuario);
                tb_usuario_campanha usuarioLogado = dbDiceHaven.tb_usuario_campanhas.Where(x => x.ID_USUARIO == idUsuarioLogado && x.ID_CAMPANHA == campanha.ID_CAMPANHA).FirstOrDefault();
                tb_usuario_campanha vinculo = dbDiceHaven.tb_usuario_campanhas.Where(x => x.ID_USUARIO == usuario.ID_USUARIO && x.ID_CAMPANHA == campanha.ID_CAMPANHA).FirstOrDefault();

                if (campanha is null)
                    throw new HttpDiceExcept("A campanha informada não existe!", HttpStatusCode.NotFound);
                if (usuario is null)
                    throw new HttpDiceExcept("O usuário informado não existe!", HttpStatusCode.NotFound);
                if (vinculo is null)
                    throw new HttpDiceExcept("O usuário informado não está vinculado a essa campanha!", HttpStatusCode.InternalServerError);
                if(!usuarioLogado.FL_ADMIN)
                    throw new HttpDiceExcept("Você não tem permissão para executar essa ação!", HttpStatusCode.InternalServerError);

                vinculo.FL_ADMIN = gerenciarAdmin.FlAdmin;
                dbDiceHaven.SaveChanges();
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao desvincular usuário da campanha! Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public List<UsuarioBasicoDTO> ListarUsuarios(int idUsuarioLogado, int? idCampanha)
        {
            try
            {
                ImageService imageService = new ImageService(_configuration);

                List<UsuarioBasicoDTO> lstUsuarios = (from c in dbDiceHaven.tb_campanhas
                        join uc in dbDiceHaven.tb_usuario_campanhas on c.ID_CAMPANHA equals uc.ID_CAMPANHA
                        join u in dbDiceHaven.tb_usuarios on uc.ID_USUARIO equals u.ID_USUARIO
                        where c.ID_CAMPANHA == idCampanha
                        select new UsuarioBasicoDTO
                        {
                            ID_USUARIO = uc.ID_USUARIO,
                            DS_NOME = u.ID_USUARIO == idUsuarioLogado ? "Você" : u.DS_NOME,
                            DS_FOTO = imageService.GetImageAsBase64(u.DS_FOTO),
                            FL_ADMIN = uc.FL_ADMIN
                        }).ToList();

                if(lstUsuarios is null || lstUsuarios.Count() == 0)
                {
                    lstUsuarios = (from u in dbDiceHaven.tb_usuarios
                                   where u.ID_USUARIO == idUsuarioLogado
                                   select new UsuarioBasicoDTO
                                   {
                                       ID_USUARIO = u.ID_USUARIO,
                                       DS_NOME = "Você",
                                       DS_FOTO = imageService.GetImageAsBase64(u.DS_FOTO),
                                       FL_ADMIN = true
                                   }).ToList();
                }

                return lstUsuarios
                           .OrderByDescending(u => u.ID_USUARIO == idUsuarioLogado)
                           .ToList();
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao `listar usuários da campanha! Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public List<PersonagemDTO> ListarPersonagens(int idCampanha)
        {
            try
            {
                ImageService imageService = new ImageService(_configuration);
                List<PersonagemDTO> lstPersonagens = (from c in dbDiceHaven.tb_campanhas
                                                      join pc in dbDiceHaven.tb_personagem_campanhas on c.ID_CAMPANHA equals pc.ID_CAMPANHA
                                                      join p in dbDiceHaven.tb_personagems on pc.ID_PERSONAGEM equals p.ID_PERSONAGEM
                                                      where c.ID_CAMPANHA == idCampanha
                                                      select new PersonagemDTO
                                                      {
                                                          ID_PERSONAGEM = p.ID_PERSONAGEM,
                                                          DS_NOME = p.DS_NOME,
                                                          DS_FOTO = imageService.GetImageAsBase64(p.DS_FOTO),
                                                          ID_USUARIO = p.ID_USUARIO,
                                                          DS_COR = p.DS_COR
                                                      }).ToList();

                return lstPersonagens;
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao listar personagens da campanha! Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public List<MensagemCampanhaDTO> ListarMensagens(int idCampanha, int quantidade, DateTime DataInicial)
        {
            try
            {
                List<MensagemCampanhaDTO> lstMensagens = (from cm in dbDiceHaven.tb_campanha_mensagens
                                                          join c in dbDiceHaven.tb_campanhas on cm.ID_CAMPANHA equals c.ID_CAMPANHA
                                                          where cm.ID_CAMPANHA == idCampanha && cm.DT_MENSAGEM < DataInicial
                                                          orderby cm.DT_MENSAGEM descending
                                                          select new MensagemCampanhaDTO
                                                          {
                                                              ID_CAMPANHA_MENSAGEM = cm.ID_CAMPANHA_MENSAGEM,
                                                              DS_MENSAGEM = cm.DS_MENSAGEM,
                                                              DT_MENSAGEM = cm.DT_MENSAGEM,
                                                              ID_USUARIO = cm.ID_USUARIO,
                                                              FL_MESTRE = cm.FL_MESTRE,
                                                              ID_CAMPANHA = cm.ID_CAMPANHA,
                                                              ID_PERSONAGEM = cm.ID_PERSONAGEM
                                                          }).Take(quantidade).ToList();

                return lstMensagens;
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao listar personagens da campanha! Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
    }
}
