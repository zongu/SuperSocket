
namespace SuperSocket.Compoment
{
    public interface IProcessCompoment
    {
        void Start();

        void Stop();
    }

    public enum ExcutiveRoleType
    {
        Server,
        Client
    }
}
