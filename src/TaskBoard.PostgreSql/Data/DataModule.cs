using Autofac;
using Autofac.Core;
using TaskBoard.PostgreSql.Data.Contracts;

namespace TaskBoard.PostgreSql.Data
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NpgsqlConnectionFactory>().As<IConnectionFactory>();
        }
    }
}