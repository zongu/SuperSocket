
namespace SuperSocket.Domain.Model
{
    using System;
    using SuperSocket.Common;
    using SuperSocket.Facility.Protocol;

    /// <summary>
    /// 通訊內容協議
    /// +----------+--------+--------------+
    /// |  Key(1)  | len(2) | request body |
    /// +----------+--------+--------------+
    /// </summary>
    public class ReceiveFilter : FixedHeaderReceiveFilter<RequestInfo>
    {
        public ReceiveFilter() : base(4)
        {
        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            return (int)header[offset + 2] * 256 + (int)header[offset + 3];
        }

        protected override RequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            return new RequestInfo((KeyType)(int)header.Array[header.Offset], bodyBuffer.CloneRange(offset, length));
        }
    }
}
