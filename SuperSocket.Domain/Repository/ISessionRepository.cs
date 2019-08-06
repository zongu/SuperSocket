
namespace SuperSocket.Domain.Repository
{
    using System.Collections.Generic;
    using SuperSocket.Domain.Model;
    using SuperSocket.WebSocket;

    /// <summary>
    /// WebSocketSession Repostiory
    /// * 登入流程: 1.Add -> 2.CheckIn
    /// * Add後若未做CheckIn可從GetAndRemoveExpiredSessions取得過期對象
    /// </summary>
    public interface ISessionRepository
    {
        /// <summary>
        /// 新增WebSocketSession連線對象
        /// </summary>        
        /// <param name="session">webSocketSession</param>
        /// <returns></returns>
        bool Add(WebSocketSession session);

        /// <summary>
        /// 登記該對象已被認可
        /// </summary>
        /// <param name="memberId">memberId</param>
        bool CheckIn(WebSocketSession session, int memberId);

        /// <summary>
        /// 取得並移除過期的webSocketSession對象
        /// </summary>
        /// <returns></returns>
        IEnumerable<WebSocketSession> GetAndRemoveExpiredSessions();

        /// <summary>
        /// 取得webSocketSession對象
        /// </summary>
        /// <param name="memberId">memberId</param>
        /// <returns></returns>
        WebSocketSession Get(int memberId);

        /// <summary>
        /// 取得webSocket對象
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        MemberStatus Get(WebSocketSession session);

        /// <summary>
        /// 取得所有Sessions對象
        /// </summary>
        /// <returns></returns>
        IEnumerable<WebSocketSession> GetAllSessions();

        /// <summary>
        /// 移除webSocketSession對象
        /// </summary>
        /// <param name="session">session</param>
        int? RemoveAndGetMemberId(WebSocketSession session);

        /// <summary>
        /// 更新session內容
        /// </summary>
        /// <param name="session"></param>
        void UpdateSession(WebSocketSession session);
    }
}
