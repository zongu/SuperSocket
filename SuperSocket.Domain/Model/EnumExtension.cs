
namespace SuperSocket.Domain.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class EnumExtension
    {
        public static byte[] GetRequestData(this KeyType type, RequestModel request)
        {
            var byteList = new List<byte>();
            //// ushort 2個字節
            var key = BitConverter.GetBytes((ushort)type);
            var data = Encoding.UTF8.GetBytes(request.ToString());
            //// int 4個字節
            var dataLength = BitConverter.GetBytes(data.Length);

            byteList.AddRange(key);
            byteList.AddRange(dataLength);
            byteList.AddRange(data);

            return byteList.ToArray();
        }
    }
}
