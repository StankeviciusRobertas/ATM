using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    internal class Users
    {        
        public string CardId { get; set; }
        public string Pin { get; set; }
        public decimal Balance { get; set; }
        public List<decimal> Transcations { get; set; } = new List<decimal>();

        public bool WithdrawMoney(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                Transcations.Add(-amount); // Neigiamas skaicius zymi isiemima
                return true;
            }
            return false;
        }
    }
}
