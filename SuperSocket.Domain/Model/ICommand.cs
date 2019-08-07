
namespace SuperSocket.Domain.Model
{
    using System;

    public interface ICommand
    {
        Tuple<Exception> Excute(SocketSession session, RequestInfo requestInfo);
    }
}
