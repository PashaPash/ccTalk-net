using System;

namespace dk.CctalkLib.Messages
{
	public class CctalkMessage
        // where TThis : CcTalkMessage<TThis,TResponse>
    {
        public static readonly Byte MinMessageLength = 5;
        public static readonly Byte MaxDataLength = 252;

        public static readonly Byte PosDestAddr = 0;
        public static readonly Byte PosDataLen = 1;
        public static readonly Byte PosSourceAddr = 2;
        public static readonly Byte PosHeader = 3;
        public static readonly Byte PosDataStart = 4;

        public Byte DestAddr { set; get; }
        public Byte SourceAddr { set; get; }
        public Byte Header { get; set; }

        public Byte[] Data;
        public Byte DataLength { get { return (Byte)(Data == null ? 0 : Data.Length); }
        }


        public Byte[] GetTransferDataNoChecksumm()
        {
            Byte[] msgData = Data;
            var msgDataLen = (Byte)(msgData == null ? 0 : msgData.Length);

            if (msgDataLen > MaxDataLength) 
                throw new InvalidOperationException("Data too long. " + GetType().Name);

            var msg = new byte[MinMessageLength + msgDataLen];
            msg[PosDestAddr] = DestAddr;
            msg[PosDataLen] = msgDataLen;
            msg[PosSourceAddr] = SourceAddr;
            msg[PosHeader] = Header;

            if (msgData!= null && msgDataLen > 0)
                Array.Copy(msgData, 0, msg, PosDataStart, msgData.Length);

            return msg;
        }


    }
}
