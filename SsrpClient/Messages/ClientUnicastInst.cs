using System;
using System.Text;

namespace SsrpClient.Messages
{
    public class ClientUnicastInst : IMessage
    {
        public static readonly byte Type = 0x4;
        public string InstanceName { get; set; }

        public void Read(byte[] message)
        {
            if (message.Length < 1 || message.Length > 34)
            {
                throw new InvalidMessageLengthException();
            }

            if (message[0] != Type || message[message.Length - 1] != '\0')
            {
                throw new InvalidMessageException();
            }

            Encoding encoding = Encoding.GetEncoding(NativeMethods.GetOEMCP());
            InstanceName = encoding.GetString(message, 1, message.Length - 2);
        }

        public byte[] Write()
        {
            Encoding encoding = Encoding.GetEncoding(NativeMethods.GetOEMCP());
            byte[] nameBytes = encoding.GetBytes(InstanceName);
            var message = new byte[nameBytes.Length + 2];
            message[0] = Type;
            Array.Copy(nameBytes, 0, message, 1, nameBytes.Length);
            message[message.Length - 1] = 0x0;

            return message;
        }
    }
}
