
namespace SuperSocket.Command
{
    using System;
    using SuperSocket.Domain.Model;

    public class LoginSuccessCommand : IServerCommand
    {
        public Tuple<Exception> Excute(RequestInfo requestInfo)
        {
            try
            {
                Console.WriteLine($"Login Success!");
                return Tuple.Create<Exception>(null);
            }
            catch (Exception ex)
            {
                return Tuple.Create<Exception>(ex);
            }
        }
    }
}
