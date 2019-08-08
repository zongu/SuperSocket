
namespace SuperSocket.Compoment
{
    using System;
    using System.Threading;
    using SuperSocket.Behavior;

    public class ClientProcessCompoment : IProcessCompoment
    {
        private SocketClientBehavior client;

        public ClientProcessCompoment()
        {
            var memberId = new Random(Guid.NewGuid().GetHashCode()).Next(100000, 199999);
            this.client = new SocketClientBehavior(
                Applibs.ConfigHelper.SocketServerAddress,
                Applibs.ConfigHelper.HealthCheckIntervalSeconds,
                memberId);
        }

        public void Start()
        {
            this.client.Open();
            while (!this.client.IsLogin)
            {
                Thread.Sleep(500);
            }

            string str = Console.ReadLine();
            while (str != "exit")
            {
                this.client.Send(str);
                str = Console.ReadLine();
            }
        }

        public void Stop()
        {
            this.client.Close();
        }
    }
}
