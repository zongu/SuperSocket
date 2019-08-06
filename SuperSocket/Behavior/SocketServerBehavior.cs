
namespace SuperSocket.Behavior
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Autofac;
    using Hangfire;
    using Newtonsoft.Json;
    using SuperSocket.Domain.Model;
    using SuperSocket.Domain.Repository;
    using SuperSocket.SocketBase;
    using SuperSocket.WebSocket;

    public class SocketServerBehavior : ISocketServerBehavior
    {
        public SocketServerBehavior(int port) : base(port)
        {
        }

        public override void Broadcast(string data)
        {
            using (var scope = Applibs.AutofacConfig.Container.BeginLifetimeScope())
            {
                var repo = scope.Resolve<ISessionRepository>();
                var sessions = repo.GetAllSessions();

                //// 平行處裡每個client廣播
                Parallel.ForEach(sessions, session =>
                {
                    session.Send(data);
                });
            }
        }

        public override void DataReceived(WebSocketSession session, byte[] value)
        {
            using (var scope = Applibs.AutofacConfig.Container.BeginLifetimeScope())
            {
                var repo = scope.Resolve<ISessionRepository>();
                repo.UpdateSession(session);

                var inputStr = Encoding.UTF8.GetString(value);
                Console.WriteLine($"DataReceived: {inputStr}");

                var request = JsonConvert.DeserializeObject<WebSocketRequest>(inputStr);
                var process = scope.ResolveKeyed<ICommandProcess>(request.Command);
                process.currentSession = session;
                process.commandString = request.Data;
                process.Excute();
            }
        }

        public override void Disconnected(WebSocketSession session, CloseReason value)
        {
            using (var scope = Applibs.AutofacConfig.Container.BeginLifetimeScope())
            {
                //// Server主動T人就不用清記憶體
                if (value != CloseReason.ServerClosing)
                {
                    var repo = scope.Resolve<ISessionRepository>();
                    var memberId = repo.RemoveAndGetMemberId(session);
                    Console.WriteLine($"MemberId: {memberId} Disconnected");
                }
            }
        }

        public override void Dispose()
        {
            this.webSocketServer.Stop();
            this.webSocketServer.Dispose();
        }

        public override void MessageReceived(WebSocketSession session, string value)
        {
            using (var scope = Applibs.AutofacConfig.Container.BeginLifetimeScope())
            {
                var repo = scope.Resolve<ISessionRepository>();
                repo.UpdateSession(session);

                Console.WriteLine($"MessageReceived: {value}");

                var request = JsonConvert.DeserializeObject<WebSocketRequest>(value);
                var process = scope.ResolveKeyed<ICommandProcess>(request.Command);
                process.currentSession = session;
                process.commandString = request.Data;
                process.Excute();
            }
        }

        public override void Register(WebSocketSession session)
        {
            using (var scope = Applibs.AutofacConfig.Container.BeginLifetimeScope())
            {
                var repo = scope.Resolve<ISessionRepository>();
                if (!repo.Add(session))
                {
                    Console.Write($"SessionId: {session.SessionID} Already Regist");
                }

                BackgroundJob.Schedule(
                    () => RemoveExpiredSession(),
                    TimeSpan.FromSeconds(Applibs.ConfigHelper.WebSocketSessionExpiredSeconds));
            }
        }

        /// <summary>
        /// 移除過期Session對象
        /// </summary>
        public static void RemoveExpiredSession()
        {
            using (var scope = Applibs.AutofacConfig.Container.BeginLifetimeScope())
            {
                var repo = scope.Resolve<ISessionRepository>();
                var expriedSessions = repo.GetAndRemoveExpiredSessions();
                expriedSessions.ToList().ForEach(session =>
                {
                    Console.WriteLine($"RemoveExpiredSession SessionId:{session.SessionID} IP:{session.LocalEndPoint.Address}");
                    try
                    {
                        session.Close(CloseReason.ServerClosing);
                    }
                    //// 忽略搶資源時清除連線失敗問題
                    catch
                    {
                    }
                });
            }
        }

        public override void Send(int memberId, string data)
        {
            using (var scope = Applibs.AutofacConfig.Container.BeginLifetimeScope())
            {
                var repo = scope.Resolve<ISessionRepository>();
                var session = repo.Get(memberId);

                if (session != null)
                {
                    session.Send(data);
                }
            }
        }
    }
}
