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
            SavingsAccount save = new SavingsAccount(1000.0m,0.01m);
			CheckingAccount check = new CheckingAccount(1000.0m,0.50m);

            Console.ReadLine();
        }
    }

    public abstract class Account
	{
		private decimal accountBalance;

		public decimal AccountBalance { get { return accountBalance; } } 

		public Account(decimal bal)
        {
			if(bal >= 0.0m)
            {
				accountBalance = bal;
			}
			else
            {
				Console.WriteLine("ERROR! Initial account balance must be at least 0.");
            }
        }

		public virtual void Credit(decimal amt)
        {
			accountBalance += amt;
        }

		public virtual bool Debit(decimal amt)
        {
			if(amt <= accountBalance)
            {
				accountBalance -= amt;
				return true;
            }
			else
            {
				Console.WriteLine("ERROR! Debit amount exceeded the account balance.");
				return false;
            }
        }
	}

	public class SavingsAccount : Account
	{
		private decimal interestRate;

		public SavingsAccount(decimal bal, decimal intr) : base(bal)
        {
			interestRate = intr;
        }

		public decimal CalculateInterest()
        {
			return interestRate * this.AccountBalance;
        }
	}

	public class CheckingAccount : Account
	{
		private decimal transFee;

		public CheckingAccount(decimal bal, decimal fee) : base(bal)
        {
			transFee = fee;
        }

		public override void Credit(decimal amt)
        {
			base.Credit(amt);
			base.Debit(transFee);
		}

		public override bool Debit(decimal amt)
        {
			if (amt <= this.AccountBalance)
			{
				if(base.Debit(amt))
                {
					base.Debit(transFee);
				}
				return true;
			}
			else
			{
				Console.WriteLine("ERROR! Debit amount exceeded the account balance.");
				return false;
			}
		}
	}
}
