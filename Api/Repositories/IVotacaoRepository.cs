using System;
using System.Collections.Generic;
using votacao_backend.Api.Entities;
using votacao_backend.Api.Models;

namespace votacao_backend.Api.Repositories
{
    public interface IVotacaoRepository
    {
        Votos ObterVotos(Guid codigo);
        void Cadastrar(Votacao votacao);
        void CadastrarVotos(Votos votos);
        bool Salvar();   
        bool VotoDuplicado(Guid codigoUsuario, Guid codigoGradeamento);
        List<ListaRetorno> CountVotacoes();
    }
}