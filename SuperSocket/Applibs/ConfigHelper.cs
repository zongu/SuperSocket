
namespace SuperSocket.Applibs
{
    internal static class ConfigHelper
    {
        /// <summary>
        /// 定時刪沒數據傳送的連接
        /// </summary>
        public static int ClearIdleSessionInterval = 30;

        /// <summary>
        /// socket timeout時間
        /// </summary>
        public static int IdleSessionTimeOut = 5;

        /// <summary>
        /// 定時刪除尚未登入連接
        /// </summary>
        public static int RemoveExpiredInterval = 10;

        /// <summary>
        /// Socket連線IP
        /// </summary>
        public static string SocketIp = @"Any";

        /// <summary>
        /// Socket對外暴露Port
        /// </summary>
        public static int SocketPort = 3000;

        /// <summary>
        /// Socket最大連線數
        /// </summary>
        public static int SocketMaxConnectionNumber = 100;

        /// <summary>
        /// Client發送心跳包頻率
        /// </summary>
        public static int HealthCheckIntervalSeconds = 20;

        /// <summary>
        /// Socket Server Address
        /// </summary>
        public static string SocketServerAddress = @"ws://localhost:3000";
    }
}
