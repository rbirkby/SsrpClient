using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiscUtil.Conversion;

namespace SsrpClient.Messages
{
    public class ServerResponse : IMessage
    {
        public static readonly byte Type = 0x5;
        private ushort _responseSize;
        public IEnumerable<Dictionary<string, string>> ResponseList { get; private set; }

        public void Read(byte[] message)
        {
            if (message.Length < 3) throw new InvalidMessageLengthException();
            if (message[0] != Type) throw new InvalidMessageException();

            _responseSize = EndianBitConverter.Little.ToUInt16(message, 1);

            if (_responseSize + 3 != message.Length)
            {
                throw new InvalidMessageLengthException();
            }

            // TODO: Validate maximum response

            Encoding encoding = Encoding.GetEncoding(NativeMethods.GetOEMCP());
            string data = encoding.GetString(message, 3, _responseSize);

            string[] groups = data.Split(new [] {";;"}, StringSplitOptions.RemoveEmptyEntries);

            ResponseList = from @group in groups
                            let tokens = @group.Split(';')
                            let evenTokens = tokens.Where((_, i) => i % 2 == 0)
                            let oddTokens = tokens.Where((_, i) => i % 2 == 1)
                            select evenTokens.Zip(oddTokens, (key, value) => Tuple.Create(key, value))
                                         .ToDictionary(kv => kv.Item1, kv => kv.Item2, StringComparer.OrdinalIgnoreCase);
        }

        public byte[] Write()
        {
            var bytes = new List<byte> { Type };
            bytes.AddRange(EndianBitConverter.Little.GetBytes(_responseSize));

            string data = string.Join(string.Empty, ResponseList.Select(s => string.Join(";", s.Select(kvp => kvp.Key + ";" + kvp.Value)) + ";;"));

            Encoding encoding = Encoding.GetEncoding(NativeMethods.GetOEMCP());
            bytes.AddRange(encoding.GetBytes(data));
                
            return bytes.ToArray();
        }
    }
}
