using System;
using System.ComponentModel.DataAnnotations;

namespace votacao_backend.Api.Models
{
    public class VotacaoInclusao
    {
        public Guid IdVotacao { get; set; }
        public Guid IdGradeamento { get; set; }
        public Guid IdUsuario { get; set; }
        public int TotalVotos { get; set; }
    }
}