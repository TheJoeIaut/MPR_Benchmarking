using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tests.Abstraction;

namespace Tests
{
    public class Mandelbrot : IPerfTest
    {
        public void Run(int width)
        {
            int height = width;
            int maxiter = 50;
            double limit = 4.0;

            Console.WriteLine("P4");
            Console.WriteLine("{0} {1}", width, height);
            using Stream s = new MemoryStream();

            for (int y = 0; y < height; y++)
            {
                int bits = 0;
                int xcounter = 0;
                double Ci = 2.0 * y / height - 1.0;

                for (int x = 0; x < width; x++)
                {
                    double Zr = 0.0;
                    double Zi = 0.0;
                    double Cr = 2.0 * x / width - 1.5;
                    int i = maxiter;

                    bits = bits << 1;
                    do
                    {
                        double Tr = Zr * Zr - Zi * Zi + Cr;
                        Zi = 2.0 * Zr * Zi + Ci;
                        Zr = Tr;
                        if (Zr * Zr + Zi * Zi > limit)
                        {
                            bits |= 1;
                            break;
                        }
                    } while (--i > 0);

                    if (++xcounter != 8) 
                        continue;

                    s.WriteByte((byte)(bits ^ 0xff));
                    bits = 0;
                    xcounter = 0;
                }
                if (xcounter != 0)
                    s.WriteByte((byte)((bits << (8 - xcounter)) ^ 0xff));
            }
        }

        Task IPerfTest.Run(int i)
        {
            return Task.Run(() => Run(i));
        }
    }
}
