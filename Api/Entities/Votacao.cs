using System;
using System.ComponentModel.DataAnnotations;

namespace votacao_backend.Api.Entities
{
    public class Votacao
    {
        [Key]
        public Guid IdVotacao { get; set; }

        [Required]
        public Guid IdGradeamento { get; set; }

        [Required]
        public Guid IdUsuario { get; set; }

        [Required]
        public string TituloTrabalho { get; set; }
    }
}