using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Queries;

namespace PaymentContext.Tests.QueriesTests
{
    [TestClass]
    public class SubscriptionQueryTests
    {
        private IList<Subscription> _subscriptions;
        public SubscriptionQueryTests()
        {
            _subscriptions = new List<Subscription>();
            for (int i = 0; i < 5; i++)
            {
                var sub = new Subscription(null);
                sub.InactivateSub();
                _subscriptions.Add(sub);
            }
        }
        [TestMethod]
        public void ShouldReturnNullWhenNoSubscriptionActive()
        {
            //Arrange   
            var querie = SubscriptionQuery.GetStudentSubscriptionActive();

            //Act
            var sub = _subscriptions.AsQueryable().Where(querie).FirstOrDefault();
            //Assert
            Assert.IsNull(sub);
        }

    }
}