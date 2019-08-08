
namespace SuperSocket.Domain.Model
{
    using System.Text;
    using Newtonsoft.Json;
    using SuperSocket.SocketBase.Protocol;

    public class RequestInfo : IRequestInfo
    {
        public RequestInfo(KeyType type, byte[] bodyBuffer)
        {
            Type = type;
            BodyBuffer = bodyBuffer;
        }

        public string Key
        {
            get => $"{Type}";
        }

        public KeyType Type { get; set; }

        public byte[] BodyBuffer { get; set; }

        public string Body
        {
            get
            {
                return Encoding.UTF8.GetString(BodyBuffer);
            }
        }
    }

    public enum KeyType
    {
        Login,
        Send,
        HealthCheck,
        BroadCast,
        SendTo,
        LoginSuccess
    }

    public class RequestModel
    {
        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }

    public class LoginModel : RequestModel
    {
        public int MemberId { get; set; }
    }

    public class SendModel : RequestModel
    {
        public string Message { get; set; }
    }

    public class HealthCheckModel : RequestModel
    {
    }

    public class BroadCastModel : RequestModel
    {
        public string Message;
    }

    public class SendToModel : RequestModel
    {
        public string Message;
    }

    public class LoginSuccessModel : RequestModel
    {
    }
}
