using IBHistoricalBarDataDumper;
using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChannelUnbounded
{
    class Program
    {
        static IAsyncEnumerable<string> ReadAll()
        {
            var buffer = Channel.CreateUnbounded<string>();

            _ = new IBDataReader(buffer.Writer).Start();

            return buffer.Reader.ReadAllAsync();
        }

        static async Task Main(string[] args)
        {
            await foreach (var item in ReadAll())
            {
                Console.WriteLine(item);
            }
        }
    }
}
