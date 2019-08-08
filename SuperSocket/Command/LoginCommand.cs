
namespace SuperSocket.Command
{
    using System;
    using SuperSocket.Domain.Model;

    public class LoginCommand : IClientCommand
    {
        public Tuple<Exception> Excute(SocketSession session, RequestInfo requestInfo)
        {
            try
            {
                Console.WriteLine($"Login Excute RequestInfo Body: {requestInfo.Body}");
                session.MemberId = requestInfo.Body.JsonStringDeserialize<LoginModel>().MemberId;
                Console.WriteLine($"MemberId:{session.MemberId} Logined");

                var dataRequest = KeyType.LoginSuccess.GetRequestData(new LoginSuccessModel());
                session.Send(dataRequest, 0, dataRequest.Length);

                return Tuple.Create<Exception>(null);
            }
            catch (Exception ex)
            {
                return Tuple.Create<Exception>(ex);
            }
        }
    }
}
