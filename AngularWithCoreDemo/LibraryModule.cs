using Autofac;
using System;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace AngularWithCoreDemo
{
    public class LibraryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            // builder.RegisterType<CharacterRepository>().As<ICharacterRepository>();

            // REGISTERING TYPES BY CONVENTION
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Factory")) // REGISTERING ALL FACTORIES
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service")) // REGISTERING ALL SERVICES
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Helper")) // REGISTERING ALL HELPERS
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("IntentHandler")) // REGISTERING ALL HANDLERS
                .AsSelf();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Handler")) // REGISTERING ALL Handler
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Intent")) // REGISTERING ALL Intent
                .AsSelf();
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Api")) // REGISTERING ALL Intent
                .AsSelf();
        }
    }
}
