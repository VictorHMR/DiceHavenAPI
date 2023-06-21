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
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace DiceHaven_Model.Models.ControlleDeAcesso
{
    public class Authenticate
    {
        private readonly IConfiguration _configuration;
        public DiceHavenBDContext dbDiceHaven;

        public Authenticate(DiceHavenBDContext dbDiceHaven, IConfiguration config)
        {
            this.dbDiceHaven = dbDiceHaven;
            _configuration = config;
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

        public UsuarioDTO Login(string login, string password)
        {
            try
            {
                UsuarioDTO usuario = (from u in dbDiceHaven.tb_usuarios
                                      where (u.DS_LOGIN == login || u.DS_EMAIL == login) && u.DS_SENHA == Conversor.HashPassword(password) && u.FL_ATIVO == true
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


    }
}
