
namespace SuperSocket.Command
{
    using System;
    using SuperSocket.Domain.Model;

    public class HealthCheckCommand : IClientCommand
    {
        public Tuple<Exception> Excute(SocketSession session, RequestInfo requestInfo)
        {
            try
            {
                if (!session.AlreadyLoin)
                {
                    throw new Exception($"SessionId:{session.SessionID} not login");
                }

                Console.WriteLine($"MemberId:{session.MemberId} health cheak {DateTime.Now}");
                return Tuple.Create<Exception>(null);
            }
            catch (Exception ex)
            {
                return Tuple.Create<Exception>(ex);
            }
        }
    }
}
