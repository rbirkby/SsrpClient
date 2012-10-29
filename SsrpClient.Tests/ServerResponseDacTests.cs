using SsrpClient.Messages;
using Xunit;

namespace SsrpClient.Tests
{
    public class ServerResponseDacTests
    {
        [Fact]
        public void CorrectMessageShouldParseSuccessfully()
        {
            var message = new ServerResponseDac();
            message.Read(new byte[] {
                0x5, 0x6, 0x0, 0x1, 0x99, 0x5
            });

            Assert.Equal(1433, message.Port);
        }

        [Fact]
        public void InvalidMethodLengthShouldThrow()
        {
            var message = new ServerResponse();
            Assert.Throws<InvalidMessageLengthException>(() => message.Read(new byte[0]));
            Assert.Throws<InvalidMessageLengthException>(() => message.Read(new byte[] {0x5}));
            Assert.Throws<InvalidMessageLengthException>(() => message.Read(new byte[] { 0x5, 0x6, 0x0 }));
            Assert.Throws<InvalidMessageLengthException>(() => message.Read(new byte[] { 0x5, 0x6 }));
        }

        [Fact]
        public void InvalidTypeShouldThrow()
        {
            var message = new ServerResponse();
            Assert.Throws<InvalidMessageException>(() => message.Read(new byte[] { 0x4, 0x6, 0x0, 0x1, 0x99, 0x5 }));
        }
    }
}
