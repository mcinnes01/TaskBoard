using Autofac;
using Autofac.Core;
using TaskBoard.Core.Data.Contracts;

namespace TaskBoard.Core.Data
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NpgsqlConnectionFactory>().As<IConnectionFactory>();

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