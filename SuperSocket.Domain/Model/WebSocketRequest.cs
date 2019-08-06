
namespace SuperSocket.Domain.Model
{
    using Newtonsoft.Json;

    public enum CommandEnum
    {
        Login,
        Send
    }

    public class WebSocketRequest
    {
        public WebSocketRequest(CommandEnum cmd, string data)
        {
            Command = cmd;
            Data = data;
        }

        public CommandEnum Command { get; set; }

        public string Data { get; set; }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}
