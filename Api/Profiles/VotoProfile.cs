using AutoMapper;

namespace votacao_backend.Api.Profiles
{
    public class VotoProfile : Profile
    {
        public VotoProfile()
        {
            CreateMap<Models.VotosInclusao, Entities.Votos>();
        }
    }
}