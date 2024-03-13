

    using Application.Services.EmailService;
    using AutoMapper;
    using Domain.Entitites;
using Microsoft.Extensions.DependencyInjection;

    namespace ASP.NET_Core_Template.Ioc
    {
        public static class AutoMapperConfig
        {
            public static void RegisterMappers(this IServiceCollection services, IConfiguration configuration)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Provider, Email>()
                        .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src));
                });

                IMapper mapper = config.CreateMapper(); 

                services.AddSingleton(mapper);
        }
        }
    }
