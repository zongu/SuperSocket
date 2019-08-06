
namespace SuperSocket.Applibs
{
    internal static class ConfigHelper
    {
        public static int WebSocketPort = 3000;

        public static int WebSocketSessionExpiredSeconds = 30;

        public static string SocketServerAddress = @"ws://localhost:3000";

        public static string RedisConn = @"localhost:6379";

        public static string OwinListenAddress = @"http://localhost:8089";
    }
}
