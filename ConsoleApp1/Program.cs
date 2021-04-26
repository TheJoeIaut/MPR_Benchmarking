using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tests;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if(args.Length!=1)
                throw new ArgumentException();

          
            using var writer = new StreamWriter(@$"D:\Google Drive\FH\Master\Masterarbeit\Results\{args[0]}_Console_{DateTime.Now.ToString("yyyymmddhhMMss")}.csv");
            Testrunner testrunner;
            switch (args[0])
            {
                case "BinaryTree":
                {          testrunner = new Testrunner(new BinaryTrees());
                        await testrunner.RunTest(30, Enumerable.Range(15, 1).ToList(),1);             
                        testrunner.WriteResults(writer);
                         
                    break;
                }
                case "SpectralNorm":
                    SpectralNorm.Run(100);
                    break;
                case "Mandelbrot":
                    testrunner = new Testrunner(new Mandelbrot());
                    await testrunner.RunTest(100, Enumerable.Range(1, 1).Select(x=>x*1000).ToList(),1);             
                    testrunner.WriteResults(writer);
                    break;
                case "Fannkuchen":
                {
                    testrunner = new Testrunner(new Fannkuchen());
                    await testrunner.RunTest(100, Enumerable.Range(9, 1).ToList(),1);             
                    testrunner.WriteResults(writer);
                    break;
                }
                case "PiDigits":
                {
                    testrunner = new Testrunner(new PiDigits());
                    await testrunner.RunTest(50, Enumerable.Range(5, 1).Select(x=>100*x).ToList(),1);             
                    testrunner.WriteResults(writer);
                    break;
                }

                case "NBody":
                {
                    testrunner = new Testrunner(new Nbody());
                    await testrunner.RunTest(100, Enumerable.Range(1, 1).Select(x=>100000*x).ToList(),1);             
                    testrunner.WriteResults(writer);
                    break;
                }
                case "Communication":
                {
                    testrunner = new Testrunner(new Communcation());
                    await testrunner.RunTest(100, Enumerable.Range(5, 1).Select(x=>1000*x).ToList(),1);             
                    testrunner.WriteResults(writer);
                    break;
                }
            }
        }
    }
}
