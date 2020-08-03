using Microsoft.EntityFrameworkCore;
using votacao_backend.Api.Entities;

namespace votacao_backend.Api.DbContexts
{
    public class VotacaoContext : DbContext
    {
        public VotacaoContext()
        {

        }
     
        public VotacaoContext(DbContextOptions<VotacaoContext> options)
           : base(options)
        {

        }

        public DbSet<Votacao> Votacoes { get; set; }
        public DbSet<Votos> Votos { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          

            base.OnModelCreating(modelBuilder);
        }

    }
}
