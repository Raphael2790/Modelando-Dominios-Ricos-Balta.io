using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Services;
using PaymentContext.Tests.Mocks;
using System;

namespace PaymentContext.Tests.HandlerTests
{   
    [TestClass]
    public class SubscriptionHandlerTests
    {   

        public SubscriptionHandlerTests()
        {

        }
        
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            //Arrange
            //Estamos testando o subscription handler
            var studentHandler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var boletoCommand = new CreateBoletoSubscriptionCommand();
            boletoCommand.FirstName = "Raphael";
            boletoCommand.LastName =  "Silvestre";
            boletoCommand.StudentDocument = "99999999999";
            boletoCommand.StudentEmail = "student@gmail.com";
            boletoCommand.BarCode = "1019181717";
            boletoCommand.BoletoNumber = "10101919";
            boletoCommand.PaymentNumber = "019189171";
            boletoCommand.PaidDate = DateTime.Now;
            boletoCommand.ExpireDate = DateTime.Now.AddDays(5);
            boletoCommand.Total = 10;
            boletoCommand.TotalPaid = 10;
            boletoCommand.Street = "Rua Sem Nome";
            boletoCommand.Number = "1020";
            boletoCommand.Neighborhood = "Vila Augusta";
            boletoCommand.City = "SP";
            boletoCommand.State = "SP";
            boletoCommand.Country = "BR";
            boletoCommand.ZipCode = "0101018917";
            boletoCommand.PayerDocument = "99919191919";
            boletoCommand.PayerDocumentType = EDocumentType.CPF;
            boletoCommand.PayerName = "Wayne Corp";
            boletoCommand.PayerEmail = "payer@hotmail.com";

            //Act
            studentHandler.Handle(boletoCommand);

            //Assert
            Assert.AreEqual(false, studentHandler.Valid);
        }
    }
}