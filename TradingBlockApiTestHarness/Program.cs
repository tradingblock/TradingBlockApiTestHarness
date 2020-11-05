using System;

namespace TradingBlockApiTestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiClient test = new ApiClient();
            test.RunTest();

            Console.ReadLine();

            Environment.Exit(Environment.ExitCode);
        }
    }
}
