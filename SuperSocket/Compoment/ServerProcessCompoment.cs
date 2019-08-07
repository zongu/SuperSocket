
namespace SuperSocket.Compoment
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Timers;
    using SuperSocket.Behavior;
    using SuperSocket.SocketBase.Config;

    public class ServerProcessCompoment : IProcessCompoment
    {
        private SocketServerBehavior server;

        private Timer timer;

        public ServerProcessCompoment()
        {
            this.server = new SocketServerBehavior();
            this.server.Setup(new ServerConfig()
            {
                Name = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName),
                //// 定時刪沒數據傳送的連接
                ClearIdleSession = true,
                ClearIdleSessionInterval = Applibs.ConfigHelper.ClearIdleSessionInterval,
                IdleSessionTimeOut = Applibs.ConfigHelper.IdleSessionTimeOut,
                Ip = Applibs.ConfigHelper.SocketIp,
                Port = Applibs.ConfigHelper.SocketPort,
                MaxConnectionNumber = Applibs.ConfigHelper.SocketMaxConnectionNumber
            });

            this.timer = new Timer();
            this.timer.Elapsed += TimeEventHandler;
            this.timer.AutoReset = true;
            this.timer.Interval = Applibs.ConfigHelper.RemoveExpiredInterval * 1000;
        }

        private void TimeEventHandler(object sender, ElapsedEventArgs e)
            => this.server.RemoveNotRegisterSession();

        public void Start()
        {
            this.server.Start();
            this.timer.Start();

            string str = Console.ReadLine();
            while (str != "exit")
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
