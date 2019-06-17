namespace SistemaWeb.Profiles
{
    using AutoMapper;
    using DTOLayer;
    using ViewModels;

    public class CustomerProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<Cliente, CustomerViewModel>()
                .ForMember(a => a.Operators, b => b.Ignore())
                .ForMember(a => a.CustomerPassword, b => b.Ignore());

            this.CreateMap<TipoPessoa, PersonTypeViewModel>();
        }
    }
}