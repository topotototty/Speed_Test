using System.Diagnostics;
using System.Threading;

namespace Speed_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Test test = new Test();
            test.Start();
        }
    }
}