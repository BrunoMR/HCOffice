using Nest;

namespace SistemaWeb.Profiles
{
    using DTOLayer.Indexes;
    using ViewModels;
    using DTOLayer;
    using AutoMapper;
    using Extensions;

    public class ResultSearchProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<ResultSearch, ResultSearchViewModel>();

            this.CreateMap<IHit<ProcessoIndex>, ResultProcessoViewModel>()
                .ForMember(a => a.Classe, b => b.MapFrom(c => c.Source.BuildClass()))
                .ForMember(a => a.Cfe4, b => b.MapFrom(c => c.Source.BuildCfe4()))
                .ForMember(a => a.UltimoDespacho, b => b.MapFrom(c => c.Source.GetCodeLastDespacho()))
                .ForMember(a => a.DespachoDescricaoCompleta, b => b.MapFrom(c => c.Source.GetDescriptionLastDespacho()))
                .ForMember(a => a.TipoApresentacao, b => b.MapFrom(c => c.Source.GetTipoApresentacao()))
                .ForMember(a => a.Numero, b => b.MapFrom(c => c.Source.Numero))
                .ForMember(a => a.Titular, b => b.MapFrom(c => c.Source.Titular))
                .ForMember(a => a.Marca, b => b.MapFrom(c => c.Source.Marca))
                .ForMember(a => a.DataDeposito, b => b.MapFrom(c => c.Source.DataDeposito == null ? null : c.Source.DataDeposito.Value.Date.ToString("dd/MM/yyyy")))
                .ForMember(a => a.DataConcessao, b => b.MapFrom(c => c.Source.DataConcessao == null ? null : c.Source.DataConcessao.Value.Date.ToString("dd/MM/yyyy")))
                .ForMember(a => a.Especificacao, b => b.MapFrom(c => c.Source.Especificacao));

            this.CreateMap<ProcessoIndex, ResultProcessoViewModel>()
                .ForMember(a => a.Classe, b => b.MapFrom(c => c.BuildClass()))
                .ForMember(a => a.Cfe4, b => b.MapFrom(c => c.BuildCfe4()))
                .ForMember(a => a.UltimoDespacho, b => b.MapFrom(c => c.GetCodeLastDespacho()))
                .ForMember(a => a.DespachoDescricaoCompleta, b => b.MapFrom(c => c.GetDescriptionLastDespacho()))
                .ForMember(a => a.TipoApresentacao, b => b.MapFrom(c => c.GetTipoApresentacao()))
                .ForMember(a => a.Numero, b => b.MapFrom(c => c.Numero))
                .ForMember(a => a.Titular, b => b.MapFrom(c => c.Titular))
                .ForMember(a => a.Marca, b => b.MapFrom(c => c.Marca))
                .ForMember(a => a.DataDeposito, b => b.MapFrom(c => c.DataDeposito == null ? null : c.DataDeposito.Value.Date.ToString("dd/MM/yyyy")))
                .ForMember(a => a.DataConcessao, b => b.MapFrom(c => c.DataConcessao == null ? null : c.DataConcessao.Value.Date.ToString("dd/MM/yyyy")))
                .ForMember(a => a.Especificacao, b => b.MapFrom(c => c.Especificacao));
        }

    }
}