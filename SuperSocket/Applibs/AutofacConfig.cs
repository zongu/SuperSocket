
namespace SuperSocket.Applibs
{
    using System.Reflection;
    using Autofac;
    using SuperSocket.Command;
    using SuperSocket.Domain.Model;

    internal static class AutofacConfig
    {
        private static IContainer container;

        public static IContainer Container
        {
            get
            {
                if (container == null)
                {
                    Register();
                }

                return container;
            }
        }

        private static void Register()
        {
            var builder = new ContainerBuilder();

            var asm = Assembly.GetExecutingAssembly();

            //// Client Command
            builder.RegisterType<LoginCommand>()
                .Keyed<IClientCommand>(KeyType.Login)
                .SingleInstance();

            builder.RegisterType<SendCommand>()
                .Keyed<IClientCommand>(KeyType.Send)
                .SingleInstance();

            builder.RegisterType<HealthCheckCommand>()
                .Keyed<IClientCommand>(KeyType.HealthCheck)
                .SingleInstance();

            //// Server Command
            builder.RegisterType<BroadCastCommand>()
                .Keyed<IServerCommand>(KeyType.BroadCast)
                .SingleInstance();

            builder.RegisterType<SendToCommand>()
                .Keyed<IServerCommand>(KeyType.SendTo)
                .SingleInstance();

            builder.RegisterType<LoginSuccessCommand>()
                .Keyed<IServerCommand>(KeyType.LoginSuccess)
                .SingleInstance();

            container = builder.Build();
        }
    }
}
