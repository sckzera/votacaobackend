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
    [Route("votacao")]
    public class VotacaoController : ControllerBase
    {
          private const string _mensagemErroExcecao = "Ocorreu um erro inesperado";
        private readonly ILogger<VotacaoController> _logger;
        private readonly IMapper _mapper;
        private readonly IVotacaoRepository _repository;

        public VotacaoController(ILogger<VotacaoController> logger
           , IVotacaoRepository repository
           , IMapper mapper)
        {
            _logger = logger;

            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

        }

            /// <summary>
        /// Cria um novo dado de Votacao
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
        public IActionResult Create([FromBody]VotacaoInclusao votacao)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(new ErroRetorno(string.Join(", ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))));
                }

                var votacaoEntity = _mapper.Map<Votacao>(votacao);

                if(_repository.VotoDuplicado(votacaoEntity.IdUsuario, votacaoEntity.IdGradeamento))
                     return new BadRequestObjectResult(new ErroRetorno("Voce ja votou no grupo: "));
                
                _repository.Cadastrar(votacaoEntity);

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
    }
}