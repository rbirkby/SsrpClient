using SsrpClient.Messages;
using Xunit;

namespace SsrpClient.Tests
{
    public class ClientBroadcastExTests
    {
        [Fact]
        public void CorrectMessageShouldParseSuccessfully()
        {
            var message = new ClientBroadcastEx();
            message.Read(new byte[] {
                0x2
            });
        }

        [Fact]
        public void InvalidMethodLengthShouldThrow()
        {
            var message = new ClientBroadcastEx();
            Assert.Throws<InvalidMessageLengthException>(() => message.Read(new byte[0]));
            Assert.Throws<InvalidMessageLengthException>(() => message.Read(new byte[] {
                0x2, 0x2
            }));
        }

        [Fact]
        public void InvalidTypeShouldThrow()
        {
            var message = new ClientBroadcastEx();
            Assert.Throws<InvalidMessageException>(() => message.Read(new byte[] { 0x99 }));
        }
    }
}
