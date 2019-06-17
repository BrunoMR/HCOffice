namespace SistemaWeb
{
    using AutoMapper;
    using Profiles;

    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<CustomerProfile>();
                cfg.AddProfile<ResultSearchProfile>();
                cfg.AddProfile<DetailSearchProfile>();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}