using System;

namespace PullOrderTransaction
{
    class Program
    {
        static void Main(string[] args)
        {
            PullOrderTransactionStart process = new PullOrderTransactionStart();
            process.Start();
        }
    }
}
