using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
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

		public void Credit(decimal amt)
        {
			accountBalance += amt;
        }

		public bool Debit(decimal amt)
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

		public override Credit(decimal amt)
        {
			this.Credit(amt);
			this.AccountBalance -= transFee;
		}

		public override Debit(decimal amt)
        {
			if (amt <= accountBalance)
			{
				if(this.Debit(amt))
					this.AccountBalance -= transFee;
			}
			else
			{
				Console.WriteLine("ERROR! Debit amount exceeded the account balance.");
			}
		}
	}
}