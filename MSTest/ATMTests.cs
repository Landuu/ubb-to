using Testing;

namespace MSTest
{
    [TestClass]
    public class ATMTests
    {
        [TestMethod]
        public void GetCommission_ShouldBeForVisa()
        {
            var atm = new ATM(1000, ATM.CardType.Visa);

            var commission = atm.GetCommission();

            Assert.AreEqual(ATMCommisions.Visa, commission);
        }

        [TestMethod]
        public void GetCommission_ShouldBeZeroForMastercard()
        {
            var atm = new ATM(1000, ATM.CardType.Mastercard);

            var comission = atm.GetCommission();

            Assert.AreEqual(ATMCommisions.Mastercard, comission);
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
        public void Withdraw_ShouldRemoveFromBalance()
        {
            decimal startingBalance = 100m;
            int withdrawAmount = 20;

            var atm = new ATM(startingBalance, ATM.CardType.Visa);
            atm.Withdraw(withdrawAmount);

            decimal endBalance = startingBalance - (withdrawAmount + withdrawAmount * atm.GetCommission());
            Assert.AreEqual(endBalance, atm.Balance);
        }
    }
}