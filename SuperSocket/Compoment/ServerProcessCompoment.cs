
namespace SuperSocket.Compoment
{
    using System;
    using Microsoft.Owin.Hosting;
    using SuperSocket.Behavior;

    public class ServerProcessCompoment : ProcessCompoment
    {
        private SocketServerBehavior socketBehavior;

        public ServerProcessCompoment()
        {
            WebApp.Start<Startup>(Applibs.ConfigHelper.OwinListenAddress);
            this.socketBehavior = new SocketServerBehavior(Applibs.ConfigHelper.WebSocketPort);
        }

        public override void Stop()
        {
            this.socketBehavior.Stop();
        }

        protected override void Process()
        {
            this.socketBehavior.Start();

            var str = Console.ReadLine();
            while(str != "exit")
            {
                this.socketBehavior.Broadcast(str);
                str = Console.ReadLine();
            }
        }
    }
}
