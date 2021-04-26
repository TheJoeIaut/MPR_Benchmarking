using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tests.Abstraction;

namespace Tests
{
    public class KNuckeotide : IPerfTest
    {
        public void Run(int i)
        {
            throw new NotImplementedException();
        }

        Task IPerfTest.Run(int i)
        {
            throw new NotImplementedException();
        }
    }
}
