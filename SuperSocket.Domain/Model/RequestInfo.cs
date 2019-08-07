
namespace SuperSocket.Domain.Model
{
    using System.Text;
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
        Send
    }
}
