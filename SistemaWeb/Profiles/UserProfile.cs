namespace SistemaWeb.Profiles
{
    using AutoMapper;

    using Identity.Dto;

    using ViewModels;

    public class UserProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<User, UserViewModel>();
        }
    }
}