
namespace SuperSocket.Domain.Model
{
    using System;
    using SuperSocket.SocketBase;

    /// <summary>
    /// 自定義Session內容
    /// </summary>
    public class SocketSession : AppSession<SocketSession, RequestInfo>
    {
        public int? MemberId { get; set; }

        public bool AlreadyLoin
        {
            get => MemberId != null;
        }

        /// <summary>
        /// 無法解析來自client需求
        /// </summary>
        /// <param name="requestInfo"></param>
        protected override void HandleUnknownRequest(RequestInfo requestInfo)
        {
            base.HandleUnknownRequest(requestInfo);
            this.Close(CloseReason.ApplicationError);
        }

        /// <summary>
        /// 解析協議exception
        /// </summary>
        /// <param name="e"></param>
        protected override void HandleException(Exception e)
        {
            Console.WriteLine($"SocketSession HandleException: {e.Message}");
            base.HandleException(e);
            this.Close(CloseReason.ApplicationError);
        }
    }
}
