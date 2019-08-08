
namespace SuperSocket.Domain.Model
{
    using System;
    using SuperSocket.Common;
    using SuperSocket.Facility.Protocol;

    /// <summary>
    /// 通訊內容協議
    /// +----------+--------+--------------+
    /// |  Key(2)  | len(4) | request body |
    /// +----------+--------+--------------+
    /// </summary>
    public class ReceiveFilter : FixedHeaderReceiveFilter<RequestInfo>
    {
        public ReceiveFilter() : base(6)
        {
        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
            => BitConverter.ToInt32(header, 2);

        protected override RequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
            =>new RequestInfo((ClientKeyType)BitConverter.ToUInt16(header.CloneRange(0, 2), 0), bodyBuffer.CloneRange(offset, length));
    }
}
