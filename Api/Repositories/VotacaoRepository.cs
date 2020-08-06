using System;
using System.Collections.Generic;
using System.Linq;
using votacao_backend.Api.DbContexts;
using votacao_backend.Api.Entities;
using votacao_backend.Api.Models;

namespace votacao_backend.Api.Repositories
{
    public class VotacaoRepository : IVotacaoRepository, IDisposable
    {
         private readonly VotacaoContext _context;

        public VotacaoRepository(VotacaoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

         public bool Salvar()
        {
            return (_context.SaveChanges() >= 0);
        }

         public void Cadastrar(Votacao votacao)
        {
            if (votacao == null)
            {
                throw new ArgumentNullException(nameof(votacao));
            }

            votacao.IdVotacao = Guid.NewGuid();

            _context.Votacoes.Add(votacao);
        }

          public void CadastrarVotos(Votos votos)
        {
            if (votos == null)
            {
                throw new ArgumentNullException(nameof(votos));
            }
            
            var existeVotos = ObterVotos(votos.IdGradeamento);

            if(existeVotos != null){

                votos.TotalVotos = existeVotos.TotalVotos ++;

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

          public bool VotoDuplicado(Guid codigoUsuario, Guid codigoGradeamento)
        {
            if (codigoUsuario == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(codigoUsuario));
            }

            if (codigoGradeamento == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(codigoGradeamento));
            }

            return _context.Votacoes.Any(a => a.IdUsuario == codigoUsuario
                                        && a.IdGradeamento == codigoGradeamento);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<ListaRetorno> CountVotacoes()
        {
            List<int> countVotos = new List<int>();

            var votos = _context.Votacoes.Select(x => new {Value= x.IdGradeamento }).ToList();

            return null;    
        }

    
    }
}