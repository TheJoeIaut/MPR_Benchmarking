using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Abstraction
{
    public interface IPerfTest
    {
        public Task Run(int i);
    }
}
