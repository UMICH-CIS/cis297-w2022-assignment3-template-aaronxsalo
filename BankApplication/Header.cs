using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
	/// <summary>
	/// HEADER - this header file implements all classes used by the main program
	/// </summary>

	/// <summary>
	/// Account Class - default account object, inherited by SavingsAccount and CheckingAccount
	/// </summary>
	public class Account
	{
		private decimal accountBalance;

		public decimal AccountBalance { get { return accountBalance; } }

		/// <summary>
		/// Account Constructor - allows user to set initial balance
		/// </summary>
		public Account(decimal bal)
		{
			if (bal >= 0.0m)
			{
				accountBalance = bal;
			}
			else
			{
				Console.WriteLine("ERROR! Initial account balance must be at least 0.");
			}
		}

		/// <summary>
		/// this function allows the user to credit into the account - virtual for inherited classes
		/// </summary>
		public virtual void Credit(decimal amt)
		{
			accountBalance += amt;
		}

		/// <summary>
		/// this function allows the user to debit from the account - virtual for inherited classes
		/// </summary>
		public virtual bool Debit(decimal amt)
		{
			if (amt <= accountBalance)
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

		/// <summary>
		/// this function exists so that SavingsAccount can override it, this will allow polymorphism with this function
		/// </summary>
		public virtual decimal CalculateInterest() { return 0; }
	}

	/// <summary>
	/// SavingsAccount Class - inherits from Account class 
	/// </summary>
	public class SavingsAccount : Account
	{
		private decimal interestRate;

		/// <summary>
		/// SavingsAccount Constructor - allows user to set initial balance as well as interest rate
		/// </summary>
		public SavingsAccount(decimal bal, decimal intr) : base(bal)
		{
			interestRate = intr;
		}

		/// <summary>
		/// this functions calculates interest by multiplying rate times the account balance
		/// </summary>
		public override decimal CalculateInterest()
		{
			return interestRate * this.AccountBalance;
		}
	}

	/// <summary>
	/// CheckingAccount Class - inherits from Account class
	/// </summary>
	public class CheckingAccount : Account
	{
		private decimal transFee;

		/// <summary>
		/// CheckingAccount Constructor - allows user to set initial balance as well as the transaction fee
		/// </summary>
		public CheckingAccount(decimal bal, decimal fee) : base(bal)
		{
			transFee = fee;
		}

		/// <summary>
		/// this function overrides credit function to add in transaction fees
		/// </summary>
		public override void Credit(decimal amt)
		{
			base.Credit(amt);
			base.Debit(transFee);
		}

		/// <summary>
		/// this function overrides debit function to add in transaction fees
		/// </summary>
		public override bool Debit(decimal amt)
		{
			if (amt <= this.AccountBalance)
			{
				if (base.Debit(amt))
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

