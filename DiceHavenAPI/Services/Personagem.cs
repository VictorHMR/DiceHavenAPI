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
using DiceHaven_API.Utils;

namespace DiceHavenAPI.Services
{
    public class Personagem : IPersonagem
    {
        public DiceHavenBDContext dbDiceHaven;
        private readonly IConfiguration _configuration;
        private readonly Supabase.Client _client;

        public Personagem(DiceHavenBDContext dbDiceHaven, IConfiguration configuration, Supabase.Client client)
        {
            this.dbDiceHaven = dbDiceHaven;
            this._configuration = configuration;
            _client = client;
        }
        public Personagem(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        public PersonagemDTO ObterPersonagem(int idPersonagem)
        {
            try
            {
                PersonagemDTO personagem = (from ps in dbDiceHaven.tb_personagems
                                            where ps.ID_PERSONAGEM == idPersonagem
                                            select new PersonagemDTO
                                            {
                                                ID_PERSONAGEM = ps.ID_PERSONAGEM,
                                                DS_NOME = ps.DS_NOME,
                                                DS_FOTO = ps.DS_FOTO,
                                                DS_COR = ps.DS_COR,
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
                List<PersonagemDTO> listaPersonagens = (from ps in dbDiceHaven.tb_personagems
                                                        where ps.ID_USUARIO == idUsuario
                                                        select new PersonagemDTO
                                                        {
                                                            ID_PERSONAGEM = ps.ID_PERSONAGEM,
                                                            DS_NOME = ps.DS_NOME,
                                                            DS_FOTO = ps.DS_FOTO,
                                                            ID_USUARIO = ps.ID_USUARIO
                                                        }).ToList();
                return listaPersonagens;
            }
            catch(Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro na listagem dos personagens. Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<int> CadastrarPersonagem(PersonagemDTO novoPersonagem)
        {
            try
            {
                SupabaseStorage SupabaseStorage = new SupabaseStorage(_configuration, _client);

                bool PersonagemExiste = dbDiceHaven.tb_personagems.Where(x => x.DS_NOME == novoPersonagem.DS_NOME).Any();

                if (PersonagemExiste)
                    throw new HttpDiceExcept("Um personagem com esse nome já existe em sua lista de personagens.", HttpStatusCode.InternalServerError);

                tb_personagem novoPersonagemBD = new tb_personagem();
                novoPersonagemBD.DS_NOME = novoPersonagem.DS_NOME;
                novoPersonagemBD.DS_FOTO = await SupabaseStorage.SaveImageFromBase64(novoPersonagem.DS_FOTO, "CharacterPicture", "CharacterPictures");
                novoPersonagemBD.DS_COR = novoPersonagem.DS_COR;
                novoPersonagemBD.ID_USUARIO = novoPersonagem.ID_USUARIO;

                dbDiceHaven.tb_personagems.Add(novoPersonagemBD);
                dbDiceHaven.SaveChanges();

                return novoPersonagemBD.ID_PERSONAGEM;
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

        public async Task EditarPersonagem(PersonagemDTO personagemInfo)
        {
            try
            {
                SupabaseStorage SupabaseStorage = new SupabaseStorage(_configuration, _client);
                tb_personagem Personagem = dbDiceHaven.tb_personagems.Find(personagemInfo.ID_PERSONAGEM);

                if (Personagem is null)
                    throw new HttpDiceExcept("O personagem informado não existe.", HttpStatusCode.InternalServerError);

                Personagem.DS_NOME = personagemInfo.DS_NOME;
                Personagem.DS_COR = personagemInfo.DS_COR;
                Personagem.ID_USUARIO = personagemInfo.ID_USUARIO;
                

                if (!string.IsNullOrEmpty(personagemInfo.DS_FOTO))
                {
                    await SupabaseStorage.DeleteFile(Personagem.DS_FOTO, "CharacterPictures");
                    Personagem.DS_FOTO = await SupabaseStorage.SaveImageFromBase64(personagemInfo.DS_FOTO, "CharacterPicture", "CharacterPictures");
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
