namespace Testing
{
    public class ATM(decimal balance, ATM.CardType card)
    {
        public enum CardType
        {
            Visa,
            Mastercard
        }

        // Properties
        public decimal Balance { get; set; } = balance;

        public CardType Card { get; private set; } = card;

        // Methods
        public void Deposit(int amount)
        {
            if (amount <= 0)
                throw new ATMException("Invalid amount");

            Balance += amount;
        }

        public decimal Withdraw(int amount)
        {
            var totalWithCommission = amount + amount * GetCommission();
            if (Balance - totalWithCommission <= 0)
                throw new ATMException("Insufficient funds");

            Balance -= totalWithCommission;
            return totalWithCommission;
        }

        public decimal GetCommission()
        {
            return Card switch
            {
                CardType.Visa => ATMCommisions.Visa,
                CardType.Mastercard => ATMCommisions.Mastercard,
                _ => throw new ATMException("Invalid card"),
            };
        }
    }
}
