namespace WithAngularApp.Server.Config
{
    public static class ConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddConfig(
             this IServiceCollection services, IConfiguration config)
        {
            services.Configure<DatabaseSettings>(
                config.GetSection("Database"));
            //services.Configure<ColorOptions>(
            //	config.GetSection(ColorOptions.Color));

            return services;
        }

        /*
		public static IServiceCollection AddMyDependencyGroup(
			 this IServiceCollection services)
		{
			services.AddScoped<IMyDependency, MyDependency>();

			return services;
		}
		*/
    }
}
