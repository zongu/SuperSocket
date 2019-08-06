
namespace SuperSocket.Persistent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SuperSocket.Domain.Model;
    using SuperSocket.Domain.Repository;
    using SuperSocket.WebSocket;

    /// <summary>
    /// WebSocketSession Repostiory
    /// * 登入流程: 1.Add -> 2.CheckIn
    /// * Add後若未做CheckIn可從GetAndRemoveExpiredSessions取得過期對象
    /// </summary>
    public class SessionRepository : ISessionRepository
    {
        /// <summary>
        /// 過期秒數
        /// </summary>
        private int expiredSeconds;

        /// <summary>
        /// 所有webSocketSession對象
        /// </summary>
        public static List<MemberStatus> webSocketSessions;

        /// <summary>
        /// 程式啟動後只執行一次
        /// </summary>
        static SessionRepository()
        {   
            webSocketSessions = new List<MemberStatus>();
        }

        public SessionRepository(int expiredSeconds)
        {
            this.expiredSeconds = expiredSeconds;
        }

        /// <summary>
        /// 新增WebSocketSession連線對象
        /// </summary>
        /// <param name="memberId">memberId</param>
        /// <param name="session">webSocketSession</param>
        /// <returns></returns>
        public bool Add(WebSocketSession session)
        {
            if (webSocketSessions.Any(s => s.Session.SessionID == session.SessionID))
            {
                return false;
            }

            webSocketSessions.Add(MemberStatus.GenerateInstance(session));
            return true;
        }

        /// <summary>
        /// 登記該對象已被認可
        /// </summary>
        /// <param name="memberId">memberId</param>
        public bool CheckIn(WebSocketSession session, int memberId)
        {
            var member = webSocketSessions.FirstOrDefault(s => s.Session.SessionID == session.SessionID);
            if (member == null)
            {
                return false;
            }

            member.MemberId = memberId;
            return true;
        }

        /// <summary>
        /// 取得webSocketSession對象
        /// </summary>
        /// <returns></returns>
        public WebSocketSession Get(int memberId)
            => webSocketSessions.FirstOrDefault(s => s.MemberId == memberId)?.Session;
        
        /// <summary>
        /// 取得webSocket對象
        /// </summary>
        public MemberStatus Get(WebSocketSession session)
            => webSocketSessions.FirstOrDefault(s => s.Session.SessionID == session.SessionID);

        /// <summary>
        /// 取得所有Sessions對象
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WebSocketSession> GetAllSessions()
            => webSocketSessions.Where(s => s.MemberId != null).Select(s => s.Session);

        /// <summary>
        /// 取得並移除過期的webSocketSession對象
        /// </summary>
        /// <param name="memberId">memberId</param>
        /// <returns></returns>
        public IEnumerable<WebSocketSession> GetAndRemoveExpiredSessions()
        {
            var exporiedMembers = webSocketSessions
                .Where(s => s.MemberId == null && s.CreateDateTime < DateTime.Now.AddSeconds(-1 * this.expiredSeconds));

            webSocketSessions = webSocketSessions.Except(exporiedMembers).ToList();
            return exporiedMembers.Select(e => e.Session);
        }

        /// <summary>
        /// 移除webSocketSession對象
        /// </summary>
        /// <param name="session">session</param>
        public int? RemoveAndGetMemberId(WebSocketSession session)
        {
            var member = webSocketSessions.FirstOrDefault(s => s.Session.SessionID == session.SessionID);
            webSocketSessions = webSocketSessions.Where(s => s.Session.SessionID != session.SessionID).ToList();
            return member?.MemberId;
        }

        /// <summary>
        /// 更新session內容
        /// </summary>
        /// <param name="session"></param>
        public void UpdateSession(WebSocketSession session)
        {
            var member = webSocketSessions.FirstOrDefault(s => s.Session.SessionID == session.SessionID);
            if (member != null)
            {
                member.Session = session;
            }
        }
    }
}
