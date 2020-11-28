using System;
using System.Threading.Tasks;

namespace IBHistoricalBarDataDumper
{
    public class IBDataReader
    {
        public IBDataReader()
        {
        }

        public event EventHandler<string> OnRead;

        public bool isCompleted = false;

        public async Task Start()
        {
            for (int i = 0; i < 20; i++)
            {
                OnRead(this, i.ToString());

                await Task.Delay((new Random()).Next(40, 1000));
            }

            isCompleted = true;
        }

        public async Task WaitUntilAllRead()
        {
            while (!isCompleted)
            {
                await Task.Delay(10);
            }
        }
    }
}