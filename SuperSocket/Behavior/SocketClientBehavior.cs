
namespace SuperSocket.Behavior
{
    using System;
    using Autofac;
    using SuperSocket.ClientEngine;
    using SuperSocket.Domain.Model;
    using WebSocket4Net;

    public class SocketClientBehavior : ISocketClientBehavior
    {
        private int memberId;

        public bool IsLogin;
        
        public SocketClientBehavior(string socketAddress, int healthCheckIntervalSeconds, int memberId) : base(socketAddress, healthCheckIntervalSeconds)
        {
            this.memberId = memberId;
            IsLogin = false;
        }

        public override void Send(string message)
        {
            var requestData = KeyType.Send.GetRequestData(new SendModel() { Message = message });
            this.client.Send(requestData, 0, requestData.Length);
        }

        protected override void HealthCheck()
        {
            var requestData = KeyType.HealthCheck.GetRequestData(new HealthCheckModel());
            this.client.Send(requestData, 0, requestData.Length);
        }

        protected override void ClientOnClosed(object sender, EventArgs e)
        {
            Console.WriteLine("Server Closed");
        }

        protected override void ClientOnDataReceived(object sender, DataReceivedEventArgs e)
        {
            var filter = new ReceiveFilter();
            var info = filter.Filter(e.Data, 0, e.Data.Length, true, out int rest);

            using (var scope = Applibs.AutofacConfig.Container.BeginLifetimeScope())
            {
                var cmd = scope.ResolveKeyed<IServerCommand>(info.Type);
                var result = cmd.Excute(info);
                if (result.Item1 != null)
                {
                    Console.WriteLine($"Command:{info.Type} Content:{info.Body} Execute Fail:{result.Item1.Message}");
                }

                if (!this.timer.Enabled && info.Type == KeyType.LoginSuccess)
                {
                    //// 啟動心跳包
                    this.timer.Start();
                    IsLogin = true;
                }
            }
        }

        protected override void ClientOnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"OnError Exception:{e.Exception.Message}");
        }

        protected override void ClientOnOpened(object sender, EventArgs e)
        {
            Console.WriteLine("Server Opened");
            var requestData = KeyType.Login.GetRequestData(new LoginModel() { MemberId = this.memberId });
            this.client.Send(requestData, 0, requestData.Length);
        }
    }
}
