using SsrpClient.Messages;
using Xunit;

namespace SsrpClient.Tests
{
    public class ClientUnicastInstTests
    {
        [Fact]
        public void CorrectMessageShouldParseSuccessfully()
        {
            var message = new ClientUnicastInst();
            message.Read(new byte[] {
                0x4, 0x41, 0x41, 0x41, 0x0
            });

            Assert.Equal("AAA", message.InstanceName);
        }

        [Fact]
        public void InvalidMethodLengthShouldThrow()
        {
            var message = new ClientUnicastInst();
            Assert.Throws<InvalidMessageLengthException>(() => message.Read(new byte[0]));
            Assert.Throws<InvalidMessageLengthException>(() => message.Read(new byte[] {
                0x4,
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
            var message = new ClientUnicastInst();
            Assert.Throws<InvalidMessageException>(() => message.Read(new byte[] { 0x4 }));
            Assert.Throws<InvalidMessageException>(() => message.Read(new byte[] { 0x4, 0x41 }));
        }

        [Fact]
        public void InvalidTypeShouldThrow()
        {
            var message = new ClientUnicastInst();
            Assert.Throws<InvalidMessageException>(() => message.Read(new byte[] { 0x99 }));
        }
    }
}
