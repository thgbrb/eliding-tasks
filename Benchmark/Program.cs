#define BENCHMARK
#define ELIDING_WITH_EXCEPTION
#define ELIDING_WITHOUT_EXCEPTION
#define NON_ELIDING_WITH_EXCEPTION
#define NON_ELIDING_WITHOUT_EXCEPTION

using System.Diagnostics;
using System.Threading.Tasks;
using Application;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Benchmark
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            RunBenchmark();
            ElidingTaskWithException();
            ElidingTaskWithoutException();
            NonElidingTaskWithException();
            NonElidingTaskWithoutException();
        }

        [Conditional("BENCHMARK")]
        public static void RunBenchmark()
        {
            var summary = BenchmarkRunner.Run<TaskNonElidingVsEliding>();
        }

        [Conditional("ELIDING_WITH_EXCEPTION")]
        public static void ElidingTaskWithException()
        {
            var task = new TaskNonElidingVsEliding();
            task.ElidingTask().GetAwaiter().GetResult();
        }

        [Conditional("ELIDING_WITHOUT_EXCEPTION")]
        public static void ElidingTaskWithoutException()
        {
            var task = new TaskNonElidingVsEliding();
            task.Setup();
            task.ElidingTask().GetAwaiter().GetResult();
        }

        [Conditional("NON_ELIDING_WITH_EXCEPTION")]
        public static void NonElidingTaskWithException()
        {
            var task = new TaskNonElidingVsEliding();
            task.NonElidingTask().GetAwaiter().GetResult();
        }

        [Conditional("NON_ELIDING_WITHOUT_EXCEPTION")]
        public static void NonElidingTaskWithoutException()
        {
            var task = new TaskNonElidingVsEliding();
            task.Setup();
            task.NonElidingTask().GetAwaiter().GetResult();
        }
    }


    [SimpleJob(RuntimeMoniker.Net472, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [RPlotExporter]
    [MemoryDiagnoser]
    public class TaskNonElidingVsEliding
    {
        private string _hostname;

        [GlobalSetup]
        public void Setup()
        {
            _hostname = "localhost";
        }

        [Benchmark]
        public async Task<string> NonElidingTask()
        {
            var app = new DoWork();
            var result = await app.GetHostnameIpAddressAsync(_hostname);

            return result;
        }

        [Benchmark]
        public async Task<string> ElidingTask()
        {
            var app = new DoWork();
            var result = await app.ElidingGetHostnameIpAddressAsync(_hostname);

            return result.AddressList[0].ToString();
        }
    }
}