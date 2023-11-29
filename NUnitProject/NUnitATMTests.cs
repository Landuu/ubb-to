using Testing;

namespace NUnitProject
{
    [TestFixture]
    public class NUnitATMTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetCommission()
        {
            var atmVisa = new ATM(1000, ATM.CardType.Visa);
            var atmMastercard = new ATM(1000, ATM.CardType.Mastercard);
            
            Assert.Multiple(() =>
            {
                Assert.That(atmVisa.GetCommission(), Is.EqualTo(ATMCommisions.Visa));
                Assert.That(atmMastercard.GetCommission(), Is.EqualTo(ATMCommisions.Mastercard));
            });
        }

        [Test]
        public void Deposit_ShouldAddToBalance()
        {
            decimal startingBalance = 100m;
            int depositAmount = 50;

            var atm = new ATM(startingBalance, ATM.CardType.Mastercard);
            atm.Deposit(depositAmount);

            Assert.That(atm.Balance, Is.EqualTo(startingBalance + depositAmount));
            Assert.That(atm.Balance, Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void Deposit_AmountLesserOrEqualZero()
        {
            var atm = new ATM(100m, ATM.CardType.Mastercard);

            Assert.Throws<ATMException>(() => atm.Deposit(0));
            Assert.Throws<ATMException>(() => atm.Deposit(-1));
        }

        [Test]
        public void Withdraw_ShouldRemoveFromBalance()
        {
            decimal startingBalance = 100m;
            int withdrawAmount = 20;

            var atm = new ATM(startingBalance, ATM.CardType.Visa);
            atm.Withdraw(withdrawAmount);

            decimal endBalance = startingBalance - (withdrawAmount + withdrawAmount * atm.GetCommission());
            Assert.That(atm.Balance, Is.EqualTo(endBalance));
        }

        [Test]
        public void Withdraw_InsufficientFunds()
        {
            decimal startingBalance = 10;
            int withdrawAmount = 20;

            var atm = new ATM(startingBalance, ATM.CardType.Visa);
            Assert.Throws<ATMException>(() => atm.Withdraw(withdrawAmount));
        }
    }
}