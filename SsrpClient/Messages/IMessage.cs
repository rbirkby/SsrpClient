
namespace SsrpClient.Messages
{
    public interface IMessage
    {
        void Read(byte[] message);
        byte[] Write();
        int InitialTimeout {get;}
    }
}
