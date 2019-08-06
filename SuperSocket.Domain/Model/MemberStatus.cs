
namespace SuperSocket.Domain.Model
{
    using System;
    using SuperSocket.WebSocket;

    public class MemberStatus
    {
        public static MemberStatus GenerateInstance(WebSocketSession session)
        {
            return new MemberStatus()
            {
                MemberId = null,
                Session = session,
                CreateDateTime = DateTime.Now
            };
        }

        public int? MemberId { get; set; }

        public WebSocketSession Session { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
