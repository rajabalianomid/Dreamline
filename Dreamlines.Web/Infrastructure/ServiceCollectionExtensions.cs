using Dreamlines.Common;
using Dreamlines.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dreamlines.Web.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //add Config configuration parameters
            var config = services.ConfigureStartupConfig<DreamlinesConfig>(configuration.GetSection("Dreamlines"));
            //add accessor to HttpContext
            services.AddHttpContextAccessor();
            //create, initialize and configure the engine
            var engine = EngineContext.Create();
            engine.Initialize(services);
            var serviceProvider = engine.ConfigureServices(services, configuration);
            SqlServerDataProvider.InitializeDatabase(config.DataConnectionString);
            return serviceProvider;
        }
        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            //create instance of config
            var config = new TConfig();

            //bind it to the appropriate section of configuration
            configuration.Bind(config);

            //and register it as a service
            services.AddSingleton(config);

            return config;
        }
        public static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<Context>(optionsBuilder =>
            {
                var dreamlinesConfig = services.BuildServiceProvider().GetRequiredService<DreamlinesConfig>();
                var dbContextOptionsBuilder = optionsBuilder.UseLazyLoadingProxies();
                dbContextOptionsBuilder.UseSqlServer(dreamlinesConfig.DataConnectionString);
            });
        }
    }
}
