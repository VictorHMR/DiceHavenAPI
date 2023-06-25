using DiceHaven_BD.Contexts;
using DiceHaven_BD.Models;
using DiceHaven_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiceHaven_Utils;
using System.Net;
using static DiceHaven_Utils.Enumeration;

namespace DiceHaven_Model.Models
{
    public class Ficha
    {
        public DiceHavenBDContext dbDiceHaven;

        public Ficha(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        public List<FichaDTO> ListarFichas(int? idCampanha, int idUsuario =0)
        {
            try
            {
                    List<FichaDTO> ListaDeFichas = (from f in dbDiceHaven.tb_fichas
                                     join p in dbDiceHaven.tb_personagems on f.ID_PERSONAGEM equals p.ID_PERSONAGEM
                                     where 
                                     (f.ID_CAMPANHA == idCampanha || idCampanha == null) 
                                     &&
                                     (p.ID_USUARIO == idUsuario || idUsuario == 0)
                                     select new FichaDTO
                                     {
                                         PERSONAGEM = new PersonagemDTO
                                         {
                                             ID_PERSONAGEM = p.ID_PERSONAGEM,
                                             DS_NOME = p.DS_NOME,
                                             DS_BACKSTORY = p.DS_BACKSTORY,
                                             DS_FOTO = Conversor.ConvertToBase64(p.DS_FOTO),
                                             NR_IDADE = p.NR_IDADE,
                                             DS_GENERO = p.DS_GENERO,
                                             DS_CAMPO_LIVRE = p.DS_CAMPO_LIVRE,
                                             ID_USUARIO = p.ID_USUARIO
                                         },
                                         ID_FICHA = f.ID_FICHA,
                                         DS_TENDENCIA = f.DS_TENDENCIA,
                                         DS_SOBRE = f.DS_SOBRE,
                                         NR_XP = f.NR_XP,
                                         NR_MAX_PV = f.NR_MAX_PV,
                                         NR_MAX_PM = f.NR_MAX_PM,
                                         NR_PV = f.NR_PV,
                                         NR_PM = f.NR_PM,
                                         NR_PONTOS_HAB = f.NR_PONTOS_HAB,
                                         ATRIBUTOS = new AtributosDTO
                                         {
                                             NR_STR = f.NR_STR,
                                             NR_DEX = f.NR_DEX,
                                             NR_CON = f.NR_CON,
                                             NR_INT = f.NR_INT,
                                             NR_WIS = f.NR_WIS,
                                             NR_CHA = f.NR_CHA
                                         },
                                         ID_RACA = f.ID_RACA,
                                         ID_CLASSE = f.ID_CLASSE,
                                         ID_CAMPANHA = f.ID_CAMPANHA
                                     }).ToList();
                return ListaDeFichas;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao listar fichas! Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public FichaDTO ObterFicha(int idPersonagem, int idCampanha)
        {
            try
            {
                FichaDTO ficha = (from f in dbDiceHaven.tb_fichas
                                  join p in dbDiceHaven.tb_personagems on f.ID_PERSONAGEM equals p.ID_PERSONAGEM
                                  join c in dbDiceHaven.tb_campanhas on f.ID_CAMPANHA equals c.ID_CAMPANHA
                                  where (f.ID_PERSONAGEM == idPersonagem && c.ID_CAMPANHA == idCampanha )
                                  select new FichaDTO
                                  {
                                      PERSONAGEM = new PersonagemDTO
                                      {
                                          ID_PERSONAGEM = p.ID_PERSONAGEM,
                                          DS_NOME = p.DS_NOME,
                                          DS_BACKSTORY = p.DS_BACKSTORY,
                                          DS_FOTO = Conversor.ConvertToBase64(p.DS_FOTO),
                                          NR_IDADE = p.NR_IDADE,
                                          DS_GENERO = p.DS_GENERO,
                                          DS_CAMPO_LIVRE = p.DS_CAMPO_LIVRE,
                                          ID_USUARIO = p.ID_USUARIO
                                      },
                                      ID_FICHA = f.ID_FICHA,
                                      DS_TENDENCIA = f.DS_TENDENCIA,
                                      DS_SOBRE = f.DS_SOBRE,
                                      NR_XP = f.NR_XP,
                                      NR_MAX_PV = f.NR_MAX_PV,
                                      NR_MAX_PM = f.NR_MAX_PM,
                                      NR_PV = f.NR_PV,
                                      NR_PM = f.NR_PM,
                                      NR_PONTOS_HAB = f.NR_PONTOS_HAB,
                                      ATRIBUTOS = new AtributosDTO
                                      {
                                          NR_STR = f.NR_STR,
                                          NR_DEX = f.NR_DEX,
                                          NR_CON = f.NR_CON,
                                          NR_INT = f.NR_INT,
                                          NR_WIS = f.NR_WIS,
                                          NR_CHA = f.NR_CHA
                                      },
                                      ID_RACA = f.ID_RACA,
                                      ID_CLASSE = f.ID_CLASSE,
                                      ID_CAMPANHA = f.ID_CAMPANHA
                                  }).FirstOrDefault();
                return ficha;
            }
            catch(Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao obter ficha! Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public int CadastrarFicha(FichaDTO novaFicha)
        {
            try
            {
                tb_campanha campanha = dbDiceHaven.tb_campanhas.Find(novaFicha.ID_CAMPANHA);
                tb_personagem personagem = dbDiceHaven.tb_personagems.Find(novaFicha.ID_PERSONAGEM);
                bool temFicha = dbDiceHaven.tb_fichas.Where(x=> x.ID_CAMPANHA == novaFicha.ID_CAMPANHA && x.ID_PERSONAGEM == novaFicha.ID_PERSONAGEM).Any();

                if (campanha is null)
                    throw new HttpDiceExcept("A campanha informada não existe!", HttpStatusCode.NoContent);
                if (personagem is null)
                    throw new HttpDiceExcept("O Personagem informado não existe!", HttpStatusCode.NoContent);
                if(temFicha)
                    throw new HttpDiceExcept("O Personagem informado já possui ficha nessa campanha!", HttpStatusCode.NoContent);


                tb_ficha novaFichaBD = new tb_ficha();
                AtributosDTO atributos;
                switch ((TipoDefinicaoAtributos)campanha.NR_DEFINICAO_ATRIBUTOS)
                {
                    case TipoDefinicaoAtributos.Distribuicao:
                        atributos = novaFicha.ATRIBUTOS;
                        break;
                    case TipoDefinicaoAtributos.BaseadoEmRaca:
                        atributos = GerarAtributosRaca(novaFicha.ID_RACA);
                        break;
                    case TipoDefinicaoAtributos.BaseadoEmClasse:
                        atributos = GerarAtributosClasse(novaFicha.ID_CLASSE);
                        break;
                    case TipoDefinicaoAtributos.BaseadoEmAmbos:
                        atributos = GerarAtributosRacaClasse(novaFicha.ID_RACA, novaFicha.ID_CLASSE);
                        break;
                    default:
                        throw new HttpDiceExcept("Tipo de definição de atributos invalida!");
                }
                novaFichaBD.DS_TENDENCIA = novaFicha.DS_TENDENCIA;
                novaFichaBD.DS_SOBRE = novaFicha.DS_SOBRE;
                novaFichaBD.NR_XP = 0;
                novaFichaBD.NR_PONTOS_HAB = 0;
                novaFichaBD.NR_MAX_PV = novaFicha.NR_MAX_PV;
                novaFichaBD.NR_PV = novaFicha.NR_PV;
                novaFichaBD.NR_MAX_PM = campanha.FL_EXISTE_MAGIA ? novaFicha.NR_MAX_PM : 0;
                novaFichaBD.NR_PM = campanha.FL_EXISTE_MAGIA ? novaFicha.NR_PM : 0;
                novaFichaBD.NR_STR = atributos.NR_STR;
                novaFichaBD.NR_DEX = atributos.NR_DEX;
                novaFichaBD.NR_CON = atributos.NR_CON;
                novaFichaBD.NR_INT = atributos.NR_INT;
                novaFichaBD.NR_WIS = atributos.NR_WIS;
                novaFichaBD.NR_CHA = atributos.NR_CHA;
                novaFichaBD.ID_RACA = novaFicha.ID_RACA;
                novaFichaBD.ID_CLASSE = novaFicha.ID_CLASSE;
                novaFichaBD.ID_PERSONAGEM = personagem.ID_PERSONAGEM;
                novaFichaBD.ID_CAMPANHA = campanha.ID_CAMPANHA;

                dbDiceHaven.tb_fichas.Add(novaFichaBD);
                dbDiceHaven.SaveChanges();

                return novaFichaBD.ID_FICHA;

            }
            catch(HttpDiceExcept ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao cadastrar ficha ! Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public AtributosDTO GerarAtributosClasse(int? idClasse)
        {
            try
            {
                if (idClasse is null)
                    throw new HttpDiceExcept("É preciso definir uma classe primeiro !", HttpStatusCode.InternalServerError);
                AtributosDTO atributosClasse = (from c in dbDiceHaven.tb_classes
                                                where c.ID_CLASSE == idClasse
                                                select new AtributosDTO
                                                {
                                                    NR_STR = c.NR_STR_PADRAO,
                                                    NR_DEX = c.NR_DEX_PADRAO,
                                                    NR_CON = c.NR_CON_PADRAO,
                                                    NR_INT = c.NR_INT_PADRAO,
                                                    NR_WIS = c.NR_WIS_PADRAO,
                                                    NR_CHA = c.NR_CHA_PADRAO
                                                }).FirstOrDefault();
                return atributosClasse;
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao gerar atributos! Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public AtributosDTO GerarAtributosRaca(int? idRaca)
        {
            try
            {
                if (idRaca is null)
                    throw new HttpDiceExcept("É preciso definir uma classe primeiro !", HttpStatusCode.InternalServerError);
                AtributosDTO atributosRaca = (from r in dbDiceHaven.tb_racas
                                              where r.ID_RACA == idRaca
                                              select new AtributosDTO
                                              {
                                                  NR_STR = r.NR_STR_PADRAO,
                                                  NR_DEX = r.NR_DEX_PADRAO,
                                                  NR_CON = r.NR_CON_PADRAO,
                                                  NR_INT = r.NR_INT_PADRAO,
                                                  NR_WIS = r.NR_WIS_PADRAO,
                                                  NR_CHA = r.NR_CHA_PADRAO
                                              }).FirstOrDefault();
                return atributosRaca;
            }
            catch (HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao gerar atributos! Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public AtributosDTO GerarAtributosRacaClasse(int? idRaca, int? idClasse)
        {
            try
            {
                AtributosDTO atributosClasse = GerarAtributosClasse(idClasse);
                AtributosDTO atributosRaca = GerarAtributosRaca(idRaca);

                AtributosDTO atributosClasseRaca = new AtributosDTO
                {
                    NR_STR = (atributosClasse.NR_STR + atributosRaca.NR_STR) / 2,
                    NR_DEX = (atributosClasse.NR_DEX + atributosRaca.NR_DEX) / 2,
                    NR_CON = (atributosClasse.NR_CON + atributosRaca.NR_CON) / 2,
                    NR_INT = (atributosClasse.NR_INT + atributosRaca.NR_INT) / 2,
                    NR_WIS = (atributosClasse.NR_WIS + atributosRaca.NR_WIS) / 2,
                    NR_CHA = (atributosClasse.NR_CHA + atributosRaca.NR_CHA) / 2
                };

                return atributosClasseRaca;
            }
            catch(HttpDiceExcept ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new HttpDiceExcept($"Ocorreu um erro ao gerar atributos! Message: {ex.Message}", HttpStatusCode.InternalServerError);
            }

        }
    }
}
