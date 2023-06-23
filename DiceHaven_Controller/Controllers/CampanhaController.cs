﻿using DiceHaven_BD.Contexts;
using DiceHaven_DTO;
using DiceHaven_Model.Models;
using DiceHaven_Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace DiceHaven_Controller.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class CampanhaController : ControllerBase
    {
        private DiceHavenBDContext dbDiceHaven;
        public CampanhaController(DiceHavenBDContext dbDiceHaven)
        {
            this.dbDiceHaven = dbDiceHaven;
        }

        [ProducesResponseType(typeof(CampanhaDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Busca uma campanha", Description = "Busca uma campanha baseado no ID_CAMPANHA")]
        [HttpGet("obterCampanha")]
        public ActionResult obterCampanha(int idCampanha)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                Campanha campanhaModel = new Campanha(dbDiceHaven);

                return StatusCode(200, campanhaModel.ObterCampanha(idCampanha));
            }
            catch(HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(List<CampanhaDTO>), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Lista campanhas", Description = "Lista todas as campanhas ligadas a um usuário")]
        [HttpGet("listarCampanhas")]
        public ActionResult listarCampanhas(int idUsuario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                Campanha campanhaModel = new Campanha(dbDiceHaven);

                return StatusCode(200, campanhaModel.ListarCampanhas());
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Cadastra uma nova campanha", Description = "Cadastra uma nova campanha com as informações passadas pelo usuário")]
        [HttpPost("cadastrarCampanha")]
        public ActionResult cadastrarCampanha([FromForm]CampanhaDTO novaCampanha)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                Campanha campanhaModel = new Campanha(dbDiceHaven);

                int idCampanha = campanhaModel.CadastrarCampanha(novaCampanha, idUsuarioLogado);
                return StatusCode(200, new { Message="Campanha cadastrada com sucesso!", Id=idCampanha});
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }

        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Atualiza uma campanha", Description = "Atualiza os dados de uma campanha com informações passadas pelo usuário")]
        [HttpPut("atualizarCampanha")]
        public ActionResult atualizarCampanha(CampanhaDTO campanhaAtualizada)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();
                int idUsuarioLogado = int.Parse(claim[0].Value);
                Permissao permissaoModel = new Permissao(dbDiceHaven);
                Campanha campanhaModel = new Campanha(dbDiceHaven);

                campanhaModel.AtualizarCampanha(campanhaAtualizada);
                return StatusCode(200, new { Message = "Campanha atualizada com sucesso!"});
            }
            catch (HttpDiceExcept ex)
            {
                return StatusCode((int)ex.CodeStatus, new { ex.Message });
            }
        }
    }
}
