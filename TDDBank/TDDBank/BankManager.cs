using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDBank
{
    public class BankManager
    {
        public List<BankAccount> Accounts { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Transaction> Transactions { get; set; }

        public BankManager()
        {
            Accounts = new List<BankAccount>();
            Customers = new List<Customer>();
            Transactions = new List<Transaction>();
            InitCustomers();
        }

        protected void InitCustomers()
        {
            Customers.Add(new Customer() { CustomerID = 1, Name = "Customer1" });
            Customers.Add(new Customer() { CustomerID = 2, Name = "Customer2" });
        }

        // Transactions
        public void Transaction(int? senderId, int? receiverId, double? amount)
        {
            if (senderId == null)
            {
                throw new NullReferenceException("Sender Id cannot be null!");
            }
            if (receiverId == null)
            {
                throw new NullReferenceException("Receiver Id cannot be null!");
            }
            if (amount == null)
            {
                throw new NullReferenceException("Amount cannot be null!");
            }

            var sender = Accounts.FirstOrDefault(a => a.AccountNumber == senderId.Value);
            if (sender == null)
            {
                throw new NullReferenceException("No Account Found with the given Sender ID!");
            }

            var receiver = Accounts.FirstOrDefault(a => a.AccountNumber == receiverId.Value);
            if (receiver == null)
            {
                throw new NullReferenceException("No Account Found with the given Receiver ID!");
            }

            if (sender.Balance < amount)
            {
                throw new Exception("Sender balance is smaller than the amount being sent!");
            }

            sender.Balance -= amount.Value;
            receiver.Balance += amount.Value;

            Transactions.Add(new Transaction { SenderID = senderId.Value, ReceiverID = receiverId.Value, Amount = amount.Value, Date = DateTime.Now.ToString() });
        }
        public IEnumerable<Transaction> GetAllTransactionsForAccount(int? id)
        {
            if(id == null)
            {
                throw new NullReferenceException("Id cannot be null!");
            }

            var acc = Accounts.FirstOrDefault(a => a.AccountNumber == id.Value);
            if(acc == null)
            {
                throw new NullReferenceException("That Account does not exist!");
            }
            return Transactions.Where(t => t.SenderID == id.Value || t.ReceiverID == id.Value).ToList();
        }

        // For Managing Bank Accounts
        public void LockAccount(int? id)
        {
            if(id == null)
            {
                throw new NullReferenceException("Id cannot be null!");
            }

            var acc = Accounts.FirstOrDefault(a => a.AccountNumber == id.Value);
            if (acc == null)
            {
                throw new NullReferenceException("No Account found!");
            }

            if(acc.Locked)
            {
                throw new Exception("Account is already Locked!");
            }

            acc.Locked = !acc.Locked;
        }
        public void UnlockAccount(int? id)
        {
            if (id == null)
            {
                throw new NullReferenceException("Id cannot be null!");
            }

            var acc = Accounts.FirstOrDefault(a => a.AccountNumber == id.Value);
            if (acc == null)
            {
                throw new NullReferenceException("No Account found!");
            }

            if (!acc.Locked)
            {
                throw new Exception("Account is already Unlocked!");
            }

            acc.Locked = !acc.Locked;
        }
        public void CreateBankAccount(BankAccount acc)
        {
            if(acc == null)
            {
                throw new NullReferenceException("Account can't be null!");
            }

            if(acc.AccountNumber == 0)
            {
                acc.AccountNumber = Accounts.Count + 1;
            }

            if(Customers.Count == 0)
            {
                throw new NullReferenceException("No existing Customers!");
            }

            var owner = Customers.FirstOrDefault(c => c.CustomerID == acc.OwnerID);
            if (owner == null)
            {
                throw new NullReferenceException("No Customer with the given ID!");
            }

            var tmp = Accounts.FirstOrDefault(a => a.AccountNumber == acc.AccountNumber);
            if(tmp != null)
            {
                throw new Exception("An Account with that Account Number already exists!");
            }

            Accounts.Add(acc);
        }
        public void WithdrawFromAccount(int? id, double amount)
        {
            if (id == null)
            {
                throw new NullReferenceException("ID cannot be null!");
            }

            var acc = Accounts.FirstOrDefault(a => a.AccountNumber == id.Value);
            if (acc == null)
            {
                throw new NullReferenceException("No Account Found with the given ID!");
            }

            acc.Balance -= amount;
        }
        public void InsertToAccount(int? id, double amount)
        {
            if (id == null)
            {
                throw new NullReferenceException("ID cannot be null!");
            }

            var acc = Accounts.FirstOrDefault(a => a.AccountNumber == id.Value);
            if (acc == null)
            {
                throw new NullReferenceException("No Account Found with the given ID!");
            }

            acc.Balance += amount;
        }
        public double GetBalance(int? id)
        {
            if (id == null)
            {
                throw new NullReferenceException("ID cannot be null!");
            }

            var acc = Accounts.FirstOrDefault(a => a.AccountNumber == id.Value);
            if (acc == null)
            {
                throw new NullReferenceException("No Account Found with the given ID!");
            }

            return acc.Balance;
        }
        public double GetTotalBalance(int? id)
        {
            if(id == null)
            {
                throw new NullReferenceException("Id cannot be null!");
            }

            var customer = Customers.FirstOrDefault(c => c.CustomerID == id.Value);
            if(customer == null)
            {
                throw new NullReferenceException("That Customer does not exist!");
            }

            var total = 0.0;
            var accs = Accounts.Where(a => a.OwnerID == id.Value);
            foreach(var acc in accs)
            {
                total += acc.Balance;
            }

            return total;
        }
        public IEnumerable<BankAccount> GetAllAccounts(int? id)
        {
            if(id == null)
            {
                throw new NullReferenceException("Id cannot be null!");
            }

            var customer = Customers.FirstOrDefault(c => c.CustomerID == id.Value);
            if(customer == null)
            {
                throw new NullReferenceException("No Customer found!");
            }

            return Accounts.Where(a => a.OwnerID == id.Value).ToList();
        }
        public BankAccount GetAccount(int? id)
        {
            if(id == null)
            {

            }

            var acc = Accounts.FirstOrDefault(a => a.AccountNumber == id.Value);
            if(acc == null)
            {

            }

            return acc;
        }

        // For Managing Customers
        public void CreateCustomer(Customer customer) // Won't really be used
        {
            if(customer == null)
            {
                throw new NullReferenceException("Customer can't be null!");
            }

            if (customer.CustomerID == 0)
            {
                customer.CustomerID = Customers.Count + 1;
            }

            var tmp = Customers.FirstOrDefault(c => c.CustomerID == customer.CustomerID);
            if (tmp != null)
            {
                throw new Exception("A Customer with that Customer ID already exists!");
            }

            Customers.Add(customer);
        }
        public Customer GetCustomer(int? id)
        {
            if (id == null)
            {
                throw new NullReferenceException("Id can't be null!");
            }

            var customer = Customers.FirstOrDefault(c => c.CustomerID == id.Value);
            if (customer == null)
            {
                throw new NullReferenceException("Customer can't be null!");
            }

            return customer;
        }
    }
}
