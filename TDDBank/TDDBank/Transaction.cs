using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDBank
{
    public class Transaction
    {
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public double Amount { get; set; }
        public string Date { get; set; }
    }
}
