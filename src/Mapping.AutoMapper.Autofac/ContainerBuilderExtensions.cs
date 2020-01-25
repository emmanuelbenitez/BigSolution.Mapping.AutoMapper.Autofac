using System.Collections.Generic;
using System.Reflection;
using Autofac;
using AutoMapper;

namespace BigSolution.Infra.Mapping
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterAutoMapper(this ContainerBuilder builder)
        {
            Requires.NotNull(builder, nameof(builder));

            builder.RegisterGeneric(typeof(AutoMapperMapper<,>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register(
                    context => {
                        var profiles = context.Resolve<IEnumerable<Profile>>();
                        return new MapperConfiguration(config => config.AddProfiles(profiles));
                    })
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.Register(
                    ctx => {
                        var scope = ctx.Resolve<ILifetimeScope>();
                        return new Mapper(
                            ctx.Resolve<IConfigurationProvider>(),
                            scope.Resolve);
                    })
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }

        public static void RegisterProfiles(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            Requires.NotNull(builder, nameof(builder));

            builder.RegisterAssemblyTypes(assemblies)
                .IsProfile()
                .As<Profile>()
                .SingleInstance();
        }
    }
}
