using System;
using System.ComponentModel.DataAnnotations;

namespace votacao_backend.Api.Models
{
    public class VotosInclusao
    {
        public Guid IdGradeamento { get; set; }
        public int TotalVotos { get; set; }
        public string TituloTrabalho { get; set; }
    }
}