﻿using Autofac;
using Autofac.Core;
using TaskBoard.Core.Data.Contracts;
using TaskBoard.PostgreSql.Data.Contracts;

namespace TaskBoard.Core.Data
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IUserRepository>()
                   .WithParameters(new Parameter[]
                   {
                       new ResolvedParameter((p, c) => p.ParameterType.IsAssignableTo<IConnectionFactory>(),
                           (p, c) => c.Resolve<IConnectionFactory>()),
                       new NamedParameter("connectionStringName", "Postgres")
                   })
                   .AsImplementedInterfaces().InstancePerRequest();
        }
    }
}