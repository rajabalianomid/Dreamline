using Autofac;
using Dreamlines.Common;
using Dreamlines.Data;
using Dreamlines.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dreamlines.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 0;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, DreamlinesConfig config)
        {
            builder.Register(context => new Context(context.Resolve<DbContextOptions<Context>>()))
                .As<IDbContext>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<SalesUnitService>().As<ISalesUnitService>().InstancePerLifetimeScope();
            builder.RegisterType<BookingService>().As<IBookingService>().InstancePerLifetimeScope();
            builder.RegisterType<DreamlinesFileProvider>().As<IDreamlinesFileProvider>().InstancePerDependency();
        }
    }
}
