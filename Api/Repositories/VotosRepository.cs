using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using votacao_backend.Api.DbContexts;
using votacao_backend.Api.Entities;
using votacao_backend.Api.Models;

namespace votacao_backend.Api.Repositories
{
    public class VotosRepository : IVotosRepository, IDisposable
    {
         private readonly VotacaoContext _context;

        public VotosRepository(VotacaoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

         public bool Salvar()
        {
            return (_context.SaveChanges() >= 0);
        }
          public void CadastrarVotos(Votos votos)
        {
            if (votos == null)
            {
                throw new ArgumentNullException(nameof(votos));
            }
            
            var existeVotos = ObterVotos(votos.IdGradeamento);

            if(existeVotos != null){
                existeVotos.TotalVotos++;
                votos.TotalVotos = existeVotos.TotalVotos;
                votos.IdVotos = existeVotos.IdVotos;

                _context.Update(existeVotos).CurrentValues.SetValues(votos); 
            }else{
                votos.TotalVotos++;
                votos.IdVotos = Guid.NewGuid();

                _context.Votos.Add(votos);
            } 
        }

        public Votos ObterVotos(Guid codigo)
        {
             if (codigo == Guid.Empty)
                throw new ArgumentNullException(nameof(codigo));

            return _context.Votos
                .Where(c => c.IdGradeamento == codigo)
                .FirstOrDefault();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<Votos> RankingTrabalhos()
        {
            List<Votos> votos = new List<Votos>();

            var consulta = (from v in _context.Votos
                           select v).Distinct().OrderByDescending(x => x.TotalVotos);

            if(consulta == null)
            return null;

            foreach (var item in consulta)
            {
                votos.Add(item);
            }
            
            return votos;
        }
    }
}