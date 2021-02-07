using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;
using System.Linq;

namespace PaymentContext.Tests.QueriesTests
{
    [TestClass]
    public class StudentQueryTests
    {
        private IList<Student> _students;

        public StudentQueryTests()
        {
            _students = new List<Student>();
            for (int i = 0; i < 10; i++)
            {
                _students.Add(new Student(new Name("Aluno" +1, "Mock"), new Document("1111111111"+ i, EDocumentType.CPF),new Email("Aluno"+i+ "@gmail.com") )); 
            }
        }
        [TestMethod]
        public void ShouldReturnNullWhenDocumentNotExists()
        {
            //Arrange
            var querie = StudentQuery.GetStudentInfo("99999999999");

            //Act
            var student = _students.AsQueryable().Where(querie).FirstOrDefault();

            //Assert
            Assert.IsNull(student);
        }

        [TestMethod]
        public void ShouldReturnStudentWhenDocumentExists()
        {
            //Arrange
            var querie = StudentQuery.GetStudentInfo("11111111111");

            //Act
            var student = _students.AsQueryable().Where(querie).FirstOrDefault();

            //Assert
            Assert.IsNotNull(student);
        }
    }
}