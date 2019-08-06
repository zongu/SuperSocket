
namespace SuperSocket.Domain.Model
{
    using Newtonsoft.Json;
    using SuperSocket.WebSocket;

    public abstract class ICommandProcess
    {
        public WebSocketSession currentSession;

        public string commandString;

        public abstract void Excute();
    }

    public class LoginModel
    {
        public LoginModel(int memberId)
        {
            MemberId = memberId;
        }

        public int MemberId { get; set; }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }

    public class SendModel
    {
        public SendModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}
