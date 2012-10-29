using System;
using System.Text;

namespace SsrpClient.Messages
{
    public class ClientUnicastDac : IMessage
    {
        public static readonly byte Type = 0xF;
        public string InstanceName { get; set; }

        public void Read(byte[] message)
        {
            if (message.Length < 2 || message.Length > 35)
            {
                throw new InvalidMessageLengthException();
            }

            if (message[0] != Type || message[message.Length - 1] != 0x0)
            {
                throw new InvalidMessageException();
            }

            if (message[1] != 0x1)
            {
                throw new InvalidMessageException();
            }

            Encoding encoding = Encoding.GetEncoding(NativeMethods.GetOEMCP());
            InstanceName = encoding.GetString(message, 2, message.Length - 3);
        }

        public byte[] Write()
        {
            Encoding encoding = Encoding.GetEncoding(NativeMethods.GetOEMCP());
            byte[] nameBytes = encoding.GetBytes(InstanceName);
            var message = new byte[nameBytes.Length + 3];

            message[0] = Type;
            message[1] = 0x1;
            Array.Copy(nameBytes, 0, message, 2, nameBytes.Length);
            message[message.Length - 1] = 0x0;

            return message;
        }
    }
}
