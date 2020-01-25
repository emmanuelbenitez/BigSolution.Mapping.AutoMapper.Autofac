using System;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using AutoMapper;

namespace BigSolution.Infra.Mapping
{
    public static class RegistrationExtensions
    {
        public static IRegistrationBuilder<TLimit, TScanningActivatorData, TRegistrationStyle>
            IsProfile<TLimit, TScanningActivatorData, TRegistrationStyle>(
                this IRegistrationBuilder<TLimit, TScanningActivatorData, TRegistrationStyle> registration)
            where TScanningActivatorData : ScanningActivatorData
        {
            Requires.NotNull(registration, nameof(registration));
            return registration.Where(t => t.Is<Profile>());
        }
    }
}
