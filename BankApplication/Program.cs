using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            SavingsAccount sa = new SavingsAccount(1000.0m,0.15m);

            Console.ReadLine();
        }
    }
}
