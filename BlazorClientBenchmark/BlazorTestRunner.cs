using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Tests;
using Tests.Abstraction;

namespace BlazorClientBenchmark
{
    public class BlazorTestRunnerHelper{
        private readonly IJSRuntime _jsRuntime;
        private readonly IPerfTest _test;
        private readonly List<int> _inputs;
        private readonly Testrunner _testrunner;
        private int _run;
   

        public BlazorTestRunnerHelper(IJSRuntime jsRuntime, IPerfTest test, List<int> inputs)
        {
            _jsRuntime = jsRuntime;
            _test = test;
            _inputs = inputs;
            _testrunner = new Testrunner(test);
        }
        
        [JSInvokable]
        public async Task RunTest()
        {
            await _testrunner.RunTest(100, _inputs, 1);
        }

        [JSInvokable]
        public async Task Finish()
        {
            var memoryStream = new MemoryStream();
            var tw = new StreamWriter(memoryStream);
            _testrunner.WriteResults(tw);
            await FileUtil.SaveAs(_jsRuntime, @$"{_test.GetType()}_Console_{DateTime.Now:yyyymmddhhMMss}.csv", memoryStream.ToArray());
        }
    }
}
