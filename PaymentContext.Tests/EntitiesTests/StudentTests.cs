using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.EntitiesTests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Student _student;
        private readonly Subscription _subscription;
        private readonly Name _name;
        private readonly Address _address;
        private readonly Document _document;
        private readonly Email _email;

        public StudentTests()
        {
            _name = new Name("Raphael", "Silvestre");
            _document = new Document("11111111111", EDocumentType.CPF);
            _email = new Email("rssshev@gmail.com");
            _student = new Student(document: _document, name: _name, email:_email);
            _address = new Address("rua lala", "234", "Vila Augista", "São Paulo", "Kansas", "Londres", "101010110");
            _subscription = new Subscription(DateTime.Now.AddMonths(6));
            
        }


        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            //Arrange
            var payment = new PayPalPayment("10101010", DateTime.Now, DateTime.Now.AddDays(5), 10, 10,_address, _document, "WAYNE CORP", _email);
            _subscription.AddPayment(payment);

            //Action
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            //Assert
            //Assert.Fail();
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenStudentHasOnlyOneSubscription()
        {
             //Arrange
            var payment = new PayPalPayment("10101010", DateTime.Now, DateTime.Now.AddDays(5), 10, 10,_address, _document, "WAYNE CORP", _email);
            _subscription.AddPayment(payment);

            //Action
            _student.AddSubscription(_subscription);

            //Assert
            //Assert.Fail();
            Assert.IsTrue(_student.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenStudentSubscriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenStudentSubscriptionHasPayment()
        {
            var payment = new PayPalPayment("10101010", DateTime.Now, DateTime.Now.AddDays(5), 10, 10,_address, _document, "WAYNE CORP", _email);
            _subscription.AddPayment(payment);

            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Valid);
        }
    }
}