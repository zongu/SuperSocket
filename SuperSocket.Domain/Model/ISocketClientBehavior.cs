
namespace SuperSocket.Domain.Model
{
    using System;
    using WebSocket4Net;

    public abstract class ISocketClientBehavior
    {
        protected WebSocket client;

        protected int memberId;

        public bool IsOpened;

        public ISocketClientBehavior(string address, int memberId)
        {
            this.memberId = memberId;
            this.client = new WebSocket(address);
            this.client.Opened += OnConnected;
            this.client.DataReceived += DataReceived;
            this.client.MessageReceived += MessageReceived;
            this.client.Closed += Disconnected;
            IsOpened = false;
        }

        public abstract void Disconnected(object sender, EventArgs e);

        public abstract void MessageReceived(object sender, MessageReceivedEventArgs e);

        public abstract void DataReceived(object sender, DataReceivedEventArgs e);

        public abstract void OnConnected(object sender, EventArgs e);

        public abstract void Send(string message);

        public void Start()
            => this.client.Open();

        public void Stop()
        {
            this.client.Close();
            this.client.Dispose();
        }
    }
}
