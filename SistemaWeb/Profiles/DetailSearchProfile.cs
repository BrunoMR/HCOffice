using Nest;

namespace SistemaWeb.Profiles
{
    using Extensions;
    using DTOLayer.Indexes;
    using DTOLayer;
    using ViewModels;
    using AutoMapper;

    public class DetailSearchProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<DetailResultSearch, DetailSearchViewModel>();

            this.CreateMap<ProcessoIndex, DetailProcessoViewModel>()
                .ForMember(a => a.Classe, b => b.MapFrom(c => c.BuildClass()))
                .ForMember(a => a.Cfe4, b => b.MapFrom(c => c.BuildCfe4()))
                .ForMember(a => a.Cfe4Description, b => b.MapFrom(c => c.BuildDescriptionCfe4()));

            this.CreateMap<Citacao, CitacaoViewModel>()
                .ForMember(a => a.Classe, b => b.MapFrom(c => c.BuildClass()));
        }
    }
}