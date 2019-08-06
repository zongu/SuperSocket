
namespace SuperSocket.Model
{
    using System;
    using Autofac;
    using Newtonsoft.Json;
    using SuperSocket.Domain.Model;
    using SuperSocket.Domain.Repository;
    using SuperSocket.WebSocket;

    public class LoginCommandProcess : ICommandProcess
    {
        public override void Excute()
        {
            using (var scope = Applibs.AutofacConfig.Container.BeginLifetimeScope())
            {
                try
                {
                    var repo = scope.Resolve<ISessionRepository>();
                    var member = repo.Get(currentSession);

                    if (member != null)
                    {
                        var cmdContent = JsonConvert.DeserializeObject<LoginModel>(commandString);
                        repo.CheckIn(currentSession, cmdContent.MemberId);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"LoginCommandProcess Exception: {ex.Message}");
                }
            }
        }
    }   
}
