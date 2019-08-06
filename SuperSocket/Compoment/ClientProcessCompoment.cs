
namespace SuperSocket.Compoment
{
    using System;
    using System.Threading;
    using SuperSocket.Behavior;

    public class ClientProcessCompoment : ProcessCompoment
    {
        private SocketClientBehavior socketClientBehavior;

        public ClientProcessCompoment()
        {
            var memberId = new Random(Guid.NewGuid().GetHashCode()).Next(100000, 199999);
            this.socketClientBehavior = new SocketClientBehavior(Applibs.ConfigHelper.SocketServerAddress, memberId);
        }

        public override void Stop()
        {
            this.socketClientBehavior.Stop();
        }

        protected override void Process()
        {
            this.socketClientBehavior.Start();

            while (!this.socketClientBehavior.IsOpened)
            {
                Thread.Sleep(500);
            }

            var str = Console.ReadLine();
            while (str != "exit")
            {
                this.socketClientBehavior.Send(str);
                str = Console.ReadLine();
            }
        }
    }
}
