using SsrpClient.Messages;
using Xunit;

namespace SsrpClient.Tests
{
    public class ClientUnicastExTests
    {
        [Fact]
        public void CorrectMessageShouldParseSuccessfully()
        {
            var message = new ClientUnicastEx();
            message.Read(new byte[] {
                0x3
            });
        }

        [Fact]
        public void InvalidMethodLengthShouldThrow()
        {
            var message = new ClientUnicastEx();
            Assert.Throws<InvalidMessageLengthException>(() => message.Read(new byte[0]));
            Assert.Throws<InvalidMessageLengthException>(() => message.Read(new byte[] {
                0x3, 0x2
            }));
        }

        [Fact]
        public void InvalidTypeShouldThrow()
        {
            var message = new ClientUnicastEx();
            Assert.Throws<InvalidMessageException>(() => message.Read(new byte[] { 0x99 }));
        }
    }
}
