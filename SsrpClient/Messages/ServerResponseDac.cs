using System;
using System.Collections.Generic;
using MiscUtil.Conversion;

namespace SsrpClient.Messages
{
    public class ServerResponseDac : IMessage
    {
        public static readonly byte Type = 0x5;
        public ushort Port { get; private set; }

        public void Read(byte[] message)
        {
            if (message.Length < 6 || message[0] != Type)
            {
                throw new InvalidMessageException();
            }

            ushort responseSize = EndianBitConverter.Little.ToUInt16(message, 1);

            if (responseSize != 0x6)
            {
                throw new InvalidMessageException();
            }

            if (message[3] != 0x1)
            {
                throw new InvalidMessageException();
            }

            Port = EndianBitConverter.Little.ToUInt16(message, 4);
        }

        public byte[] Write()
        {
            var bytes = new List<byte>
            {
                Type, 0x6, 0x0, 0x1
            };

            bytes.AddRange(EndianBitConverter.Little.GetBytes(Port));

            return bytes.ToArray();
        }

        public int InitialTimeout
        {
            get { throw new NotImplementedException(); }
        }
    }
}
