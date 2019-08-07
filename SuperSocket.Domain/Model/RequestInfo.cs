
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
        HealthCheck
    }

    public class LoginModel
    {
        public int MemberId { get; set; }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }

    public class SendModel
    {
        public string Message { get; set; }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }

    public class HealthCheckModel
    {
        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}
