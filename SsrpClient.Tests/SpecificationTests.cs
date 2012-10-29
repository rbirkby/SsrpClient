using System;
using System.Collections.Generic;
using System.Linq;
using SsrpClient.Messages;
using Xunit;

namespace SsrpClient.Tests
{
    public class SpecificationTests
    {
        private static byte[] HexStringToArray(string hexString)
        {
            return hexString.Split(new [] {' ', '\n', '\r'}, StringSplitOptions.RemoveEmptyEntries)
                .Select(h => Convert.ToByte(h, 16))
                .ToArray();
        }

        [Fact]
        public void ProtocolExample1ShouldParseCorrectly()
        {
            byte[] request = HexStringToArray("03");

            new ClientUnicastEx().Read(request);

            byte[] response = HexStringToArray(@"05 47 01 53 65 72 76 65 72 4e 61 6d 65 3b 49 4c
                                                 53 55 4e 47 31 3b 49 6e 73 74 61 6e 63 65 4e 61 
                                                 6d 65 3b 59 55 4b 4f 4e 53 54 44 3b 49 73 43 6c 
                                                 75 73 74 65 72 65 64 3b 4e 6f 3b 56 65 72 73 69 
                                                 6f 6e 3b 39 2e 30 30 2e 31 33 39 39 2e 30 36 3b 
                                                 74 63 70 3b 35 37 31 33 37 3b 3b 53 65 72 76 65 
                                                 72 4e 61 6d 65 3b 49 4c 53 55 4e 47 31 3b 49 6e 
                                                 73 74 61 6e 63 65 4e 61 6d 65 3b 59 55 4b 4f 4e 
                                                 44 45 56 3b 49 73 43 6c 75 73 74 65 72 65 64 3b 
                                                 4e 6f 3b 56 65 72 73 69 6f 6e 3b 39 2e 30 30 2e 
                                                 31 33 39 39 2e 30 36 3b 6e 70 3b 5c 5c 49 4c 53 
                                                 55 4e 47 31 5c 70 69 70 65 5c 4d 53 53 51 4c 24 
                                                 59 55 4b 4f 4e 44 45 56 5c 73 71 6c 5c 71 75 65 
                                                 72 79 3b 3b 53 65 72 76 65 72 4e 61 6d 65 3b 49 
                                                 4c 53 55 4e 47 31 3b 49 6e 73 74 61 6e 63 65 4e 
                                                 61 6d 65 3b 4d 53 53 51 4c 53 45 52 56 45 52 3b 
                                                 49 73 43 6c 75 73 74 65 72 65 64 3b 4e 6f 3b 56 
                                                 65 72 73 69 6f 6e 3b 39 2e 30 30 2e 31 33 39 39 
                                                 2e 30 36 3b 74 63 70 3b 31 34 33 33 3b 6e 70 3b 
                                                 5c 5c 49 4c 53 55 4e 47 31 5c 70 69 70 65 5c 73 
                                                 71 6c 5c 71 75 65 72 79 3b 3b");

            var responseMessage = new ServerResponse();
            responseMessage.Read(response);

            var expected = new [] {
                new Dictionary<string, string> {
                    {"ServerName", "ILSUNG1"},
                    {"InstanceName", "YUKONSTD"},
                    {"IsClustered", "No"},
                    {"Version", "9.00.1399.06"},
                    {"tcp", "57137"}
                },
                new Dictionary<string, string> {
                    {"ServerName", "ILSUNG1"},
                    {"InstanceName", "YUKONDEV"},
                    {"IsClustered", "No"},
                    {"Version", "9.00.1399.06"},
                    {"np", @"\\ILSUNG1\pipe\MSSQL$YUKONDEV\sql\query"}
                },
                new Dictionary<string, string> {
                    {"ServerName", "ILSUNG1"},
                    {"InstanceName", "MSSQLSERVER"},
                    {"IsClustered", "No"},
                    {"Version", "9.00.1399.06"},
                    {"tcp", "1433"},
                    {"np", @"\\ILSUNG1\pipe\sql\query"}
                }
            };

            Assert.Equal(expected, responseMessage.ResponseList);
            Assert.Equal(response, responseMessage.Write());
        }

        [Fact]
        public void ProtocolExample2ShouldParseCorrectly()
        {
            byte[] request = HexStringToArray("04 59 55 4b 4f 4e 53 54 44 00"); // .YUKONSTD

            var requestMessage = new ClientUnicastInst();
            requestMessage.Read(request);

            Assert.Equal("YUKONSTD", requestMessage.InstanceName);
            Assert.Equal(request, requestMessage.Write());

            byte[] response = HexStringToArray(@"05 58 00 53 65 72 76 65 72 4e 61 6d 65 3b 49 4c
                                                 53 55 4e 47 31 3b 49 6e 73 74 61 6e 63 65 4e 61
                                                 6d 65 3b 59 55 4b 4f 4e 53 54 44 3b 49 73 43 6c
                                                 75 73 74 65 72 65 64 3b 4e 6f 3b 56 65 72 73 69
                                                 6f 6e 3b 39 2e 30 30 2e 31 33 39 39 2e 30 36 3b
                                                 74 63 70 3b 35 37 31 33 37 3b 3b");

            var responseMessage = new ServerResponse();
            responseMessage.Read(response);

            var expected = new[] {
                new Dictionary<string, string> {
                    {"ServerName", "ILSUNG1"},
                    {"InstanceName", "YUKONSTD"},
                    {"IsClustered", "No"},
                    {"Version", "9.00.1399.06"},
                    {"tcp", "57137"}
                }
            };

            Assert.Equal(expected, responseMessage.ResponseList);
            Assert.Equal(response, responseMessage.Write());
        }

        [Fact]
        public void ProtocolExample3ShouldParseCorrectly()
        {
            byte[] request = HexStringToArray("0f 01 59 55 4b 4f 4e 53 54 44 00"); // ..YUKONSTD

            var requestMessage = new ClientUnicastDac();
            requestMessage.Read(request);

            Assert.Equal("YUKONSTD", requestMessage.InstanceName);
            Assert.Equal(request, requestMessage.Write());

            byte[] response = HexStringToArray("05 06 00 01 32 df"); // ....2

            var responseMessage = new ServerResponseDac();
            responseMessage.Read(response);

            Assert.Equal(0xdf32, responseMessage.Port);
            Assert.Equal(response, responseMessage.Write());
        }
    }
}
