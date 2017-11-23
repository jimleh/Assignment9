using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDBank
{
    class Program
    {
        public static int UserId = 1;

        static void Main(string[] args)
        {
            var acc = new BankAccount();
            //BankManager manager = new BankManager();

            //int input;
            //string line;

            //do
            //{
            //    Console.WriteLine("Bank - Main Menu"
            //        + "\n1. Get Total Balance"
            //        + "\n2. Open a new Account"
            //        + "\n3. Show All Accounts"
            //        + "\n4. Show Account Details"
            //        + "\n5. Make a Deposit"
            //        + "\n6. Make a Withdrawal"
            //        + "\n7. Make a new Transaction"
            //        + "\n8. Show All Transactions for Account"
            //        + "\n9. Lock/Unlock Account"
            //        + "\n0. Exit");

            //    while (!int.TryParse(line = Console.ReadLine().ToString(), out input))
            //    {
            //        Console.WriteLine("Invalid input! Please read and follow the instructions.");
            //    }

            //    switch(input)
            //    {
            //        case 1:
            //            Console.WriteLine("Total balance for all accounts: " + GetTotalBalanceForUser(manager).ToString());
            //            break;
            //        case 2:
            //            Console.WriteLine("New account opened with the account number: " + CreateNewBankAccount(manager));
            //            break;
            //        case 3:
            //            var accs = GetAllAccountsForUser(manager);
            //            Console.WriteLine("\nPrinting basic information about all your accounts!");
            //            foreach(var acc in accs)
            //            {
            //                Console.WriteLine(acc.AccountNumber + " - " + acc.Balance + ":-");
            //            }
            //            Console.WriteLine();
            //            break;
            //        case 4:
            //            Console.WriteLine(ShowDetailsForAccount(manager));
            //            break;
            //        case 5:
            //            Console.WriteLine(MakeADeposit(manager));
            //            break;
            //        case 6:
            //            Console.WriteLine(MakeAWithdrawal(manager));
            //            break;
            //        case 7:
            //            Console.WriteLine(MakeTransaction(manager));
            //            break;
            //        case 8:
            //            var transactions = GetAllAccountsForUser(manager);
            //            Console.WriteLine("\nPrinting basic information about all your accounts!");
            //            foreach(var transaction in transactions)
            //            {
            //               // Console.WriteLine(transaction.AccountNumber + " - " + acc.Balance + ":-");
            //            }
            //            Console.WriteLine();
            //            break;
            //        case 9:
            //            Console.WriteLine(MakeTransaction(manager));
            //            break;
            //        case 0:
            //            Console.WriteLine("Exiting the Application");
            //            break;
            //        default:
            //            Console.WriteLine("Invalid input! Please read and follow the instructions.");
            //            break;
            //    }

            //}
            //while (input != 0);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }

        private static string MakeTransaction(BankManager manager)
        {
            int input = (int)GetValidInput("Sender Account number");
            var acc = manager.GetAccount(input);
            int input2 = (int)GetValidInput("Receiver Account number");
            var acc2 = manager.GetAccount(input2);

            double amount = GetValidInput("Amount");
            manager.Transaction(input, input2, amount);

            return amount.ToString() + " was transferred from account " + input + " to " + input2 + ".";
        }

        private static string MakeADeposit(BankManager manager)
        {
            int input = (int)GetValidInput("Account number");
            var acc = manager.GetAccount(input);

            double amount = GetValidInput("Amount");
            manager.InsertToAccount(input, amount);

            return amount.ToString() + " was desposited into the account: " + input.ToString();
        }

        private static string MakeAWithdrawal(BankManager manager)
        {
            int input = (int)GetValidInput("Account number");
            var acc = manager.GetAccount(input);

            double amount = GetValidInput("Amount");
            manager.WithdrawFromAccount(input, amount);

            return amount.ToString() + " was withdrawed from the account: " + input.ToString();

        }

        public static double GetTotalBalanceForUser(BankManager manager)
        {
            return manager.GetTotalBalance(UserId);
        }

        public static int CreateNewBankAccount(BankManager manager)
        {
            var acc = new BankAccount() { OwnerID = UserId };
            manager.CreateBankAccount( acc );

            return acc.AccountNumber;
        }

        public static IEnumerable<BankAccount> GetAllAccountsForUser(BankManager manager)
        {
            return manager.GetAllAccounts(UserId);
        }

        public static string ShowDetailsForAccount(BankManager manager)
        {
            int input = (int)GetValidInput("Account number");

            var acc = manager.GetAccount(input);
            return "Account Number: " + acc.AccountNumber
                + "Balance: " + acc.Balance;
            // And whatever other stuff may be in a bank account

        }

        private static double GetValidInput(string msg, bool isDouble = false)
        {
            string line;
            if(!isDouble)
            {
                int input;
                Console.WriteLine("Enter " + msg + ":");
                while (!int.TryParse(line = Console.ReadLine().ToString(), out input))
                {
                    Console.WriteLine("Enter a valid " + msg + ":");
                }

                return input;
            }

            double dInput;
            Console.WriteLine("Enter " + msg + ":");
            while (!double.TryParse(line = Console.ReadLine().ToString(), out dInput))
            {
                Console.WriteLine("Enter a valid " + msg + ":");
            }

            return dInput;
        }
    }
}
