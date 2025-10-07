using System.Diagnostics;

namespace SimulationOfDataProcessing;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Synchronous processing");
        RunSynchronousProcessing();

        Console.WriteLine();
        Console.WriteLine("Asynchronous processing");

        await RunAsynchronousProcessing();

        Console.WriteLine();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    private static string ProcessData(string dataName)
    {
        Console.WriteLine($"Processing started: {dataName}");
        Thread.Sleep(3000);
        return $"'{dataName}' processing was completed in 3 seconds";
    }

    private static void RunSynchronousProcessing()
    {
        var stopwatch = Stopwatch.StartNew();

        Console.WriteLine(ProcessData("F1"));
        Console.WriteLine(ProcessData("F2"));
        Console.WriteLine(ProcessData("F3"));

        stopwatch.Stop();
        Console.WriteLine($"Total duration: {stopwatch.Elapsed.TotalSeconds:F2} seconds");
    }

    private static async Task RunAsynchronousProcessing()
    {
        var stopwatch = Stopwatch.StartNew();

        var tasks = new List<Task<string>>
        {
            ProcessDataAsync("F1"),
            ProcessDataAsync("F2"),
            ProcessDataAsync("F3")
        };

        while (tasks.Count > 0)
        {
            var finishedTask = await Task.WhenAny(tasks);

            var result = await finishedTask;  
            Console.WriteLine(result); 
            tasks.Remove(finishedTask);

        }

        stopwatch.Stop();
        Console.WriteLine($"Total duration: {stopwatch.Elapsed.TotalSeconds:F2} seconds");
    }

    private static async Task<string> ProcessDataAsync(string dataName)
    {
        Console.WriteLine($"Processing started: {dataName}");

        await Task.Delay(3000);

        return $"'{dataName}' processing was completed in 3 seconds";
    }
}
