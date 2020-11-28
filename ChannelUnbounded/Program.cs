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

            var reader = new IBDataReader();
            reader.OnRead += async (_, msg) => await buffer.Writer.WriteAsync(msg);
            _ = reader.Start();

            _ = Task.Run(async () =>
                {
                    await reader.WaitUntilAllRead();
                    buffer.Writer.TryComplete();
                });

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
