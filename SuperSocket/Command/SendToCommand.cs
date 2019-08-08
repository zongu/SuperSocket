
namespace SuperSocket.Command
{
    using System;
    using SuperSocket.Domain.Model;

    public class SendToCommand : IServerCommand
    {
        public Tuple<Exception> Excute(RequestInfo requestInfo)
        {
            try
            {
                var sendToData = requestInfo.Body.JsonStringDeserialize<SendToModel>();
                Console.WriteLine($"Server SendTo: {sendToData.Message}");

                return Tuple.Create<Exception>(null);
            }
            catch (Exception ex)
            {
                return Tuple.Create<Exception>(ex);
            }
        }
    }
}
