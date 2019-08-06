
namespace SuperSocket
{
    using Hangfire;
    using Hangfire.Redis;
    using Owin;
    using SuperSocket.Applibs;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration
                .Configuration
                .UseRedisStorage(
                    NoSqlService.RedisConnections,
                    new RedisStorageOptions()
                    {
                        Db = 15,
                        SucceededListSize = 5,
                        DeletedListSize = 0
                    });

            //// 啟用HanfireServer
            app.UseHangfireServer();

            //// 啟用Hangfire的Dashboard
            app.UseHangfireDashboard();
        }
    }
}
