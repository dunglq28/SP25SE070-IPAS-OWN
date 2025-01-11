using AutoMapper;
using CapstoneProject_SP25_IPAS_Repository.IRepository;
using CapstoneProject_SP25_IPAS_Repository.Repository;
using CapstoneProject_SP25_IPAS_Repository.UnitOfWork;
using CapstoneProject_SP25_IPAS_Service.IService;
using CapstoneProject_SP25_IPAS_Service.Mapping;
using CapstoneProject_SP25_IPAS_Service.Service;

namespace CapstoneProject_SP25_IPAS_API.ProgramConfig
{
    public static class SystemServiceInstaller
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Add Mapping profiles
            var mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile<MappingProfile>();
            });

            services.AddSingleton(mapper.CreateMapper());

            // Register repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepostiory, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            // Register servicies
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMailService, MailService>();

            services.AddHttpClient();

        }
    }
}
