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
        var startTime = DateTime.Now;

        Console.WriteLine(ProcessData("F1"));
        Console.WriteLine(ProcessData("F2"));
        Console.WriteLine(ProcessData("F3"));

        var totalTime = DateTime.Now - startTime;
        Console.WriteLine($"Total duration: {totalTime.TotalSeconds:F1} seconds");
    }

    private static async Task RunAsynchronousProcessing()
    {
        var startTime = DateTime.Now;

        var task1 = ProcessDataAsync("F1");
        var task2 = ProcessDataAsync("F2");
        var task3 = ProcessDataAsync("F3");

        var tasks = new[] { task1, task2, task3 };
        while (tasks.Length > 0)
        {
            var finishedTask = await Task.WhenAny(tasks);
            Console.WriteLine(await finishedTask);

            tasks = tasks.Where(t => t != finishedTask).ToArray();
        }

        var totalTime = DateTime.Now - startTime;
        Console.WriteLine($"Total duration: {totalTime.TotalSeconds:F1} seconds");
    }

    private static async Task<string> ProcessDataAsync(string dataName)
    {
        Console.WriteLine($"Processing started: {dataName}");
        await Task.Delay(3000);
        return $"'{dataName}' processing was completed in 3 seconds";
    }
}
