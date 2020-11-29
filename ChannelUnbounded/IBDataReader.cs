using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace IBHistoricalBarDataDumper
{
    public class IBDataReader
    {
        public delegate bool TryComplete(Exception error = null);
        public delegate void Complete(Exception error = null);

        private readonly Complete complete;
        private readonly TryComplete tryComplete;
        public IBDataReader(Complete complete)
        {
            this.complete = complete;
        }
        public IBDataReader(TryComplete tryComplete)
        {
            this.tryComplete = tryComplete;
        }

        public event EventHandler<string> OnRead;

        public async Task Start()
        {
            for (int i = 0; i < 10; i++)
            {
                OnRead(this, i.ToString());
                var ms = (new Random()).Next(40, 1000);
                Console.WriteLine($"The {i} has been emitted. Wait for {ms} ms.");
                await Task.Delay(ms);
            }

            tryComplete(new AccessViolationException("OK"));
        }
    }
}