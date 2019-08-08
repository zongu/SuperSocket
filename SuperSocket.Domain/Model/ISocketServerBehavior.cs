
namespace SuperSocket.Domain.Model
{
    using SuperSocket.SocketBase;
    using SuperSocket.SocketBase.Protocol;

    public abstract class ISocketServerBehavior : AppServer<SocketSession, RequestInfo>
    {
        protected ISocketServerBehavior() : base(new DefaultReceiveFilterFactory<ReceiveFilter, RequestInfo>())
        {
            this.NewSessionConnected += ServerOnConnected;
            this.SessionClosed += ServerOnClosed;
            this.NewRequestReceived += ServerOnRequested;
        }

        /// <summary>
        /// 廣播
        /// </summary>
        /// <param name="message"></param>
        public abstract void Broadcast(string message);

        /// <summary>
        /// 移除尚未登入Client
        /// </summary>
        public abstract void RemoveNotRegisterSession();

        /// <summary>
        /// 發訊給某人
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="message"></param>
        public abstract void SendTo(int memberId, string message);

        /// <summary>
        /// 收到Request事件
        /// </summary>
        /// <param name="session"></param>
        /// <param name="requestInfo"></param>
        protected abstract void ServerOnRequested(SocketSession session, RequestInfo requestInfo);

        /// <summary>
        /// Client斷開事件
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        protected abstract void ServerOnClosed(SocketSession session, CloseReason value);

        /// <summary>
        /// Client連線事件
        /// </summary>
        /// <param name="session"></param>
        protected abstract void ServerOnConnected(SocketSession session);
    }
}
