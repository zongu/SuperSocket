
namespace SuperSocket.Domain.Model
{
    using System;

    public interface IClientCommand
    {
        Tuple<Exception> Excute(SocketSession session, RequestInfo requestInfo);
    }
}
