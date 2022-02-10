using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    /// <summary>
    /// This program allows the user to credit/debit from a savings and checking account
    /// </summary>
    /// <Student>Aaron Salo</Student>
    /// <Class>CIS297</Class>
    /// <Semester>Winter 2022</Semester>
	class Program
    {
        /// <summary>
		/// This function is used to run the test code required by Exercise 11.8 - user will be prompted to run it with choice 0
		/// </summary>
		public static void testRun()
		{
			Console.WriteLine("CONSTRUCTOR ERROR TEST:");
			Account acc = new Account(-1000.0m); //tests < zero constructor balance
			SavingsAccount save = new SavingsAccount(-1000.0m,0.01m); //tests < zero constructor balance
			CheckingAccount check = new CheckingAccount(-1000.0m,0.50m); //tests < zero constructor balance

			Console.WriteLine("\nINITIAL BALANCE TEST:");
			acc = new Account(1000.0m); //test constructor balance
			save = new SavingsAccount(1000.0m,0.01m); //test constructor balance
			check = new CheckingAccount(1000.0m,0.50m); //test constructor balance
			Console.WriteLine($"Account: {acc.AccountBalance}\nSavings: {save.AccountBalance}\nChecking: {check.AccountBalance}\n");

			Console.WriteLine("\nCREDIT/INTEREST TEST:");
			acc.Credit(500.0m); //adds $500 to standard account
			save.Credit(save.CalculateInterest()); //adds interest to savings
			check.Credit(500.0m); //adds $500 to checking account - fee will happen
			Console.WriteLine($"Account: {acc.AccountBalance}\nSavings: {save.AccountBalance}\nChecking: {check.AccountBalance}\n");

			Console.WriteLine("\nDEBIT TEST:");
			acc.Debit(250.0m); //removes $250 from standard account
			save.Debit(110.0m); //removes $110 from savings account
			check.Debit(100.0m); //removes $100 from checking account - fee will happen again
			Console.WriteLine($"Account: {acc.AccountBalance}\nSavings: {save.AccountBalance}\nChecking: {check.AccountBalance}\n");

			Console.WriteLine("\nDEBIT ERROR TEST:");
			acc.Debit(25000.0m); //tests debits > balance
			save.Debit(11000.0m); //tests debits > balance
			check.Debit(10000.0m); //tests debits > balance

			Console.WriteLine("\nPress enter to continue...");
			Console.ReadLine(); //holds open console
		}

		/// <summary>
		/// MAIN FUNCTION
		/// </summary>
		static void Main(string[] args)
        {
            int input = -1; //user input - initialized to -1 one for 0 option
			Account[] accounts = new Account[2]; //list of accounts - savings and checking
			accounts[0] = new SavingsAccount(0,0.01m); //first account in array is savings - $0 bal - 1% int
			accounts[1] = new CheckingAccount(0,0.15m); //second account in array is checking - $0 bal - $0.15 fee

			while(input != 7 && input != 0)
            { 
				Console.Clear();
				Console.WriteLine("BANK ACCOUNT APPLICATION\n");
				Console.WriteLine("\n1-Savings Credit\n2-Savings Debit\n3-Savings Interest\n4-Checking Credit\n5-Checking Debit\n6-Check Balances\n7-Exit\n------------------------\n0-Run Test");
				
				Console.Write("\nEnter choice: ");
				input = Convert.ToInt32(Console.ReadLine());
				while(input < 0 || input > 7)
                {
					Console.Write("\nEnter choice: ");
					input = Convert.ToInt32(Console.ReadLine());
                }

				decimal amount = 0; //amount used for credits/debits

				switch(input)
                {
					case 0:
						Console.Clear();
						testRun();
						break;
					case 1:
						Console.Clear();
						Console.Write("BANK ACCOUNT APPLICATION\n\nEnter amount to credit in savings: ");
						amount = Convert.ToDecimal(Console.ReadLine());
						amount = Convert.ToDecimal(Console.ReadLine());
						accounts[0].Credit(amount);
						Console.WriteLine($"\n\n${amount} credited into savings account.");
						break;
					case 2:
						Console.Clear();
						Console.Write("BANK ACCOUNT APPLICATION\n\nEnter amount to debit from savings: ");
						amount = Convert.ToDecimal(Console.ReadLine());
						amount = Convert.ToDecimal(Console.ReadLine());
						if(accounts[0].Debit(amount))
							Console.WriteLine($"\n\n${amount} debited from savings account.");
						break;
					case 3:
						Console.Clear();
						Console.Write("BANK ACCOUNT APPLICATION\n\nSavings interest calculating...");
						amount = accounts[0].CalculateInterest();
						accounts[0].Credit(amount);
						Console.WriteLine($"\n\n${amount} of interest has been credited.");
						break;
					case 4:
						Console.Clear();
						Console.Write("BANK ACCOUNT APPLICATION\n\nEnter amount to credit into checking: ");
						amount = Convert.ToDecimal(Console.ReadLine());
						amount = Convert.ToDecimal(Console.ReadLine());
						accounts[1].Credit(amount);
						Console.WriteLine($"\n\n${amount} credited into checking account.");
						break;
					case 5:
						Console.Clear();
						Console.Write("BANK ACCOUNT APPLICATION\n\nEnter amount to debit from checking: ");
						amount = Convert.ToDecimal(Console.ReadLine());
						amount = Convert.ToDecimal(Console.ReadLine());
						if(accounts[1].Debit(amount))
							Console.WriteLine($"\n\n${amount} debited from checking account.");
						break;
				 	case 6:
						Console.Clear();
						Console.WriteLine($"\nSavings Balance: ${accounts[0].AccountBalance}\nChecking Balance: ${accounts[1].AccountBalance}");
						break;
					default:
						break;
                }

				Console.WriteLine("\nPress enter to continue...");
				Console.ReadLine(); //holds open console
			}
        }
   	}

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
			if(bal >= 0.0m)
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
