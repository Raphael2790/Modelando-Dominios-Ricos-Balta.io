using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.EntitiesTests
{
    [TestClass]
    public class DocumentTests
    {
        //Red - Green - Refactory
        [TestMethod]
        [DataTestMethod]
        [DataRow("123", EDocumentType.CNPJ)]
        [DataRow("2341", EDocumentType.CPF)]
        [DataRow("928272662", EDocumentType.CNPJ)]
        public void ShouldReturnErrorWhenCNPJIsInvalid(string cnpj, EDocumentType type)
        {
            var doc = new Document(cnpj, type);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("40191919821234")]
        [DataRow("12345678919234")]
        public void ShouldReturnSuccessWhenCNPJIsValid(string cnpj)
        {
            var doc = new Document(cnpj, EDocumentType.CNPJ);
            Assert.IsTrue(doc.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CPF);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenCPFIsValid()
        {
            var doc = new Document("11111111111", EDocumentType.CPF);
            Assert.IsTrue(doc.Valid);
        }
    }
}