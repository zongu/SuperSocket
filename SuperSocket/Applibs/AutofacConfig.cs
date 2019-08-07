
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

            builder.RegisterType<LoginCommand>()
                .Keyed<ICommand>(KeyType.Login)
                .SingleInstance();

            builder.RegisterType<SendCommand>()
                .Keyed<ICommand>(KeyType.Send)
                .SingleInstance();

            container = builder.Build();
        }
    }
}
