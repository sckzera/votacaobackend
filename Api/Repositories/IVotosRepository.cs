using System;
using System.Collections.Generic;
using votacao_backend.Api.Entities;
using votacao_backend.Api.Models;

namespace votacao_backend.Api.Repositories
{
    public interface IVotosRepository
    {
        Votos ObterVotos(Guid codigo);
        void CadastrarVotos(Votos votos);
        List<Votos> RankingTrabalhos();
        bool Salvar();   
    }
}