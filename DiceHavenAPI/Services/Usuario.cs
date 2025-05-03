using DiceHavenAPI.Contexts;
using DiceHavenAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceHavenAPI.Utils;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DiceHavenAPI.DTOs;
using DiceHavenAPI.Interfaces;
using DiceHaven_API.DTOs;

namespace DiceHavenAPI.Services
{
    public class Usuario : IUsuario
    {
        public DiceHavenBDContext dbDiceHaven;
        private readonly IConfiguration _configuration;

        public Usuario(DiceHavenBDContext dbDiceHaven, IConfiguration config)
        {
            this.dbDiceHaven = dbDiceHaven;
            _configuration = config;
        }
        public Usuario(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }
        public AuthTokenDTO GerarToken(UsuarioDTO usuario)
        {
            try
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, usuario.ID_USUARIO.ToString()));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiration = DateTime.UtcNow.AddDays(7);
                JwtSecurityToken token = new JwtSecurityToken(
                   issuer: null,
                   audience: null,
                   claims: claims,
                   expires: expiration,
                   signingCredentials: creds);

                return new AuthTokenDTO
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expiration
                };
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept("Ocorreu um erro ao autenticar !", HttpStatusCode.BadRequest);
            }


        }

        public UsuarioDTO Login(LoginDTO login)
        {
            try
            {
                UsuarioDTO usuario = (from u in dbDiceHaven.tb_usuarios
                                      where (u.DS_LOGIN == login.Login || u.DS_EMAIL == login.Login) && u.DS_SENHA == Conversor.HashPassword(login.Password) && u.FL_ATIVO == true
                                      select new UsuarioDTO
                                      {
                                          ID_USUARIO = u.ID_USUARIO,
                                          DS_NOME = u.DS_NOME,
                                          DT_NASCIMENTO = u.DT_NASCIMENTO,
                                          DS_LOGIN = u.DS_LOGIN,
                                          DS_EMAIL = u.DS_EMAIL,
                                          FL_ATIVO = u.FL_ATIVO,
                                          DT_ULTIMO_ACESSO = u.DT_ULTIMO_ACESSO
                                      }).FirstOrDefault();
                if (usuario is null)
                    throw new HttpDiceExcept("Ocorreu um erro ao autenticar ! Verifique suas credenciais.");

                dbDiceHaven.tb_usuarios.Find(usuario.ID_USUARIO).DT_ULTIMO_ACESSO = DateTime.Now;
                dbDiceHaven.SaveChanges();
                return usuario;

            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ! Message: {ex.Message}", HttpStatusCode.Unauthorized);
            }
        }

        public int cadastrarUsuario(UsuarioDTO request)
        {
            try
            {
                ImageService imageService = new ImageService(_configuration);

                if (!loginValido(request.DS_LOGIN))
                    throw new HttpDiceExcept("Usuário já existe", HttpStatusCode.Conflict);

                else if (!emailValido(request.DS_EMAIL))
                    throw new HttpDiceExcept("email já existe", HttpStatusCode.Conflict);
                else
                {
                    dbDiceHaven.Database.BeginTransaction();

                    tb_usuario novoUsuario = new tb_usuario();
                    novoUsuario.DS_NOME = request.DS_NOME;
                    novoUsuario.DT_NASCIMENTO = request.DT_NASCIMENTO;
                    novoUsuario.DS_LOGIN = request.DS_LOGIN;
                    novoUsuario.DS_SENHA = Conversor.HashPassword(request.DS_SENHA);
                    novoUsuario.DS_EMAIL = request.DS_EMAIL?.ToLower();
                    novoUsuario.FL_ATIVO = request.FL_ATIVO;
                    novoUsuario.DT_ULTIMO_ACESSO = DateTime.Now;
                    novoUsuario.DS_FOTO = string.IsNullOrEmpty(request.DS_FOTO) ? null: imageService.SaveImageFromBase64(request.DS_FOTO);
                    dbDiceHaven.Add(novoUsuario);
                    dbDiceHaven.SaveChanges();

                    tb_config_usuario config = new tb_config_usuario();
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

        public void alterarDadosUsuario(UsuarioDTO request)
        {
            try
            {
                ImageService imageService = new ImageService(_configuration);

                if (!loginValido(request.DS_LOGIN))
                    throw new HttpDiceExcept("Usuário já existe", HttpStatusCode.Conflict);

                else if (!emailValido(request.DS_EMAIL))
                    throw new HttpDiceExcept("email já existe", HttpStatusCode.Conflict);
                else
                {
                    dbDiceHaven.Database.BeginTransaction();

                    tb_usuario Usuario = dbDiceHaven.tb_usuarios.Find(request.ID_USUARIO);
                    Usuario.DS_LOGIN = request.DS_LOGIN ?? Usuario.DS_LOGIN;
                    Usuario.DS_EMAIL = request.DS_EMAIL?.ToLower() ?? Usuario.DS_EMAIL;
                    Usuario.DS_FOTO = !string.IsNullOrEmpty(request.DS_FOTO) ? imageService.SaveImageFromBase64(request.DS_FOTO) : Usuario.DS_FOTO;
                    Usuario.FL_ATIVO = true;
                    dbDiceHaven.SaveChanges();
                    dbDiceHaven.Database.CommitTransaction();
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
            ImageService imageService = new ImageService(_configuration);

            UsuarioDTO usuario = (from u in dbDiceHaven.tb_usuarios
                                  where u.ID_USUARIO == idUsuario
                                  select new UsuarioDTO
                                  {
                                      ID_USUARIO = idUsuario,
                                      DS_NOME = u.DS_NOME,
                                      DT_NASCIMENTO = u.DT_NASCIMENTO,
                                      DS_LOGIN = u.DS_LOGIN,
                                      DS_SENHA = u.DS_SENHA,
                                      DS_EMAIL = u.DS_EMAIL,
                                      FL_ATIVO = u.FL_ATIVO,
                                      DT_ULTIMO_ACESSO = u.DT_ULTIMO_ACESSO,
                                      DS_FOTO = string.IsNullOrEmpty(u.DS_FOTO) ? null : imageService.GetImageAsBase64(u.DS_FOTO)
                                  }).FirstOrDefault();
            return usuario;
        }

        public bool loginValido(string login)
        {
            try
            {
                return !dbDiceHaven.tb_usuarios.Where(x => x.DS_LOGIN == login).Any();
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
                return !dbDiceHaven.tb_usuarios.Where(x => x.DS_EMAIL == email).Any();
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
                tb_config_usuario configAntiga = dbDiceHaven.tb_config_usuarios.Find(idUsuario);
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
