using System;
using System.Threading.Tasks;

namespace Demo_Excel
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                // await processDataParallelAsync();
                await AsyncExecutor.ProcessDataAsync();
                // await ParalelAsyncExecutor.processDataParallelAsync();
                watch.Stop();
                ConsoleLogWriter.WritelineMessage($"Total time execution using Parallel - Async {watch.ElapsedMilliseconds}", ConsoleColor.DarkMagenta);

                /*
                watch.Reset();
                watch.Start();
                // await ProcessDataAsync();
                await AsyncExecutor.ProcessDataAsync();
                watch.Stop();
                ConsoleLogWriter.WritelineMessage($"Total time execution Blocked - Async {watch.ElapsedMilliseconds}", ConsoleColor.DarkMagenta);
                */

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while processing excel... {ex}");
            }

            ConsoleLogWriter.WritelineMessage("Completed, Press key to exit.", ConsoleColor.DarkBlue);
            Console.ReadLine();
        }
    }
}
