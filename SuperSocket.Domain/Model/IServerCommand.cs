
namespace SuperSocket.Domain.Model
{
    using System;

    public interface IServerCommand
    {
        Tuple<Exception> Excute(RequestInfo requestInfo);
    }
}
