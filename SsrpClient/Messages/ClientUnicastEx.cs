
namespace SsrpClient.Messages
{
    public class ClientUnicastEx : IMessage
    {
        public static readonly byte Type = 0x3;

        public void Read(byte[] message)
        {
            if (message.Length != 1) throw new InvalidMessageLengthException();
            if (message[0] != Type) throw new InvalidMessageException();
        }

        public byte[] Write()
        {
            return new byte[] { Type };
        }

        public int InitialTimeout
        {
            get { return 5000; }
        }
    }
}
