
namespace SuperSocket.Domain.Model
{
    using SuperSocket.SocketBase;
    using SuperSocket.WebSocket;

    public abstract class ISocketServerBehavior
    {
        protected WebSocketServer webSocketServer;

        public ISocketServerBehavior(int port)
        {
            this.webSocketServer = new WebSocketServer();
            this.webSocketServer.NewSessionConnected += Register;
            this.webSocketServer.NewMessageReceived += MessageReceived;
            this.webSocketServer.NewDataReceived += DataReceived;
            this.webSocketServer.SessionClosed += Disconnected;
            this.webSocketServer.Setup(port);
        }

        /// <summary>
        /// 全域廣播
        /// </summary>
        /// <param name="data"></param>
        public abstract void Broadcast(string data);

        /// <summary>
        /// 收到client訊息串流
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        public abstract void DataReceived(WebSocketSession session, byte[] value);

        /// <summary>
        /// client斷開
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        public abstract void Disconnected(WebSocketSession session, CloseReason value);

        /// <summary>
        /// 停止跟釋放資源
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// 收到client訊息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        public abstract void MessageReceived(WebSocketSession session, string value);

        /// <summary>
        /// 新Session連線要求
        /// </summary>
        /// <param name="session"></param>
        public abstract void Register(WebSocketSession session);

        /// <summary>
        /// 個別傳送
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="data"></param>
        public abstract void Send(int memberId, string data);

        public void Start()
            => this.webSocketServer.Start();

        public void Stop()
        {
            this.webSocketServer.Stop();
            this.webSocketServer.Dispose();
        }
    }
}
