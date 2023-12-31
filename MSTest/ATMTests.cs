using Testing;

namespace MSTest
{
    [TestClass]
    public class ATMTests
    {
        [TestMethod]
        public void GetCommission()
        {
            var atmVisa = new ATM(1000, ATM.CardType.Visa);
            var atmMastercard = new ATM(1000, ATM.CardType.Mastercard);

            Assert.AreEqual(ATMCommisions.Visa, atmVisa.GetCommission());
            Assert.AreEqual(ATMCommisions.Mastercard, atmMastercard.GetCommission());
        }

        [TestMethod]
        public void Deposit_ShouldAddToBalance()
        {
            decimal startingBalance = 100m;
            int depositAmount = 50;

            var atm = new ATM(startingBalance, ATM.CardType.Mastercard);
            atm.Deposit(depositAmount);

            Assert.AreEqual(startingBalance + depositAmount, atm.Balance);
            Assert.IsTrue(atm.Balance >= 0);
        }

        [TestMethod]
        public void Deposit_AmountLesserOrEqualZero()
        {
            var atm = new ATM(100m, ATM.CardType.Mastercard);

            Assert.ThrowsException<ATMException>(() => atm.Deposit(0));
            Assert.ThrowsException<ATMException>(() => atm.Deposit(-1));
        }

        [TestMethod]
        public void Withdraw_ShouldRemoveFromBalance()
        {
            decimal startingBalance = 100m;
            int withdrawAmount = 20;

            var atm = new ATM(startingBalance, ATM.CardType.Visa);
            atm.Withdraw(withdrawAmount);

            decimal endBalance = startingBalance - (withdrawAmount + withdrawAmount * atm.GetCommission());
            Assert.AreEqual(endBalance, atm.Balance);
        }

        [TestMethod]
        public void Withdraw_InsufficientFunds()
        {
            decimal startingBalance = 10;
            int withdrawAmount = 20;

            var atm = new ATM(startingBalance, ATM.CardType.Visa);
            Assert.ThrowsException<ATMException>(() => atm.Withdraw(withdrawAmount));
        }
    }
}