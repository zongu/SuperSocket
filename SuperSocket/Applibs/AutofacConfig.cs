
namespace SuperSocket.Applibs
{
    using System.Reflection;
    using Autofac;
    using SuperSocket.Domain.Model;
    using SuperSocket.Domain.Repository;
    using SuperSocket.Model;
    using SuperSocket.Persistent;

    internal static class AutofacConfig
    {
        private static IContainer container;

        public static IContainer Container
        {
            get
            {
                if(container == null)
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

            builder.RegisterType<SessionRepository>()
                .WithParameter("expiredSeconds", ConfigHelper.WebSocketSessionExpiredSeconds)
                .As<ISessionRepository>()
                .SingleInstance();

            builder.RegisterType<LoginCommandProcess>()
                .Keyed<ICommandProcess>(CommandEnum.Login)
                .SingleInstance();

            builder.RegisterType<SendCommandProcess>()
                .Keyed<ICommandProcess>(CommandEnum.Send)
                .SingleInstance();

            container = builder.Build();
        }
    }
}
