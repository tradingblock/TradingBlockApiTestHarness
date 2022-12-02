using System;
using TradingBlockApiTestHarness.Streaming;

namespace TradingBlockApiTestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiClient test = new ApiClient();
            var token = test.GetToken();

            //using (Client wSClient = new Client(token))
            //{
            //    wSClient.RunAsync();
            //}

            test.RunTest(token);

            System.Console.ReadLine();

            Environment.Exit(Environment.ExitCode);
        }
    }
}
