using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using System;

namespace DateTimeBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<DateTimeOperations>();
        }
    }

    [MemoryDiagnoser]
    [RankColumn]
    public class DateTimeOperations
    {
        private const string dateString = "2020-09-14";
        private readonly DateOperations dateOperations = new DateOperations();

        // Convert that string to datetime obj get .year
        // Using substring 0 to index of -
        // Regex pattern match to get the year
        // Split the string t3 items using -

        [Benchmark]
        public void GetYearFromDateTime()
        {
            dateOperations.GetYearFromDateTime(dateString);
        }

        [Benchmark]
        public void GetYearFromSubstring()
        {
            dateOperations.GetYearFromSubstring(dateString);
        }

        [Benchmark]
        public void GetYearFromSplit()
        {
            dateOperations.GetYearFromSplit(dateString);
        }

        [Benchmark]
        public void GetYearFromReadOnlySpan()
        {
            dateOperations.GetYearFromReadOnlySpan(dateString);
        }
    }
}
