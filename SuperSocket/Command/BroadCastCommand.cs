
namespace SuperSocket.Command
{
    using System;
    using SuperSocket.Domain.Model;

    public class BroadCastCommand : IServerCommand
    {
        public Tuple<Exception> Excute(RequestInfo requestInfo)
        {
            try
            {
                var broadcastData = requestInfo.Body.JsonStringDeserialize<BroadCastModel>();
                Console.WriteLine($"Server Broadcast: {broadcastData.Message}");

                return Tuple.Create<Exception>(null);
            }
            catch (Exception ex)
            {
                return Tuple.Create<Exception>(ex);
            }
        }
    }
}
