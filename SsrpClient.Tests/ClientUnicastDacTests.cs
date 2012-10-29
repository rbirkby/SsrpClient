using SsrpClient.Messages;
using Xunit;

namespace SsrpClient.Tests
{
    public class ClientUnicastDacTests
    {
        [Fact]
        public void CorrectMessageShouldParseSuccessfully()
        {
            var message = new ClientUnicastDac();
            message.Read(new byte[] {
                0xF, 0x1, 0x41, 0x41, 0x41, 0x0
            });

            Assert.Equal("AAA", message.InstanceName);
        }

        [Fact]
        public void InvalidMethodLengthShouldThrow()
        {
            var message = new ClientUnicastDac();
            Assert.Throws<InvalidMessageLengthException>(() => message.Read(new byte[0]));
            Assert.Throws<InvalidMessageLengthException>(() => message.Read(new byte[] {0xF}));
            Assert.Throws<InvalidMessageLengthException>(() => message.Read(new byte[] {
                0xF, 0x1,
                0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 
                0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 
                0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 
                0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 0x41, 
                0x0
            }));
        }

        [Fact]
        public void MissingNullTerminatorShouldThrow()
        {
            var message = new ClientUnicastDac();
            Assert.Throws<InvalidMessageException>(() => message.Read(new byte[] { 0xF, 0x1}));
            Assert.Throws<InvalidMessageException>(() => message.Read(new byte[] { 0xF, 0x1, 0x41 }));
        }

        [Fact]
        public void InvalidTypeShouldThrow()
        {
            var message = new ClientUnicastDac();
            Assert.Throws<InvalidMessageException>(() => message.Read(new byte[] { 0x99, 0x1, 0x41, 0x0 }));
        }
    }
}
