using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Tests.Abstraction;

namespace Tests
{
    public class PiDigits:IPerfTest
    {
        const int DigitsPerLine = 10;

        public void Run(int para)
        {
            var i = 1;

            foreach (var d in GenDigits().Take(para))
            {
                Console.Out.Write(d);
                if ((i % DigitsPerLine) == 0)
                    Console.Out.WriteLine("\t:" + i);
                i++;
            }

            // Pad out any trailing digits for the final line
            if ((para % DigitsPerLine) > 0)
                Console.Out.WriteLine(new string(' ', (DigitsPerLine - (para % DigitsPerLine))) + "\t:" + para);
        }

        private static IEnumerable<int> GenDigits()
        {
            var k = 1;
            var n1 = new BigInteger(4);
            var n2 = new BigInteger(3);
            var d = BigInteger.One;

            while (true)
            {
                // digit
                var u = BigInteger.Divide(n1, d);
                var v = BigInteger.Divide(n2, d);

                if (BigInteger.Compare(u,v) == 0)                
                {
                    yield return (int)u;

                    // extract
                    u = BigInteger.Multiply(u, -10);
                    u = BigInteger.Multiply(u, d);
                    n1 = BigInteger.Multiply(n1, 10);
                    n1 = BigInteger.Add(n1, u);
                    n2 = BigInteger.Multiply(n2, 10);
                    n2 = BigInteger.Add(n2, u);
                }
                else
                {
                    // produce
                    var k2 = k * 2;
                    u = BigInteger.Multiply(n1, k2 - 1);
                    v = BigInteger.Add(n2, n2);
                    var w = BigInteger.Multiply(n1, k - 1);
                    n1 = BigInteger.Add(u, v);
                    u = BigInteger.Multiply(n2, k + 2);
                    n2 = BigInteger.Add(w, u);
                    d = BigInteger.Multiply(d, k2 + 1);
                    k++;
                }
            }
        }

        Task IPerfTest.Run(int i)
        {
            return Task.Run(() => Run(i));
        }
    }
}
