using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.EntitiesTests
{
    [TestClass]
    public class PaymentTests
    {
        [TestMethod]
        public void ShouldReturnSucessWhenPaymentWasTotalValue()
        {
            //Arrange
            var payment = new CreditCardPayment("Raphael", "404040", "098900", DateTime.Now, DateTime.Now.AddDays(6), 10, 10, null, null, "Batman", null);

            //Act

            //Assert
            Assert.IsTrue(payment.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenPaymentWasNotTotalValue()
        {
            //Arrange
            var payment = new CreditCardPayment("Raphael", "404040", "098900", DateTime.Now, DateTime.Now.AddDays(6), 10, 5, null, null, "Batman", null);
            //Act

            //Assert
            Assert.IsTrue(payment.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenPaymentTotalValueIsZero()
        {
            //Arrange
            var payment = new CreditCardPayment("Raphael", "404040", "098900", DateTime.Now, DateTime.Now.AddDays(6), 0, 10, null, null, "Batman", null);
            //Act

            //Assert
            Assert.IsTrue(payment.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenPaymentByPayPalHasNoTransactCode()
        {
            //Arrange
            var payment = new PayPalPayment("", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, null, null, "Wayne Corp", null);
            //Act

            //Assert
            Assert.IsTrue(payment.Invalid);
        }
    }
}