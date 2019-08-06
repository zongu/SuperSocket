
namespace SuperSocket.Behavior
{
    using System;
    using System.Text;
    using SuperSocket.Domain.Model;
    using WebSocket4Net;

    public class SocketClientBehavior : ISocketClientBehavior
    {
        public SocketClientBehavior(string address, int memberId) : base(address, memberId)
        {
        }

        public override void DataReceived(object sender, DataReceivedEventArgs e)
        {
            var inputStr = Encoding.UTF8.GetString(e.Data);
            Console.WriteLine($"DataReceived: {inputStr}");
        }

        public override void Disconnected(object sender, EventArgs e)
        {
            IsOpened = false;
            Console.WriteLine("ClientProcessCompoment Closed");
        }

        public override void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine($"MessageReceived: {e.Message}");
        }

        public override void OnConnected(object sender, EventArgs e)
        {
            this.client.Send(new WebSocketRequest(CommandEnum.Login, new LoginModel(this.memberId).ToString()).ToString());
            IsOpened = true;
            Console.WriteLine("ClientProcessCompoment Opened");
        }

        public override void Send(string message)
        {
            if (IsOpened)
            {
                this.client.Send(new WebSocketRequest(CommandEnum.Send, new SendModel(message).ToString()).ToString());
            }
        }
    }
}
