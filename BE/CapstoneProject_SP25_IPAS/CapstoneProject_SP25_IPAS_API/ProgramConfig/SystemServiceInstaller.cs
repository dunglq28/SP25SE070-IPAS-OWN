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

            services.AddHttpClient();

        }
    }
}
