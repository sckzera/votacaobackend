using System;
using System.ComponentModel.DataAnnotations;

namespace votacao_backend.Api.Models
{
    public class TotalVotacoes
    {
        public string TituloTrabalho { get; set; }

        public int TotalVotos { get; set; }
    }
}