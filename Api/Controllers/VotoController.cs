using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using votacao_backend.Api.Entities;
using votacao_backend.Api.Models;
using votacao_backend.Api.Repositories;

namespace votacao_backend.Api.Controllers
{
    [ApiController]
    [Route("votos")]
    public class VotoController : ControllerBase
    {
          private const string _mensagemErroExcecao = "Ocorreu um erro inesperado";
        private readonly ILogger<VotacaoController> _logger;
        private readonly IMapper _mapper;
        private readonly IVotosRepository _repository;

        public VotoController(ILogger<VotacaoController> logger
           , IVotosRepository repository
           , IMapper mapper)
        {
            _logger = logger;

            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

        }

            /// <summary>
        /// Cria um novo voto ou incrementa
        /// </summary>
        /// <response code="201">Retorna quando um recurso foi criado com sucesso</response>
        /// <response code="400">Retorna quando houve uma requisição mal formada</response>
        /// <response code="409">Retorna quando o recurso ja existe</response>
        /// <response code="500">Retorna quando houve um erro interno do serviço</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErroRetorno))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErroRetorno))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErroRetorno))]
        [Produces("application/json")]
        public IActionResult Create([FromBody]VotosInclusao votacao)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(new ErroRetorno(string.Join(", ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))));
                }

                var votosEntity = _mapper.Map<Votos>(votacao);
                
                _repository.CadastrarVotos(votosEntity);

                if(_repository.Salvar())
                {
                    return CreatedAtRoute(null, null);
                }

                return new JsonResult(500, new ErroRetorno(_mensagemErroExcecao));
            }
            catch (Exception ex)
            {
                _logger.LogError(_mensagemErroExcecao, ex);
                return new JsonResult(500, new ErroRetorno(_mensagemErroExcecao));
            }
        }

        
        /// <summary>
        /// Consultar Ranking
        /// </summary>
        /// <response code="201">Retorna quando um recurso foi criado com sucesso</response>
        /// <response code="400">Retorna quando houve uma requisição mal formada</response>
        /// <response code="409">Retorna quando o recurso ja existe</response>
        /// <response code="500">Retorna quando houve um erro interno do serviço</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListaRetorno))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErroRetorno))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErroRetorno))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErroRetorno))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErroRetorno))]
        public IActionResult GetRanking()
        {
            try
            {
                if (!ModelState.IsValid)
                    return new BadRequestObjectResult(new ErroRetorno(string.Join(", ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))));

                    var retorno = _repository.RankingTrabalhos();

                    if(retorno == null)
                     return new BadRequestObjectResult(new ErroRetorno("Não há trabalhos cadastrados ou avaliados. "));

                return Ok(_mapper.Map<List<Votos>>(retorno));
            }
            catch (Exception ex)
            {
                _logger.LogError(_mensagemErroExcecao, ex);
                return new JsonResult(500, new ErroRetorno(_mensagemErroExcecao));
            }
        }
    }
}