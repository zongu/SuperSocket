
namespace SuperSocket.Domain.Model
{
    using System.Text;
    using Newtonsoft.Json;
    using SuperSocket.SocketBase.Protocol;

    public class RequestInfo : IRequestInfo
    {
        public RequestInfo(ClientKeyType type, byte[] bodyBuffer)
        {
            Type = type;
            BodyBuffer = bodyBuffer;
        }

        public string Key
        {
            get => $"{Type}";
        }

        public ClientKeyType Type { get; set; }

        public byte[] BodyBuffer { get; set; }

        public string Body
        {
            get
            {
                return Encoding.UTF8.GetString(BodyBuffer);
            }
        }
    }

    /// <summary>
    /// 來自Client command
    /// </summary>
    public enum ClientKeyType
    {
        Login,
        Send,
        HealthCheck
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

    /// <summary>
    /// 來自Server command
    /// </summary>
    public enum ServerKeyType
    {

        BroadCast,
        SendTo
    }

    public class BroadCastModel : RequestModel
    {
        public string Message;
    }

    public class SendToModel : RequestModel
    {
        public string Message;
    }
}
