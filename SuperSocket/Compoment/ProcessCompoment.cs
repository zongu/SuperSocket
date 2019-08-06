
namespace SuperSocket.Compoment
{
    public abstract class ProcessCompoment
    {
        protected abstract void Process();

        public abstract void Stop();

        public void Start()
        {
            Process();
        }
    }
}
