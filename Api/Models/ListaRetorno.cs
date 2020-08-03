using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace votacao_backend.Api.Models
{
    public class ListaRetorno
    {
        List<TotalVotacoes> resultado {get; set;}
    }
}