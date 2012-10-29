using System.Collections.Generic;
using SsrpClient.Messages;
using Xunit;

namespace SsrpClient.Tests
{
    public class ServerResponseTests
    {
        [Fact]
        public void CorrectMessageShouldParseSuccessfully()
        {
            var message = new ServerResponse();
            message.Read(new byte[] {
                0x5, 0x5, 0x0, 0x41, 0x3b, 0x41, 0x3b, 0x3b 
            });

            Assert.Equal(new [] {
                new Dictionary<string, string> {
                    {"A", "A"}
                }
            }, message.ResponseList);
        }

        [Fact]
        public void InvalidMethodLengthShouldThrow()
        {
            var message = new ServerResponse();
            Assert.Throws<InvalidMessageLengthException>(() => message.Read(new byte[0]));
            Assert.Throws<InvalidMessageLengthException>(() => message.Read(new byte[] {0x5}));
            Assert.Throws<InvalidMessageLengthException>(() => message.Read(new byte[] { 0x5, 0x5, 0x0, 0x41 }));
        }

        [Fact]
        public void InvalidTypeShouldThrow()
        {
            var message = new ServerResponse();
            Assert.Throws<InvalidMessageException>(() => message.Read(new byte[] { 0x3, 0x1, 0x0, 0x41 }));
        }
    }
}
