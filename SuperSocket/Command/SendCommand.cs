
namespace SuperSocket.Command
{
    using System;
    using SuperSocket.Domain.Model;

    public class SendCommand : IClientCommand
    {
        public Tuple<Exception> Excute(SocketSession session, RequestInfo requestInfo)
        {
            try
            {
                if (!session.AlreadyLoin)
                {
                    throw new Exception($"SessionId:{session.SessionID} not login");
                }

                Console.WriteLine($"MemberId: {session.MemberId} Send:{requestInfo.Body.JsonStringDeserialize<SendModel>().Message}");
                return Tuple.Create<Exception>(null);
            }
            catch (Exception ex)
            {
                return Tuple.Create<Exception>(ex);
            }
        }
    }
}
