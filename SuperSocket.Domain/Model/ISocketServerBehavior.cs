
namespace SuperSocket.Domain.Model
{
    using SuperSocket.SocketBase;
    using SuperSocket.SocketBase.Protocol;

    public abstract class ISocketServerBehavior : AppServer<SocketSession, RequestInfo>
    {
        protected ISocketServerBehavior() : base(new DefaultReceiveFilterFactory<ReceiveFilter, RequestInfo>())
        {
            this.NewSessionConnected += OnConnected;
            this.SessionClosed += OnClosed;
        }

        protected abstract void OnClosed(SocketSession session, CloseReason value);

        protected abstract void OnConnected(SocketSession session);
    }
}
