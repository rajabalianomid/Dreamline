using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

namespace Dreamlines.Common
{
    public class Engine : IEngine
    {
        #region Properties

        /// <summary>
        /// Gets or sets service provider
        /// </summary>
        private IServiceProvider _serviceProvider { get; set; }

        #endregion

        #region Utilities

        /// <summary>
        /// Get IServiceProvider
        /// </summary>
        /// <returns>IServiceProvider</returns>
        protected IServiceProvider GetServiceProvider()
        {
            var accessor = ServiceProvider.GetService<IHttpContextAccessor>();
            var context = accessor.HttpContext;
            return context?.RequestServices ?? ServiceProvider;
        }

        /// <summary>
        /// Run startup tasks
        /// </summary>
        /// <param name="typeFinder">Type finder</param>
        protected virtual void RunStartupTasks(ITypeFinder typeFinder)
        {
            ////find startup tasks provided by other assemblies
            //var startupTasks = typeFinder.FindClassesOfType<IStartupTask>();

            ////create and sort instances of startup tasks
            ////we startup this interface even for not installed plugins. 
            ////otherwise, DbContext initializers won't run and a plugin installation won't work
            //var instances = startupTasks
            //    .Select(startupTask => (IStartupTask)Activator.CreateInstance(startupTask))
            //    .OrderBy(startupTask => startupTask.Order);

            ////execute tasks
            //foreach (var task in instances)
            //    task.Execute();
        }

        /// <summary>
        /// Register dependencies using Autofac
        /// </summary>
        /// <param name="DreamlinesConfig">Startup Dreamlines configuration parameters</param>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="typeFinder">Type finder</param>
        protected virtual IServiceProvider RegisterDependencies(DreamlinesConfig dreamlinesConfig, IServiceCollection services, ITypeFinder typeFinder)
        {
            var containerBuilder = new ContainerBuilder();

            //register engine
            containerBuilder.RegisterInstance(this).As<IEngine>().SingleInstance();

            //register type finder
            containerBuilder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            //find dependency registrars provided by other assemblies
            var dependencyRegistrars = typeFinder.FindClassesOfType<IDependencyRegistrar>();

            //create and sort instances of dependency registrars
            var instances = dependencyRegistrars
                //.Where(dependencyRegistrar => PluginManager.FindPlugin(dependencyRegistrar)?.Installed ?? true) //ignore not installed plugins
                .Select(dependencyRegistrar => (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar))
                .OrderBy(dependencyRegistrar => dependencyRegistrar.Order);

            //register all provided dependencies
            foreach (var dependencyRegistrar in instances)
                dependencyRegistrar.Register(containerBuilder, typeFinder, dreamlinesConfig);

            //populate Autofac container builder with the set of registered service descriptors
            containerBuilder.Populate(services);

            //create service provider
            _serviceProvider = new AutofacServiceProvider(containerBuilder.Build());
            return _serviceProvider;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize engine
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public void Initialize(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var hostingEnvironment = provider.GetRequiredService<IHostingEnvironment>();
            CommonHelper.DefaultFileProvider = new DreamlinesFileProvider(hostingEnvironment);
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            //check for assembly already loaded
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            if (assembly != null)
                return assembly;

            //get assembly from TypeFinder
            var tf = Resolve<ITypeFinder>();
            assembly = tf.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            return assembly;
        }

        /// <summary>
        /// Add and configure services
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        /// <returns>Service provider</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //find startup configurations provided by other assemblies
            var typeFinder = new WebAppTypeFinder();

            //register dependencies
            var config = services.BuildServiceProvider().GetService<DreamlinesConfig>();
            RegisterDependencies(config, services, typeFinder);
            //resolve assemblies here. otherwise, plugins can throw an exception when rendering views
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            return _serviceProvider;
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <typeparam name="T">Type of resolved service</typeparam>
        /// <returns>Resolved service</returns>
        public T Resolve<T>() where T : class
        {
            return (T)GetServiceProvider().GetRequiredService(typeof(T));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Service provider
        /// </summary>
        public virtual IServiceProvider ServiceProvider => _serviceProvider;

        #endregion
    }
}
