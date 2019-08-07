
namespace SuperSocket.Compoment
{
    using System;
    using System.Timers;
    using SuperSocket.Behavior;

    public class ServerProcessCompoment : IProcessCompoment
    {
        private SocketServerBehavior server;

        private Timer timer;

        public ServerProcessCompoment()
        {
            this.server = new SocketServerBehavior();
            this.timer.Elapsed += TimeEventHandler;
            this.timer.AutoReset = true;
            this.timer.Interval = Applibs.ConfigHelper.ServerRemoveExpiredInterval * 1000;
        }

        private void TimeEventHandler(object sender, ElapsedEventArgs e)
            => this.server.RemoveExpiredAndNotRegisterSession();

        public void Start()
        {
            this.server.Start();
            this.timer.Start();

            string str = Console.ReadLine();
            while(str != "exit")
            {
                this.server.Broadcast(str);
                str = Console.ReadLine();
            }
        }

        public void Stop()
        {
            this.timer.Stop();
            this.timer.Dispose();
            this.server.Stop();
            this.server.Dispose();
        }
    }
}
