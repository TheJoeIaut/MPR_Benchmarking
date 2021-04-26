using CsvHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tests.Abstraction;

namespace Tests
{
    public class Testrunner
    {
        private readonly IPerfTest _test;
        private readonly List<(int, TimeSpan,int,int, long)> _results = new List<(int, TimeSpan,int,int, long)>();

        public Testrunner(IPerfTest test)
        {
            _test = test;
        }

        public async Task RunTest(int numberOfRuns, List<int> inputs, int run)
        {
            var watch = new Stopwatch();
            for (var i = 0; i < numberOfRuns; i++)
            {
            foreach (var input in inputs)
            {
    
                  watch.Start();
                  await _test.Run(input);
                  
                  watch.Stop();
                  var currentGcMemory = GC.GetTotalMemory(false);
                  _results.Add((input, watch.Elapsed,i,run, currentGcMemory ));
               
                  Console.WriteLine((input, watch.Elapsed,i,run, currentGcMemory));
                  
                  watch.Reset();
              }
            }
        }

        public void WriteResults(TextWriter stream)
        {
            using var csv = new CsvWriter(stream, CultureInfo.InvariantCulture);
            
            csv.Configuration.Delimiter=";";

            foreach (var input in _results.Select(x=>x.Item1).Distinct())
            {
                foreach (var result in _results.Where(x=>x.Item1 == input).OrderBy(x=>x.Item3))
                {
                    csv.WriteField(result.Item1);      
                    csv.WriteField(result.Item2.TotalMilliseconds);
                    csv.WriteField(result.Item3);
                    csv.WriteField(result.Item4);
                    csv.WriteField(result.Item5);
                    csv.NextRecord();
                }
                csv.WriteField(input); 
                csv.WriteField(new TimeSpan(Convert.ToInt64(_results.Where(x=>x.Item1 == input).Select(x=>x.Item2.Ticks).Average())).TotalMilliseconds);
                csv.NextRecord();
            }
        }
    }
}
