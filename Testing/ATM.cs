using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Testing
{
    public class ATM(decimal balance, ATM.CardType card)
    {
        public enum CardType
        {
            Visa,
            Mastercard
        }

        public decimal Balance { get; set; } = balance;

        public CardType Card { get; private set; } = card;

        public void Deposit(int amount)
        {
            if (amount <= 0)
                throw new Exception("Invalid amount");

            Balance += amount;
        }

        public decimal Withdraw(int amount)
        {
            var totalWithCommission = amount + amount * GetCommission();
            if (Balance - totalWithCommission <= 0)
                throw new Exception("Insufficient funds");

            Balance -= totalWithCommission;
            return totalWithCommission;
        }

        public decimal GetCommission()
        {
            return Card switch
            {
                CardType.Visa => ATMCommisions.Visa,
                CardType.Mastercard => ATMCommisions.Mastercard,
                _ => throw new Exception("Invalid card"),
            };
        }


    }
}
