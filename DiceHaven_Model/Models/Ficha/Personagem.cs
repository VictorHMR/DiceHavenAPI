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
using DiceHaven_DTO.Ficha;

namespace DiceHaven_Model.Models.Ficha
{
    public class Personagem
    {
        public DiceHavenBDContext dbDiceHaven;

        public Personagem(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
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
                                                            DS_BACKSTORY = ps.DS_BACKSTORY,
                                                            DS_FOTO = Conversor.ConvertToBase64(ps.DS_FOTO),
                                                            NR_IDADE = ps.NR_IDADE,
                                                            DS_GENERO = ps.DS_GENERO,
                                                            DS_CAMPO_LIVRE = ps.DS_CAMPO_LIVRE,
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
                bool PersonagemExiste = dbDiceHaven.tb_personagems.Where(x => x.DS_NOME == novoPersonagem.DS_NOME).Any();

                if (PersonagemExiste)
                    throw new HttpDiceExcept("Um personagem com esse nome já existe em sua lista de personagens.", HttpStatusCode.InternalServerError);

                tb_personagem novoPersonagemBD = new tb_personagem();
                novoPersonagemBD.DS_NOME = novoPersonagem.DS_NOME;
                novoPersonagemBD.DS_BACKSTORY = novoPersonagem.DS_BACKSTORY;
                novoPersonagemBD.DS_FOTO = Conversor.ConvertToByteArray(novoPersonagem.DS_FOTO);
                novoPersonagemBD.NR_IDADE = novoPersonagem.NR_IDADE;
                novoPersonagemBD.DS_GENERO = novoPersonagem.DS_GENERO;
                novoPersonagemBD.DS_CAMPO_LIVRE = novoPersonagem.DS_CAMPO_LIVRE;
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
                tb_personagem Personagem = dbDiceHaven.tb_personagems.Find(personagemInfo.ID_PERSONAGEM);

                if (Personagem is null)
                    throw new HttpDiceExcept("O personagem informado não existe.", HttpStatusCode.InternalServerError);

                Personagem.DS_NOME = personagemInfo.DS_NOME;
                Personagem.DS_BACKSTORY = personagemInfo.DS_BACKSTORY;
                Personagem.DS_FOTO = Conversor.ConvertToByteArray(personagemInfo.DS_FOTO);
                Personagem.NR_IDADE = personagemInfo.NR_IDADE;
                Personagem.DS_GENERO = personagemInfo.DS_GENERO;
                Personagem.DS_CAMPO_LIVRE = personagemInfo.DS_CAMPO_LIVRE;
                Personagem.ID_USUARIO = personagemInfo.ID_USUARIO;
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
