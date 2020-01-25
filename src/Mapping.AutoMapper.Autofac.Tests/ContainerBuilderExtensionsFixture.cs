using System.Collections.Generic;
using Autofac;
using AutoMapper;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace BigSolution.Infra.Mapping
{
    public class ContainerBuilderExtensionsFixture
    {
        [Fact]
        public void ConfigurationProviderResolved()
        {
            _containerBuilder.RegisterAutoMapper();
            var container = _containerBuilder.Build();

            container.Resolve<IConfigurationProvider>().Should().NotBeNull()
                .And.BeOfType<MapperConfiguration>();
        }

        [Fact]
        public void MapperResolved()
        {
            _containerBuilder.RegisterAutoMapper();

            var container = _containerBuilder.Build();
            container.Resolve<IMapper>().Should().NotBeNull()
                .And.BeOfType<Mapper>();
        }

        [Fact]
        public void ProfilesRegistered()
        {
            _containerBuilder.RegisterProfiles(GetType().Assembly);

            var container = _containerBuilder.Build();
            var profiles = container.Resolve<IEnumerable<Profile>>();
            profiles.Should().ContainSingle().And.AllBeOfType<FakeProfile>();
        }

        [Fact]
        public void RegisterAutoMapperSucceeds()
        {
            _containerBuilder.RegisterAutoMapper();
            var container = _containerBuilder.Build();

            container.IsRegistered(typeof(IMapper<string, string>)).Should().BeTrue();
            container.IsRegistered<IConfigurationProvider>().Should().BeTrue();
            container.IsRegistered<IMapper>().Should().BeTrue();
        }

        private readonly ContainerBuilder _containerBuilder = new ContainerBuilder();

        [UsedImplicitly]
        private sealed class FakeProfile : Profile { }
    }
}
