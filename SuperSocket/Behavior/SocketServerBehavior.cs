
namespace SuperSocket.Behavior
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Autofac;
    using SuperSocket.Domain.Model;
    using SuperSocket.SocketBase;

    public class SocketServerBehavior : ISocketServerBehavior
    {
        public override void Broadcast(string message)
        {
            var sessions = this.GetAllSessions().Where(s => s.AlreadyLoin);
            var messageBytes = KeyType.BroadCast.GetRequestData(new BroadCastModel() { Message = message });

            //// 平行廣播
            Parallel.ForEach(sessions, session =>
            {
                session.Send(messageBytes, 0, messageBytes.Length);
            });
        }

        public override void RemoveNotRegisterSession()
        {
            var expiredTime = DateTime.Now.AddSeconds(Applibs.ConfigHelper.ClearIdleSessionInterval);
            var expiredSessons = this.GetAllSessions()
                .Where(s => s.LastActiveTime < expiredTime || (!s.AlreadyLoin && s.StartTime < expiredTime));

            //// 平行處理T人
            Parallel.ForEach(expiredSessons, session =>
            {
                Console.WriteLine($"SessionId:{session.SessionID} MemberId:{session.MemberId} expired");
                session.Close(CloseReason.ServerClosing);
            });
        }

        public override void SendTo(int memberId, string message)
        {
            var messageBytes = KeyType.SendTo.GetRequestData(new SendToModel() { Message = message });
            var session = this.GetAllSessions().FirstOrDefault(s => s.MemberId == memberId);
            if (session != null)
            {
                session.Send(messageBytes, 0, messageBytes.Length);
            }
        }

        protected override void ServerOnClosed(SocketSession session, CloseReason value)
        {   
            Console.WriteLine($"SessionId:{session.SessionID} MemberId:{session.MemberId} Closed-{value}");
        }

        protected override void ServerOnConnected(SocketSession session)
        {   
            Console.WriteLine($"SessionId:{session.SessionID} Connected");
        }

        protected override void ServerOnRequested(SocketSession session, RequestInfo requestInfo)
        {
            using (var scope = Applibs.AutofacConfig.Container.BeginLifetimeScope())
            {
                try
                {
                    var cmd = scope.ResolveKeyed<IClientCommand>(requestInfo.Type);
                    var result = cmd.Excute(session, requestInfo);
                    if (result.Item1 != null)
                    {
                        Console.WriteLine($"Command:{requestInfo.Type} Content:{requestInfo.Body} Execute Fail:{result.Item1.Message}");
                    }
                }
                catch (Autofac.Core.Registration.ComponentNotRegisteredException)
                {
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"OnRequested Exception:{ex.Message}");
                }

            }
        }
    }
}
