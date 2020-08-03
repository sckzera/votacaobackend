using System;
using System.ComponentModel.DataAnnotations;

namespace votacao_backend.Api.Entities
{
    public class Votos
    {
        [Key]
        public Guid IdVotos { get; set; }
        public Guid IdGradeamento { get; set; }
        public int TotalVotos { get; set; }
        public string TituloTrabalho { get; set; }
        
    }
}