using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDBank
{
    public class BankAccount
    {
        public int AccountNumber { get; set; }
        public double Balance { get; set; }
        public int OwnerID { get; set; }
        public bool Locked { get; set; }

        // public BankAccount() {}
        public BankAccount(Customer customer = null)
        {
            if(customer != null)
            {
                OwnerID = customer.CustomerID;
            }
        }

        ~BankAccount()
        {
        }
    }
}
