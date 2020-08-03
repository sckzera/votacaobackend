using AutoMapper;

namespace votacao_backend.Api.Profiles
{
    public class VotacaoProfile : Profile
    {
        public VotacaoProfile()
        {
            CreateMap<Models.VotacaoInclusao, Entities.Votacao>();
        }
    }
}