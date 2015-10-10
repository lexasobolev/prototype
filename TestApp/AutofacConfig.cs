using Autofac;
using Autofac.Features.Variance;
using Events;
using Events.Services;
using Events.Services.Implementation;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Logging.Data;
using Infrastructure.Logging.Services;
using Infrastructure.Security;
using MyTests.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public static class AutofacConfig
    {
        //public static dependency
        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new ContravariantRegistrationSource());

            var anchors = new List<Type>
            {
                typeof(AutofacConfig), // UI.Web
                typeof(Event), // Events
                typeof(IUnitOfWork), // Infastruture
                typeof(LogContext), // Infastruture.Logging.Data
                typeof(ITestEvent), 

            };

            anchors.ForEach(anchor =>
            {
                builder.RegisterAssemblyTypes(anchor.Assembly)
                    .Where(t => t.Namespace != null && (
                        t.Namespace.EndsWith("Services.Implementation") ||
                        t.Name.EndsWith("Context")))
                    .AsImplementedInterfaces();
            });

            builder.RegisterType<ServiceProvider>()
                .AsImplementedInterfaces();

            var container = builder.Build();
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            Event.Subscribe(container.Resolve<IEventDispatcher>());
        }
    }

    class ServiceProvider : IServiceProvider
    {
        public ServiceProvider(IComponentContext context)
        {
            Context = context;
        }

        public IComponentContext Context { get; }

        public object GetService(Type serviceType)
        {
            return Context.Resolve(serviceType);
        }
    }
}
