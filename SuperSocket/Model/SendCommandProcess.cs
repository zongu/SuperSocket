
namespace SuperSocket.Model
{
    using System;
    using Autofac;
    using Newtonsoft.Json;
    using SuperSocket.Domain.Model;
    using SuperSocket.Domain.Repository;

    public class SendCommandProcess : ICommandProcess
    {
        public override void Excute()
        {
            using (var scope = Applibs.AutofacConfig.Container.BeginLifetimeScope())
            {
                try
                {
                    var repo = scope.Resolve<ISessionRepository>();
                    var member = repo.Get(currentSession);

                    if(member == null || member.MemberId == null)
                    {
                        throw new Exception("MemberStatus is empty");
                    }

                    var cmdContent = JsonConvert.DeserializeObject<SendModel>(commandString);
                    Console.WriteLine($"MemberId: {member.MemberId} Send: {cmdContent.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"SendCommandProcess Exception: {ex.Message}");
                }
            }
        }
    }
}
