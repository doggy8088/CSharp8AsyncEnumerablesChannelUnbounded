using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace IBHistoricalBarDataDumper
{
    public class IBDataReader
    {
        private readonly ChannelWriter<string> writer;

        public IBDataReader(ChannelWriter<string> writer)
        {
            this.writer = writer;
        }

        public event EventHandler<string> OnRead;

        public async Task Start()
        {
            for (int i = 0; i < 10; i++)
            {
                await writer.WriteAsync(i.ToString());

                var ms = (new Random()).Next(40, 1000);
                Console.WriteLine($"The {i} has been emitted. Wait for {ms} ms.");
                await Task.Delay(ms);
            }

            writer.TryComplete();
        }
    }
}