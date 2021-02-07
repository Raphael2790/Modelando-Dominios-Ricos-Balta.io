using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.CommandTests
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenCreateBoletoSubscriptionCommandFirstnameHasMaxLengthMoreThanTwenty()
        {
            //Arrange
            var boletoCommand = new CreateBoletoSubscriptionCommand();

            //Act
            boletoCommand.FirstName = "Raphael Silva Santos Sauro";
            boletoCommand.LastName = "Santos";
            boletoCommand.StudentDocument = "10101010111";
            boletoCommand.PayerDocument ="11111111111";
            boletoCommand.StudentEmail = "rssshev@hotmail.com";
            boletoCommand.Validate();

            //Assert
            Assert.IsTrue(boletoCommand.Invalid);
        }

        
    }
}