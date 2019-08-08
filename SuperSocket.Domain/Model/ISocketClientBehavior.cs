
namespace SuperSocket.Domain.Model
{
    using System;
    using System.Timers;
    using SuperSocket.ClientEngine;
    using WebSocket4Net;

    public abstract class ISocketClientBehavior
    {
        protected WebSocket client;

        protected Timer timer;

        public ISocketClientBehavior(string socketAddress, int healthCheckIntervalSeconds)
        {   
            this.client = new WebSocket(socketAddress);
            this.client.Opened += ClientOnOpened;
            this.client.DataReceived += ClientOnDataReceived;
            this.client.Error += ClientOnError;
            this.client.Closed += ClientOnClosed;

            this.timer = new Timer();
            this.timer.AutoReset = true;
            this.timer.Interval = healthCheckIntervalSeconds * 1000;
            this.timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
            => HealthCheck();

        protected abstract void HealthCheck();

        protected abstract void ClientOnError(object sender, ErrorEventArgs e);

        protected abstract void ClientOnDataReceived(object sender, DataReceivedEventArgs e);

        protected abstract void ClientOnClosed(object sender, EventArgs e);

        protected abstract void ClientOnOpened(object sender, EventArgs e);

        public abstract void Send(string message);

        public void Open()
        {
            this.client.Open();
        }

        public void Close()
        {
            this.timer.Stop();
            this.timer.Dispose();
            this.client.Close();
            this.client.Dispose();
        }
    }
}
