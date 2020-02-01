using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reboard.Utils.ConsoleBase
{
    public class UtilMethods
    {
        private CancellationTokenSource processingTask;

        public string Ask(string name)
        {
            Console.Write($"Type {name}: ");
            return Console.ReadLine();
        }

        public void Processing()
        {
            processingTask = new CancellationTokenSource();
            Task.Factory.StartNew(async () => await WriteProcessing(processingTask.Token), processingTask.Token);
        }

        public void Completed()
        {
            processingTask?.Cancel();
            Console.Clear();
            Console.WriteLine("Completed");
        }

        private async Task WriteProcessing(CancellationToken token, int iteration = 0)
        {
            if (token.IsCancellationRequested) return;
            Console.Clear();
            Console.Write("Processing" + new string('.', iteration));
            await Task.Delay(100);
            await WriteProcessing(token, iteration++);
        }

    }
}
