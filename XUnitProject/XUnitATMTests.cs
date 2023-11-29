using Testing;

namespace XUnitProject
{
    public class XUnitATMTests
    {
        [Fact]
        public void GetCommission()
        {
            var atmVisa = new ATM(1000, ATM.CardType.Visa);
            var atmMastercard = new ATM(1000, ATM.CardType.Mastercard);

            Assert.Equal(ATMCommisions.Visa, atmVisa.GetCommission());
            Assert.Equal(ATMCommisions.Mastercard, atmMastercard.GetCommission());
        }

        [Fact]
        public void Deposit_ShouldAddToBalance()
        {
            decimal startingBalance = 100m;
            int depositAmount = 50;

            var atm = new ATM(startingBalance, ATM.CardType.Mastercard);
            atm.Deposit(depositAmount);

            Assert.Equal(startingBalance + depositAmount, atm.Balance);
            Assert.True(atm.Balance >= 0);
        }

        [Fact]
        public void Deposit_AmountLesserOrEqualZero()
        {
            var atm = new ATM(100m, ATM.CardType.Mastercard);

            Assert.Throws<ATMException>(() => atm.Deposit(0));
            Assert.Throws<ATMException>(() => atm.Deposit(-1));
        }

        [Fact]
        public void Withdraw_ShouldRemoveFromBalance()
        {
            decimal startingBalance = 100m;
            int withdrawAmount = 20;

            var atm = new ATM(startingBalance, ATM.CardType.Visa);
            atm.Withdraw(withdrawAmount);

            decimal endBalance = startingBalance - (withdrawAmount + withdrawAmount * atm.GetCommission());
            Assert.Equal(endBalance, atm.Balance);
        }

        [Fact]
        public void Withdraw_InsufficientFunds()
        {
            decimal startingBalance = 10;
            int withdrawAmount = 20;

            var atm = new ATM(startingBalance, ATM.CardType.Visa);
            Assert.Throws<ATMException>(() => atm.Withdraw(withdrawAmount));
        }
    }
}