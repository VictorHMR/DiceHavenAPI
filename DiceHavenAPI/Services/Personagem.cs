using DiceHavenAPI.Contexts;
using DiceHavenAPI.Models;
using DiceHavenAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceHavenAPI.Utils;
using System.Net;
using Microsoft.Extensions.Configuration;
using DiceHavenAPI.Interfaces;
using DiceHaven_API.DTOs.Response;

namespace DiceHavenAPI.Services
{
    public class Personagem : IPersonagem
    {
        public DiceHavenBDContext dbDiceHaven;
        private readonly IConfiguration _configuration;


        public Personagem(DiceHavenBDContext dbDiceHaven, IConfiguration configuration)
        {
            this.dbDiceHaven = dbDiceHaven;
            this._configuration = configuration;
        }
        public Personagem(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        public PersonagemDTO ObterPersonagem(int idPersonagem)
        {
            try
            {
                ImageService imageService = new ImageService(_configuration);

                PersonagemDTO personagem = (from ps in dbDiceHaven.tb_personagems
                                            where ps.ID_PERSONAGEM == idPersonagem
                                            select new PersonagemDTO
                                            {
                                                ID_PERSONAGEM = ps.ID_PERSONAGEM,
                                                DS_NOME = ps.DS_NOME,
                                                DS_FOTO = imageService.GetImageAsBase64(ps.DS_FOTO),
                                                ID_USUARIO = ps.ID_USUARIO
                                            }).FirstOrDefault();

                return personagem;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro na listagem do personagem. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public List<PersonagemDTO> ListarPersonagem(int idUsuario)
        {
            try
            {
                ImageService imageService = new ImageService(_configuration);

                List<PersonagemDTO> listaPersonagens = (from ps in dbDiceHaven.tb_personagems
                                                        where ps.ID_USUARIO == idUsuario
                                                        select new PersonagemDTO
                                                        {
                                                            ID_PERSONAGEM = ps.ID_PERSONAGEM,
                                                            DS_NOME = ps.DS_NOME,
                                                            DS_FOTO = imageService.GetImageAsBase64(ps.DS_FOTO),
                                                            ID_USUARIO = ps.ID_USUARIO
                                                        }).ToList();
                return listaPersonagens;
            }
            catch(Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro na listagem dos personagens. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public void CadastrarPersonagem(PersonagemDTO novoPersonagem)
        {
            try
            {
                ImageService imageService = new ImageService(_configuration);

                bool PersonagemExiste = dbDiceHaven.tb_personagems.Where(x => x.DS_NOME == novoPersonagem.DS_NOME).Any();

                if (PersonagemExiste)
                    throw new HttpDiceExcept("Um personagem com esse nome já existe em sua lista de personagens.", HttpStatusCode.InternalServerError);

                tb_personagem novoPersonagemBD = new tb_personagem();
                novoPersonagemBD.DS_NOME = novoPersonagem.DS_NOME;
                novoPersonagemBD.DS_FOTO = imageService.SaveImageFromBase64(novoPersonagem.DS_FOTO);
                novoPersonagemBD.ID_USUARIO = novoPersonagem.ID_USUARIO;

                dbDiceHaven.tb_personagems.Add(novoPersonagemBD);
                dbDiceHaven.SaveChanges();
            }
            catch(HttpDiceExcept ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro na criação do personagem. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }


        }

        public void EditarPersonagem(PersonagemDTO personagemInfo)
        {
            try
            {
                ImageService imageService = new ImageService(_configuration);
                tb_personagem Personagem = dbDiceHaven.tb_personagems.Find(personagemInfo.ID_PERSONAGEM);

                if (Personagem is null)
                    throw new HttpDiceExcept("O personagem informado não existe.", HttpStatusCode.InternalServerError);

                Personagem.DS_NOME = personagemInfo.DS_NOME;
                Personagem.ID_USUARIO = personagemInfo.ID_USUARIO;

                if (!string.IsNullOrEmpty(personagemInfo.DS_FOTO) && personagemInfo.DS_FOTO != Personagem.DS_FOTO)
                {
                    imageService.DeleteImage(Personagem.DS_FOTO);
                    Personagem.DS_FOTO = imageService.SaveImageFromBase64(personagemInfo.DS_FOTO);
                }

                dbDiceHaven.SaveChanges();
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro na criação do personagem. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }


        }
    }
}
